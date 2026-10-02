package gateway

// Console command surface: the interactive server console and operator tooling
// execute game commands through here. Console callers are fully privileged.
// Commands use flat space-separated arguments, "/"
// prefix accepted but optional.

import (
	"context"
	"errors"
	"fmt"
	"sort"
	"strconv"
	"strings"
	"time"
)

// consoleUsage is the operator-facing help table.
var consoleUsage = map[string]string{
	"help":        "/help - show this command list",
	"status":      "/status - show server status",
	"reload":      "/reload - reload gameplay config",
	"give":        "/give <playerId> <itemId> [count] - give an item to a player",
	"giveall":     "/giveall <itemId> <count> - give an item to every player",
	"gold":        "/gold <playerId> <amount> - set a player's gold",
	"setlevel":    "/setlevel <playerId> <level> - set a player's level",
	"kick":        "/kick <playerId> - disconnect a player",
	"mail":        "/mail <playerId|all> <title> | <body> - send a system mail",
	"finishroom":  "/finishroom <roomId> - force-finish a running room",
	"accounts":    "/accounts - count registered accounts",
	"delplayer":   "/delplayer <playerId> - delete a player",
	"connections": "/connections - list online player ids",
	"stop":        "/stop - shut the server down",
}

// ConsoleExec runs one operator command line from the server console.
func (s *Server) ConsoleExec(ctx context.Context, line string) (string, error) {
	line = strings.TrimSpace(line)
	if line == "" {
		return "", errors.New("empty command")
	}
	fields := strings.Fields(line)
	name := strings.TrimPrefix(strings.TrimPrefix(fields[0], "/"), "!")
	args := fields[1:]
	fn, ok := consoleCommands[name]
	if !ok {
		return "", fmt.Errorf("unknown command %q - try /help", name)
	}
	reply, err := fn(ctx, s, args)
	s.log.Info("console command", "command", name, "args", strings.Join(args, " "), "err", err)
	if _, auditErr := s.store.DB().ExecContext(ctx,
		`INSERT INTO admin_audit(action,detail,created_at) VALUES(?,?,?)`,
		"console_command",
		fmt.Sprintf(`{"command":%q,"args":%q}`, name, strings.Join(args, " ")),
		time.Now().Unix()); auditErr != nil {
		s.log.Warn("console audit write failed", "err", auditErr)
	}
	if err != nil {
		return "", err
	}
	return reply, nil
}

// ConsoleUsages lists the command usage strings for /help.
func (s *Server) ConsoleUsages() []string {
	names := make([]string, 0, len(consoleUsage))
	for name := range consoleUsage {
		names = append(names, name)
	}
	sort.Strings(names)
	out := make([]string, 0, len(names))
	for _, name := range names {
		out = append(out, consoleUsage[name])
	}
	return out
}

// Stop requests a graceful shutdown of every listener.
func (s *Server) Stop() { s.stopOnce.Do(func() { close(s.stopCh) }) }

type consoleFn func(ctx context.Context, s *Server, args []string) (string, error)

var consoleCommands = map[string]consoleFn{
	"help":        consoleHelp,
	"status":      consoleStatus,
	"reload":      consoleReload,
	"give":        consoleGive,
	"giveall":     consoleGiveAll,
	"gold":        consoleGold,
	"setlevel":    consoleSetLevel,
	"kick":        consoleKick,
	"mail":        consoleMail,
	"finishroom":  consoleFinishRoom,
	"accounts":    consoleAccounts,
	"delplayer":   consoleDelPlayer,
	"connections": consoleConnections,
}

func consoleHelp(ctx context.Context, s *Server, args []string) (string, error) {
	return strings.Join(s.ConsoleUsages(), "\n"), nil
}

func consoleStatus(ctx context.Context, s *Server, args []string) (string, error) {
	g := s.gameplay.Get()
	return fmt.Sprintf("uptime=%s tables=%d gameplay=%s",
		time.Since(s.startedAt).Round(time.Second), s.resources.TableCount(), g.String()), nil
}

func consoleReload(ctx context.Context, s *Server, args []string) (string, error) {
	s.gameplay.Reload()
	return "gameplay config reloaded: " + s.gameplay.Get().String(), nil
}

func consoleGive(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) < 2 || len(args) > 3 {
		return "", errors.New("usage: /give <playerId> <itemId> [count]")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("playerId %q is not a number", args[0])
	}
	itemID, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil {
		return "", fmt.Errorf("itemId %q is not a number", args[1])
	}
	count := int64(1)
	if len(args) == 3 {
		if count, err = strconv.ParseInt(args[2], 10, 64); err != nil || count <= 0 || count > 100000 {
			return "", fmt.Errorf("count %q must be 1..100000", args[2])
		}
	}
	if _, found := s.resources.Get("Item_infos", itemID); !found {
		return "", fmt.Errorf("item %d is not in Item_infos", itemID)
	}
	if _, err := s.store.LookupPlayer(ctx, playerID); err != nil {
		return "", fmt.Errorf("player %d not found", playerID)
	}
	if err := s.store.AddItem(ctx, playerID, int32(itemID), int32(count)); err != nil {
		return "", err
	}
	push := s.gmBagPush(playerID, int32(itemID), int32(count))
	s.broadcastPush(push, [3]byte{1, 0, 0})
	return fmt.Sprintf("gave item %d x%d to player %d", itemID, count, playerID), nil
}

func consoleGiveAll(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 2 {
		return "", errors.New("usage: /giveall <itemId> <count>")
	}
	itemID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("itemId %q is not a number", args[0])
	}
	count, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil || count <= 0 || count > 100000 {
		return "", fmt.Errorf("count %q must be 1..100000", args[1])
	}
	if _, found := s.resources.Get("Item_infos", itemID); !found {
		return "", fmt.Errorf("item %d is not in Item_infos", itemID)
	}
	ids, err := s.store.AllPlayerIDs(ctx)
	if err != nil {
		return "", err
	}
	for _, id := range ids {
		if err := s.store.AddItem(ctx, id, int32(itemID), int32(count)); err != nil {
			return "", fmt.Errorf("player %d: %w", id, err)
		}
		s.broadcastPush(s.gmBagPush(id, int32(itemID), int32(count)), [3]byte{1, 0, 0})
	}
	return fmt.Sprintf("gave item %d x%d to %d players", itemID, count, len(ids)), nil
}

func consoleGold(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 2 {
		return "", errors.New("usage: /gold <playerId> <amount>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("playerId %q is not a number", args[0])
	}
	amount, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil || amount < 0 || amount > 99999999 {
		return "", fmt.Errorf("amount %q must be 0..99999999", args[1])
	}
	if err := s.store.SetPlayerGoldLobby(ctx, playerID, int32(amount)); err != nil {
		return "", err
	}
	return fmt.Sprintf("set player %d gold to %d", playerID, amount), nil
}

func consoleSetLevel(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 2 {
		return "", errors.New("usage: /setlevel <playerId> <level>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("playerId %q is not a number", args[0])
	}
	level, err := strconv.ParseInt(args[1], 10, 64)
	if err != nil || level < 1 || level > 200 {
		return "", fmt.Errorf("level %q must be 1..200", args[1])
	}
	if err := s.store.SetPlayerLevelLobby(ctx, playerID, int32(level)); err != nil {
		return "", err
	}
	return fmt.Sprintf("set player %d level to %d", playerID, level), nil
}

func consoleKick(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 1 {
		return "", errors.New("usage: /kick <playerId>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("playerId %q is not a number", args[0])
	}
	if !s.PlayerOnline(playerID) {
		return "", fmt.Errorf("player %d is not online", playerID)
	}
	s.DisconnectPlayer(playerID)
	return fmt.Sprintf("kicked player %d", playerID), nil
}

func consoleMail(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) < 3 {
		return "", errors.New("usage: /mail <playerId|all> <title> | <body>")
	}
	rest := strings.Join(args[1:], " ")
	title, body, _ := strings.Cut(rest, "|")
	title = strings.TrimSpace(title)
	body = strings.TrimSpace(body)
	if title == "" {
		return "", errors.New("mail title is empty")
	}
	if strings.EqualFold(args[0], "all") {
		n, err := s.SendAdminMailAll(ctx, title, body, "Server", nil)
		if err != nil {
			return "", err
		}
		return fmt.Sprintf("sent mail %q to %d players", title, n), nil
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("target must be a playerId or 'all'")
	}
	if _, err := s.SendAdminMail(ctx, playerID, title, body, "Server", nil); err != nil {
		return "", err
	}
	return fmt.Sprintf("sent mail %q to player %d", title, playerID), nil
}

func consoleFinishRoom(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 1 {
		return "", errors.New("usage: /finishroom <roomId>")
	}
	roomID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("roomId %q is not a number", args[0])
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return "", err
	}
	if room.State != 25 {
		return "", errors.New("room is not running")
	}
	push, err := s.finishRoomPush(ctx, room, 0)
	if err != nil {
		return "", err
	}
	s.broadcastPush(push, [3]byte{1, 0, 0})
	s.log.Warn("room was force finished from the console", "room_id", roomID)
	return fmt.Sprintf("room %d force finished", roomID), nil
}

func consoleAccounts(ctx context.Context, s *Server, args []string) (string, error) {
	n, err := s.store.CountAccounts(ctx)
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("%d accounts registered", n), nil
}

func consoleDelPlayer(ctx context.Context, s *Server, args []string) (string, error) {
	if len(args) != 1 {
		return "", errors.New("usage: /delplayer <playerId>")
	}
	playerID, err := strconv.ParseInt(args[0], 10, 64)
	if err != nil {
		return "", fmt.Errorf("playerId %q is not a number", args[0])
	}
	if err := s.store.AdminDeletePlayer(ctx, playerID); err != nil {
		return "", err
	}
	return fmt.Sprintf("deleted player %d", playerID), nil
}

func consoleConnections(ctx context.Context, s *Server, args []string) (string, error) {
	ids := s.hub.onlinePlayerIDs()
	if len(ids) == 0 {
		return "no players online", nil
	}
	parts := make([]string, 0, len(ids))
	for _, id := range ids {
		parts = append(parts, strconv.FormatInt(id, 10))
	}
	return fmt.Sprintf("%d online: %s", len(ids), strings.Join(parts, ", ")), nil
}
