package gateway

import (
	"context"
	"errors"
	"fmt"
	"sort"
	"strconv"
	"strings"
	"time"

	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// gmCommand is one operator command, invoked from the game client by sending a
// private chat message that starts with "/" to any friend. Console-style
// flat args; access is gated by gameplay.GmAllowedAccounts.
type gmCommand struct {
	fn func(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error)
}

// gmOutcome is the chat reply plus optional protocol pushes that ride the
// dispatch response back to the caller.
type gmOutcome struct {
	reply  string
	pushes []Push
}

// gmCaller identifies the account behind a GM request so the allowlist can be
// evaluated against stable account fields rather than player nicknames.
type gmCaller struct {
	playerID int64
	loginKey string
	phone    string
}

// gmUsageList is the single source of truth for usage strings so package-level
// command registration and help rendering never reference each other.
var gmUsageList = map[string]string{
	"help":     "/help",
	"status":   "/status",
	"reload":   "/reload",
	"give":     "/give <itemId> [count]",
	"gold":     "/gold <playerId> <amount>",
	"setlevel": "/setlevel <playerId> <level>",
}

var gmCommands = map[string]gmCommand{
	"help":     {fn: gmHelp},
	"status":   {fn: gmStatus},
	"reload":   {fn: gmReload},
	"give":     {fn: gmGive},
	"gold":     {fn: gmGold},
	"setlevel": {fn: gmSetLevel},
}

func gmCommandNames() []string {
	names := make([]string, 0, len(gmCommands))
	for name := range gmCommands {
		names = append(names, name)
	}
	sort.Strings(names)
	return names
}

// gmExec parses and runs one GM chat line. ok=false means the message falls
// through to normal private chat handling (not a command, or caller denied).
func (s *Server) gmExec(ctx context.Context, callerID int64, line string) (string, []Push, bool) {
	line = strings.TrimSpace(line)
	if !strings.HasPrefix(line, "/") {
		return "", nil, false
	}
	fields := strings.Fields(line)
	name := strings.TrimPrefix(fields[0], "/")
	cmd, ok := gmCommands[name]
	if !ok {
		return "", nil, false
	}
	caller, err := s.gmCaller(ctx, callerID)
	if err != nil {
		s.log.Warn("gm caller lookup failed", "player_id", callerID, "err", err)
		return "", nil, false
	}
	if !s.gameplay.Get().GMAllowed(caller.loginKey, caller.phone) {
		return "", nil, false
	}
	reply, err := cmd.fn(ctx, s, caller, fields[1:])
	if err != nil {
		return fmt.Sprintf("[GM] %s failed: %v", name, err), nil, true
	}
	s.log.Info("gm command executed", "command", name, "caller_player_id", callerID, "args", strings.Join(fields[1:], " "))
	if _, err := s.store.DB().ExecContext(ctx, `INSERT INTO admin_audit(action,detail,created_at) VALUES(?,?,?)`,
		"gm_command", fmt.Sprintf(`{"command":%q,"caller_player_id":%d,"args":%q}`, name, callerID, strings.Join(fields[1:], " ")), time.Now().Unix()); err != nil {
		s.log.Warn("gm audit write failed", "err", err)
	}
	return reply.reply, reply.pushes, true
}

func (s *Server) gmCaller(ctx context.Context, playerID int64) (*gmCaller, error) {
	var loginKey, phone string
	err := s.store.DB().QueryRowContext(ctx,
		`SELECT COALESCE(a.login_key,''),COALESCE(a.phone,'') FROM players p JOIN accounts a ON a.id=p.account_id WHERE p.id=?`,
		playerID).Scan(&loginKey, &phone)
	if err != nil {
		return nil, err
	}
	return &gmCaller{playerID: playerID, loginKey: loginKey, phone: phone}, nil
}

func gmHelp(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	names := make([]string, 0, len(gmUsageList))
	for name := range gmUsageList {
		names = append(names, name)
	}
	sort.Strings(names)
	lines := make([]string, 0, len(names))
	for _, name := range names {
		lines = append(lines, gmUsageList[name])
	}
	return gmOutcome{reply: "[GM] commands:\n" + strings.Join(lines, "\n")}, nil
}

func gmStatus(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	g := s.gameplay.Get()
	return gmOutcome{reply: fmt.Sprintf("[GM] %s tables=%d", g.String(), s.resources.TableCount())}, nil
}

func gmReload(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	s.gameplay.Reload()
	return gmOutcome{reply: "[GM] gameplay config reloaded: " + s.gameplay.Get().String()}, nil
}

func gmGive(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	if len(args) < 1 || len(args) > 2 {
		return gmOutcome{}, errors.New("usage: /give <itemId> [count]")
	}
	itemID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return gmOutcome{}, fmt.Errorf("itemId %q is not a number", args[0])
	}
	count := int64(1)
	if len(args) == 2 {
		if count, err = strconv.ParseInt(args[1], 10, 64); err != nil || count <= 0 || count > 100000 {
			return gmOutcome{}, fmt.Errorf("count %q must be 1..100000", args[1])
		}
	}
	if _, found := s.resources.Get("Item_infos", itemID); !found {
		return gmOutcome{}, fmt.Errorf("item %d is not in Item_infos", itemID)
	}
	if err := s.store.AddItem(ctx, caller.playerID, int32(itemID), int32(count)); err != nil {
		return gmOutcome{}, err
	}
	return gmOutcome{
		reply:  fmt.Sprintf("[GM] gave item %d x%d to player %d", itemID, count, caller.playerID),
		pushes: []Push{s.gmBagPush(caller.playerID, int32(itemID), int32(count))},
	}, nil
}

func gmGold(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	if len(args) != 2 {
		return gmOutcome{}, errors.New("usage: /gold <playerId> <amount>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return gmOutcome{}, fmt.Errorf("playerId %q is not a number", args[0])
	}
	amount, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil || amount < 0 || amount > 99999999 {
		return gmOutcome{}, fmt.Errorf("amount %q must be 0..99999999", args[1])
	}
	if _, err := s.store.LookupPlayer(ctx, playerID); err != nil {
		return gmOutcome{}, fmt.Errorf("player %d not found", playerID)
	}
	if err := s.store.SetPlayerGoldLobby(ctx, playerID, int32(amount)); err != nil {
		return gmOutcome{}, err
	}
	return gmOutcome{reply: fmt.Sprintf("[GM] set player %d gold to %d", playerID, amount)}, nil
}

func gmSetLevel(ctx context.Context, s *Server, caller *gmCaller, args []string) (gmOutcome, error) {
	if len(args) != 2 {
		return gmOutcome{}, errors.New("usage: /setlevel <playerId> <level>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return gmOutcome{}, fmt.Errorf("playerId %q is not a number", args[0])
	}
	level, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil || level < 1 || level > 200 {
		return gmOutcome{}, fmt.Errorf("level %q must be 1..200", args[1])
	}
	if err := s.store.SetPlayerLevelLobby(ctx, playerID, int32(level)); err != nil {
		return gmOutcome{}, err
	}
	return gmOutcome{reply: fmt.Sprintf("[GM] set player %d level to %d", playerID, level)}, nil
}

// gmBagPush builds the inventory change push for a lobby grant. It is returned
// through DispatchResult.Pushes because pushes ride the request response.
func (s *Server) gmBagPush(playerID int64, itemID int32, count int32) Push {
	return s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: []*modelpb.ItemEtc{{ItemId: itemID, Count: count}},
	}, []int64{playerID}, 0)
}

// GMExec runs one GM line for the admin console. The admin token already
// gates access, so the gameplay allowlist is not consulted here; execution
// and audit stay identical to chat-triggered commands.
func (s *Server) GMExec(ctx context.Context, playerID int64, line string) (string, error) {
	line = strings.TrimSpace(line)
	if !strings.HasPrefix(line, "/") {
		return "", errors.New("command must start with /")
	}
	fields := strings.Fields(line)
	name := strings.TrimPrefix(fields[0], "/")
	cmd, ok := gmCommands[name]
	if !ok {
		return "", fmt.Errorf("unknown command %q", name)
	}
	caller := &gmCaller{playerID: playerID}
	outcome, err := cmd.fn(ctx, s, caller, fields[1:])
	s.log.Info("gm command via admin", "command", name, "player_id", playerID, "args", strings.Join(fields[1:], " "), "err", err)
	if _, auditErr := s.store.DB().ExecContext(ctx, `INSERT INTO admin_audit(action,detail,created_at) VALUES(?,?,?)`,
		"gm_command_admin", fmt.Sprintf(`{"command":%q,"player_id":%d,"args":%q}`, name, playerID, strings.Join(fields[1:], " ")), time.Now().Unix()); auditErr != nil {
		s.log.Warn("gm audit write failed", "err", auditErr)
	}
	if err != nil {
		return "", err
	}
	return outcome.reply, nil
}

// GMCommandUsages lists command usage strings for the admin console.
func (s *Server) GMCommandUsages() []string {
	names := gmCommandNames()
	usages := make([]string, 0, len(names))
	for _, name := range names {
		usages = append(usages, gmUsageList[name])
	}
	return usages
}
