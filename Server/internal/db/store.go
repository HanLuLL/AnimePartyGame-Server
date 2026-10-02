package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"

	_ "github.com/mattn/go-sqlite3"
)

var (
	ErrPlayerNotFound          = errors.New("player not found")
	ErrMoveAlreadyRolled       = errors.New("movement already rolled for this turn")
	ErrTurnPlayerMismatch      = errors.New("player does not own the current turn")
	ErrActionNotReady          = errors.New("action is not ready in the current turn phase")
	ErrInvalidLotteryChoice    = errors.New("lottery choice count or value is invalid")
	ErrInvalidPursuitTarget    = errors.New("pursuit target is not eligible")
	ErrInvalidDivinationChoice = errors.New("Divination choice is not in the offered pair")
	ErrInvalidSkillEventChoice = errors.New("skill event choice is not in the offered pair")
)

const (
	TurnPhaseThrowDice       = "throw_dice"
	TurnPhaseControlMove     = "control_move"
	TurnPhaseBombThrow       = "bomb_throw"
	TurnPhaseMoving          = "moving"
	TurnPhaseMoveAgain       = "move_again"
	TurnPhaseRollGold        = "roll_gold"
	TurnPhaseLandHeal        = "land_heal"
	TurnPhaseLandBloodLoss   = "land_blood_loss"
	TurnPhaseHospital        = "hospital"
	TurnPhaseFillingStation  = "filling_station"
	TurnPhaseAbandonCards    = "abandon_cards"
	TurnPhaseEvent           = "event"
	TurnPhaseDestiny         = "destiny"
	TurnPhaseDivination      = "divination"
	TurnPhaseSelectEvent     = "select_event"
	TurnPhaseGambleGuess     = "gamble_guess"
	TurnPhaseGambleThrow     = "gamble_throw"
	TurnPhaseLottery         = "lottery"
	TurnPhasePursuit         = "pursuit"
	TurnPhaseShop            = "shop"
	TurnPhasePVEShop         = "pve_shop"
	TurnPhaseBattery         = "battery"
	TurnPhaseBattleChallenge = "battle_challenge"
	TurnPhaseBattleCards     = "battle_cards"
	TurnPhaseBattleAttack    = "battle_attack"
	TurnPhaseBattleDefense   = "battle_defense"
)

type EventAttrDelta struct {
	PlayerID     int64
	HPChange     int32
	SetHP        bool
	HPValue      int32
	MaxHP        int32
	GoldChange   int32
	Cards        []CardState
	ReplaceCards bool
	// DiscardLimit pauses a single-player Destiny draw after granting its full
	// configured count when the resulting hand exceeds this limit.
	DiscardLimit int32
	Buffs        []BuffState
	LandBuffs    []LandBuffState
	PlaceChanged bool
	NodeID       int32
	BackNodeID   int32
	FrontNodeIDs []int32
}

const (
	destinyDamageDownBuffID int32 = 4000301
	destinyDamageUpBuffID   int32 = 4000401
)

// ApplyDestinyDamageBuffs resolves the one-shot damage modifiers defined by
// Destiny_infos 40003/40004 and Buff_infos 4000301/4000401. The caller must
// persist the returned remaining buffs with the corresponding HP change.
func ApplyDestinyDamageBuffs(damage int32, buffs []BuffState) (int32, []BuffState, []BuffState) {
	if damage <= 0 {
		return damage, append([]BuffState(nil), buffs...), nil
	}
	remaining := make([]BuffState, 0, len(buffs))
	removed := make([]BuffState, 0, 2)
	adjustment := int32(0)
	for _, buff := range buffs {
		switch buff.BuffID {
		case destinyDamageDownBuffID:
			value := buff.DamageAdjustment
			if value == 0 {
				value = -2 // backward compatibility for older persisted buffs
			}
			adjustment += value
			removed = append(removed, buff)
		case destinyDamageUpBuffID:
			value := buff.DamageAdjustment
			if value == 0 {
				value = 2 // backward compatibility for older persisted buffs
			}
			adjustment += value
			removed = append(removed, buff)
		default:
			remaining = append(remaining, buff)
		}
	}
	damage += adjustment
	if damage < 0 {
		damage = 0
	}
	return damage, remaining, removed
}

// BattlePlayerStats is the match-scoped combat summary displayed by the
// client's battle player-info window.
type BattlePlayerStats struct {
	KillCount      int32
	TotalDie       int32
	TotalDamage    int32
	TotalInjured   int32
	TreatmentScore int32
}

type BattleStatsDelta struct {
	PlayerID       int64
	KillCount      int32
	TotalDie       int32
	TotalDamage    int32
	TotalInjured   int32
	TreatmentScore int32
}

// CardState is the persistent part of model.CardInfo needed to resume a live
// battle and to send the same hand snapshot the client consumes.
type CardState struct {
	UniqueID   int32 `json:"uniqueId"`
	CardID     int32 `json:"cardId"`
	PurifyNum  int32 `json:"purifyNum"`
	IsTemp     bool  `json:"isTemp"`
	BattleCost int32 `json:"battleCost"`
}

type BuffSourceState struct {
	S  int32 `json:"s"`
	ID int32 `json:"id"`
}

// BuffState stores the protocol fields needed for Hero.Buffs snapshots and
// insert updates. Timing and the source remain explicit so reconnects retain
// the client-visible effect state.
type BuffState struct {
	UniqueID         int64             `json:"unique_id"`
	BuffID           int32             `json:"buff_id"`
	Params           map[int32]int64   `json:"params,omitempty"`
	RestoreParams    map[int32]int64   `json:"restore_params,omitempty"`
	KeepRound        int32             `json:"keepRound,omitempty"`
	DelayRound       int32             `json:"delayRound,omitempty"`
	UseTime          int32             `json:"useTime,omitempty"`
	TargetIDs        []int64           `json:"targetIds,omitempty"`
	Priority         int32             `json:"priority,omitempty"`
	NodeID           int32             `json:"node_id,omitempty"`
	Progress         int32             `json:"progress,omitempty"`
	RelationID       string            `json:"relation_id,omitempty"`
	IsSakura         bool              `json:"is_sakura,omitempty"`
	BuffIndex        int32             `json:"buffIndex,omitempty"`
	CanRangeDamage   bool              `json:"canRangeDamage,omitempty"`
	RelativeBuffUIDs map[int32]int64   `json:"relativeBuffUids,omitempty"`
	Chain            []BuffSourceState `json:"chain,omitempty"`
	Source           *BuffSourceState  `json:"source,omitempty"`
	Key              string            `json:"key,omitempty"`
	DamageAdjustment int32             `json:"damage_adjustment,omitempty"`
}

type EventAttrResult struct {
	PlayerID     int64
	OldHP        int32
	NewHP        int32
	MaxHP        int32
	OldGold      int32
	NewGold      int32
	Cards        []CardState
	CardsChanged bool
	Buffs        []BuffState
	BuffsChanged bool
	NewBuffs     []BuffState
	RemovedBuffs []BuffState
	PlaceChanged bool
	NodeID       int32
	BackNodeID   int32
	FrontNodeIDs []int32
}

type GameEventResult struct {
	EventID         int32
	Round           int32
	NextPlayer      int64
	GameProgress    int32
	GameMaxProgress int32
	ProgressChanged bool
	DiscardRequired bool
	Changes         []EventAttrResult
	LandBuffChanges []LandBuffUpdate
}

// LandBuffState represents a buff attached to a map node rather than a hero.
// Value is the server-side pickup value and is not sent as a client buff field.
type LandBuffState struct {
	NodeID int32     `json:"nodeId"`
	Value  int32     `json:"value"`
	Buff   BuffState `json:"buff"`
}

type LandBuffUpdate struct {
	NodeID int32
	Buffs  []BuffState
}

type LandBuffPickupResult struct {
	LandBuff  LandBuffState
	OldGold   int32
	NewGold   int32
	Remaining []BuffState
}

// GameActionResult is the shared transaction result for resource-backed land
// interactions such as random events and Destiny cards.
type GameActionResult = GameEventResult

type MoveCardsResult struct {
	Cards           []CardState
	Changed         bool
	DiscardRequired bool
}

type MoveGoldResult struct {
	OldGold int32
	NewGold int32
	Changed bool
}

type MoveTeleport struct {
	NodeID       int32
	FrontNodeIDs []int32
}

type MovePlaceResult struct {
	Changed      bool
	NodeID       int32
	BackNodeID   int32
	FrontNodeIDs []int32
}

// ShopState persists the current land shop so the offer and sold flags survive
// reconnects and process restarts.
type ShopState struct {
	PlayerID            int64   `json:"playerId"`
	Cards               []int32 `json:"cards"`
	Gold                int32   `json:"gold"`
	EntryGold           int32   `json:"entryGold,omitempty"`
	FreeCard            int32   `json:"freeCard,omitempty"`
	FreeCardNum         int32   `json:"freeCardNum,omitempty"`
	Alreadys            []bool  `json:"alreadys"`
	PVE                 bool    `json:"pve,omitempty"`
	DiscountGold        int32   `json:"discountGold,omitempty"`
	AssistGold          int32   `json:"assistGold,omitempty"`
	TalentSkillFreeCard []bool  `json:"talentSkillFreeCard,omitempty"`
}

type ShopBuyResult struct {
	State      ShopState
	OldGold    int32
	NewGold    int32
	Cards      []CardState
	Bought     []int32
	Closed     bool
	NextPlayer int64
	Round      int32
}

type LotteryChoiceResult struct {
	Lotterys   map[int32]bool
	NextPlayer int64
	Round      int32
}

type PursuitResult struct {
	NodeID       int32
	BackNodeID   int32
	FrontNodeIDs []int32
	Exit         bool
	NextPlayer   int64
	Round        int32
}

type TurnStartEffectResult struct {
	OldHP        int32
	NewHP        int32
	UpdatedBuffs []BuffState
	RemovedBuffs []BuffState
}

type HospitalResult struct {
	InHospital bool
	OldHP      int32
	NewHP      int32
	MaxHP      int32
	NodeID     int32
	NextPlayer int64
	Round      int32
}

type Store struct{ db *sql.DB }

type PveHeroProfile struct {
	HeroID  int32
	Level   int32
	Exp     int32
	Talents []int32
}

type InventoryItem struct {
	ItemID int32
	Count  int32
}

type GachaProgress struct {
	GachaID     int32
	PoolID      int32
	Count       int32
	RewardCount int32
}

type Player struct {
	ID             int64
	AccountID      int64
	Nick           string
	IsBot          bool
	Level          int32
	Exp            int32
	Slot           int32
	Ready          bool
	NodeID         int32
	BackNodeID     int32
	FrontNodeIDs   []int32
	Gold           int32
	HP             int32
	SpecialScore   int32
	HospitalRounds int32
	HeroLevel      int32
	RoomID         int64
	HeroID         int32
	HeroConfirmed  bool
	UseAdorn       int32
	SkinPendant    int32
	SkinConfirmed  bool
	Progress       int32
	PveLevel       int32
	PveTalentID    int32
	PveHeroes      map[int32]PveHeroProfile
	Inventory      []InventoryItem
	GachaProgress  []GachaProgress
	SignInRewards  map[int32]SignInRewardState
	SkillCooldowns map[int32]int32
	GameData       map[int32]int32
	Cards          []CardState
	UseCardNum     int32
	UseCardMaxNum  int32
	Buffs          []BuffState
	Bombs          []BombState
	Lotterys       map[int32]bool
	FashionPlans   map[int32]map[int32]int32
	UsePlan        int32
}
type Room struct {
	ID              int64
	Name            string
	Password        string
	MapID           int32
	MapIndex        int32
	MaxTime         int32
	UpgradePlan     int32
	TimePlan        int32
	Mode            int32
	LobbyID         uint64
	SpeedType       int32
	Difficulty      int32
	GameProgress    int32
	GameMaxProgress int32
	SpecialScore    int32
	SkipStory       bool
	RoomLabel       int32
	MasterID        int64
	State           int32
	CreatedAt       int64
	UpdatedAt       int64
	Players         []Player
	LandBuffs       []LandBuffState
	Battle          *BattleState
}
type RoomCreate struct {
	Name, Password                                                                string
	MapID, MaxTime, UpgradePlan, TimePlan, Mode, SpeedType, Difficulty, RoomLabel int32
	LobbyID                                                                       uint64
	SkipStory                                                                     bool
}

type RoomSettingsUpdate struct {
	Password                                string
	MapID, UpgradePlan, TimePlan, SpeedType int32
	Difficulty, RoomLabel                   int32
	SkipStory                               bool
}

type InitialPosition struct {
	NodeID       int32
	BackNodeID   int32
	FrontNodeIDs []int32
}

type FillingStationUpgrade struct {
	Star int32
	Gold int32
}

type FillingStationResult struct {
	Asymmetrical    bool
	Completed       bool
	Pending         int32
	Round           int32
	NextPlayer      int64
	HealAmount      int32
	OldHP           int32
	NewHP           int32
	MaxHP           int32
	OldGold         int32
	NewGold         int32
	OldLevel        int32
	NewLevel        int32
	Upgraded        bool
	OldSpecialScore int32
	NewSpecialScore int32
	OldGameScore    int32
	NewGameScore    int32
}

func Open(path string) (*Store, error) {
	if path == "" {
		path = "data/server.db"
	}
	db, err := sql.Open("sqlite3", path+"?_busy_timeout=5000&_foreign_keys=on&_journal_mode=WAL")
	if err != nil {
		return nil, err
	}
	db.SetMaxOpenConns(1)
	s := &Store{db: db}
	if err = s.Migrate(context.Background()); err != nil {
		db.Close()
		return nil, err
	}
	return s, nil
}
func (s *Store) Close() error { return s.db.Close() }
func (s *Store) DB() *sql.DB  { return s.db }

func (s *Store) GetSetting(ctx context.Context, key string) (string, bool, error) {
	var value string
	err := s.db.QueryRowContext(ctx, `SELECT value FROM server_settings WHERE key=?`, key).Scan(&value)
	if errors.Is(err, sql.ErrNoRows) {
		return "", false, nil
	}
	return value, err == nil, err
}

func (s *Store) SetSetting(ctx context.Context, key, value string) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO server_settings(key,value,updated_at) VALUES(?,?,?) ON CONFLICT(key) DO UPDATE SET value=excluded.value,updated_at=excluded.updated_at`, key, value, time.Now().Unix())
	return err
}

func (s *Store) SetSettings(ctx context.Context, settings map[string]string) error {
	if len(settings) == 0 {
		return nil
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	stmt, err := tx.PrepareContext(ctx, `INSERT INTO server_settings(key,value,updated_at) VALUES(?,?,?) ON CONFLICT(key) DO UPDATE SET value=excluded.value,updated_at=excluded.updated_at`)
	if err != nil {
		return err
	}
	defer stmt.Close()
	updatedAt := time.Now().Unix()
	for key, value := range settings {
		if key == "" {
			return errors.New("setting key is empty")
		}
		if _, err = stmt.ExecContext(ctx, key, value, updatedAt); err != nil {
			return err
		}
	}
	return tx.Commit()
}

func (s *Store) GetOrCreatePlayer(ctx context.Context, loginKey, platform, nick, device, token string) (Player, int64, string, error) {
	if nick == "" {
		nick = "Guest"
	}
	now := time.Now().Unix()
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Player{}, 0, "", err
	}
	defer tx.Rollback()
	_, err = tx.ExecContext(ctx, `INSERT INTO accounts(login_key,platform,nick,device_id,token,created_at,last_login_at) VALUES(?,?,?,?,?,?,?) ON CONFLICT(login_key) DO UPDATE SET platform=excluded.platform,nick=excluded.nick,device_id=excluded.device_id,token=excluded.token,last_login_at=excluded.last_login_at`, loginKey, platform, nick, device, token, now, now)
	if err != nil {
		return Player{}, 0, "", err
	}
	var accountID int64
	var storedToken string
	if err = tx.QueryRowContext(ctx, `SELECT id,token FROM accounts WHERE login_key=?`, loginKey).Scan(&accountID, &storedToken); err != nil {
		return Player{}, 0, "", err
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, accountID, nick, now); err != nil {
		return Player{}, 0, "", err
	}
	var p Player
	var lotteriesJSON, frontJSON string
	err = tx.QueryRowContext(ctx, `SELECT id,account_id,nick,level,exp,COALESCE(slot,0),COALESCE(node_id,0),COALESCE(back_node_id,0),front_node_ids_json,gold,hp,COALESCE(room_id,0),lotterys_json FROM players WHERE account_id=?`, accountID).Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &p.Slot, &p.NodeID, &p.BackNodeID, &frontJSON, &p.Gold, &p.HP, &p.RoomID, &lotteriesJSON)
	if err != nil {
		return Player{}, 0, "", err
	}
	if err = json.Unmarshal([]byte(frontJSON), &p.FrontNodeIDs); err != nil {
		return Player{}, 0, "", fmt.Errorf("decode front land IDs for player %d: %w", p.ID, err)
	}
	if err = json.Unmarshal([]byte(lotteriesJSON), &p.Lotterys); err != nil {
		return Player{}, 0, "", fmt.Errorf("decode lottery choices for player %d: %w", p.ID, err)
	}
	if p.Lotterys == nil {
		p.Lotterys = map[int32]bool{}
	}
	if err = tx.Commit(); err != nil {
		return Player{}, 0, "", err
	}
	return p, accountID, storedToken, nil
}
func (s *Store) GetOrCreateDevPlayer(ctx context.Context, loginKey, nick, device, token string) (Player, int64, string, error) {
	return s.GetOrCreatePlayer(ctx, loginKey, "dev", nick, device, token)
}

func (s *Store) CreateRoom(ctx context.Context, master Player, c RoomCreate) (Room, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, err
	}
	defer tx.Rollback()
	var current sql.NullInt64
	if err = tx.QueryRowContext(ctx, `SELECT room_id FROM players WHERE id=?`, master.ID).Scan(&current); err != nil {
		return Room{}, err
	}
	if current.Valid {
		return Room{}, errors.New("player already in room")
	}
	now := time.Now().Unix()
	res, err := tx.ExecContext(ctx, `INSERT INTO rooms(name,pwd,map_id,max_time,upgrade_plan,time_plan,mode,lobby_id,speed_type,difficulty,skip_story,room_label,master_id,state,created_at,updated_at) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`, c.Name, c.Password, c.MapID, c.MaxTime, c.UpgradePlan, c.TimePlan, c.Mode, int64(c.LobbyID), c.SpeedType, c.Difficulty, c.SkipStory, c.RoomLabel, master.ID, 1, now, now)
	if err != nil {
		return Room{}, err
	}
	roomID, err := res.LastInsertId()
	if err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,?,?,1)`, roomID, master.ID, 0, 0); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=0 WHERE id=?`, roomID, master.ID); err != nil {
		return Room{}, err
	}
	if err = tx.Commit(); err != nil {
		return Room{}, err
	}
	return s.RoomSnapshot(ctx, roomID)
}

// UpdateRoomSettings applies the fields sent by the supplied client's
// ChangeRoomC2S request. Mode and MaxTime are not populated by that client and
// remain unchanged in storage.
func (s *Store) UpdateRoomSettings(ctx context.Context, roomID, actorID int64, update RoomSettingsUpdate) (Room, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, err
	}
	defer tx.Rollback()

	var masterID int64
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT master_id,state FROM rooms WHERE id=?`, roomID).Scan(&masterID, &state); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return Room{}, errors.New("room not exist")
		}
		return Room{}, err
	}
	if masterID != actorID {
		return Room{}, errors.New("only room master can change settings")
	}
	var memberCount int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND player_id=?`, roomID, actorID).Scan(&memberCount); err != nil {
		return Room{}, err
	}
	if memberCount != 1 {
		return Room{}, errors.New("room player not exist")
	}
	if state != 1 {
		return Room{}, errors.New("room is not waiting")
	}
	result, err := tx.ExecContext(ctx, `UPDATE rooms SET pwd=?,map_id=?,upgrade_plan=?,time_plan=?,speed_type=?,difficulty=?,skip_story=?,room_label=?,updated_at=? WHERE id=? AND master_id=? AND state=1`,
		update.Password, update.MapID, update.UpgradePlan, update.TimePlan, update.SpeedType, update.Difficulty,
		update.SkipStory, update.RoomLabel, time.Now().Unix(), roomID, actorID)
	if err != nil {
		return Room{}, err
	}
	if changed, rowsErr := result.RowsAffected(); rowsErr != nil {
		return Room{}, rowsErr
	} else if changed != 1 {
		return Room{}, errors.New("room is not waiting")
	}
	if err = tx.Commit(); err != nil {
		return Room{}, err
	}
	return s.RoomSnapshot(ctx, roomID)
}

func (s *Store) JoinRoom(ctx context.Context, roomID int64, p Player, slot int32, pwd string) (Room, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, err
	}
	defer tx.Rollback()
	var state int32
	var storedPwd string
	var roomName string
	if err = tx.QueryRowContext(ctx, `SELECT state,pwd,name FROM rooms WHERE id=?`, roomID).Scan(&state, &storedPwd, &roomName); err != nil {
		return Room{}, err
	}
	if state != 1 {
		return Room{}, errors.New("room not waiting")
	}
	if storedPwd != "" && storedPwd != pwd {
		return Room{}, errors.New("room password mismatch")
	}
	var current sql.NullInt64
	if err = tx.QueryRowContext(ctx, `SELECT room_id FROM players WHERE id=?`, p.ID).Scan(&current); err != nil {
		return Room{}, err
	}
	if current.Valid && current.Int64 != roomID {
		return Room{}, errors.New("player already in room")
	}
	var count int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=?`, roomID).Scan(&count); err != nil {
		return Room{}, err
	}
	if count >= 4 {
		return Room{}, errors.New("room full")
	}
	if slot <= 0 || slot > 3 {
		for slot = 0; slot <= 3; slot++ {
			var exists int
			_ = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND slot=?`, roomID, slot).Scan(&exists)
			if exists == 0 {
				break
			}
		}
	}
	var exists int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND slot=?`, roomID, slot).Scan(&exists); err != nil {
		return Room{}, err
	}
	if exists > 0 {
		return Room{}, errors.New("slot occupied")
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,?,?,1)`, roomID, p.ID, slot, 0); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=? WHERE id=?`, roomID, slot, p.ID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM room_invites WHERE invitee_id=?`, p.ID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE rooms SET updated_at=? WHERE id=?`, time.Now().Unix(), roomID); err != nil {
		return Room{}, err
	}
	if err = tx.Commit(); err != nil {
		return Room{}, err
	}
	return s.RoomSnapshot(ctx, roomID)
}

func (s *Store) RoomSnapshot(ctx context.Context, id int64) (Room, error) {
	var r Room
	var lobby int64
	var skip int
	err := s.db.QueryRowContext(ctx, `SELECT id,name,pwd,map_id,max_time,upgrade_plan,time_plan,mode,lobby_id,speed_type,difficulty,skip_story,room_label,master_id,state,created_at,updated_at FROM rooms WHERE id=?`, id).Scan(&r.ID, &r.Name, &r.Password, &r.MapID, &r.MaxTime, &r.UpgradePlan, &r.TimePlan, &r.Mode, &lobby, &r.SpeedType, &r.Difficulty, &skip, &r.RoomLabel, &r.MasterID, &r.State, &r.CreatedAt, &r.UpdatedAt)
	if err != nil {
		return Room{}, err
	}
	r.LobbyID = uint64(lobby)
	r.SkipStory = skip != 0
	var currentRound int32
	progressErr := s.db.QueryRowContext(ctx, `SELECT round,game_progress,game_max_progress,special_score FROM game_sessions WHERE room_id=?`, id).Scan(&currentRound, &r.GameProgress, &r.GameMaxProgress, &r.SpecialScore)
	if progressErr != nil && !errors.Is(progressErr, sql.ErrNoRows) {
		return Room{}, progressErr
	}
	rows, err := s.db.QueryContext(ctx, `SELECT p.id,p.account_id,p.nick,a.platform,p.level,p.exp,rm.slot,rm.ready,p.node_id,p.back_node_id,p.front_node_ids_json,p.gold,p.hp,p.special_score,p.hospital_rounds,p.hero_level,p.room_id,rm.hero_id,rm.hero_confirmed,rm.use_adorn,rm.skin_pendant,rm.skin_confirmed,rm.progress,rm.pve_level,rm.pve_talent_id,rm.game_data_json,p.battle_cards_json,p.battle_buffs_json,p.lotterys_json FROM room_members rm JOIN players p ON p.id=rm.player_id JOIN accounts a ON a.id=p.account_id WHERE rm.room_id=? ORDER BY rm.slot`, id)
	if err != nil {
		return Room{}, err
	}
	defer rows.Close()
	for rows.Next() {
		var p Player
		p.UseCardMaxNum = 1
		var platform string
		var ready, heroConfirmed, skinConfirmed int
		var gameDataJSON, cardsJSON, buffsJSON, lotteriesJSON, frontJSON string
		if err = rows.Scan(&p.ID, &p.AccountID, &p.Nick, &platform, &p.Level, &p.Exp, &p.Slot, &ready, &p.NodeID, &p.BackNodeID, &frontJSON, &p.Gold, &p.HP, &p.SpecialScore, &p.HospitalRounds, &p.HeroLevel, &p.RoomID, &p.HeroID, &heroConfirmed, &p.UseAdorn, &p.SkinPendant, &skinConfirmed, &p.Progress, &p.PveLevel, &p.PveTalentID, &gameDataJSON, &cardsJSON, &buffsJSON, &lotteriesJSON); err != nil {
			return Room{}, err
		}
		if frontJSON != "" {
			if err = json.Unmarshal([]byte(frontJSON), &p.FrontNodeIDs); err != nil {
				return Room{}, fmt.Errorf("decode front land IDs for player %d: %w", p.ID, err)
			}
		}
		p.IsBot = platform == "bot"
		if gameDataJSON != "" {
			_ = json.Unmarshal([]byte(gameDataJSON), &p.GameData)
		}
		if cardsJSON != "" {
			_ = json.Unmarshal([]byte(cardsJSON), &p.Cards)
		}
		if buffsJSON != "" {
			_ = json.Unmarshal([]byte(buffsJSON), &p.Buffs)
		}
		if lotteriesJSON != "" {
			if err = json.Unmarshal([]byte(lotteriesJSON), &p.Lotterys); err != nil {
				return Room{}, fmt.Errorf("decode lottery choices for player %d: %w", p.ID, err)
			}
		}
		if p.Lotterys == nil {
			p.Lotterys = map[int32]bool{}
		}
		p.Ready = ready != 0
		p.HeroConfirmed = heroConfirmed != 0
		p.SkinConfirmed = skinConfirmed != 0
		r.Players = append(r.Players, p)
	}
	if err = rows.Err(); err != nil {
		return Room{}, err
	}
	if err = rows.Close(); err != nil {
		return Room{}, err
	}
	for i := range r.Players {
		if err = s.HydratePlayerGameplayData(ctx, &r.Players[i]); err != nil {
			return Room{}, err
		}
	}
	landBuffRows, err := s.db.QueryContext(ctx, `SELECT node_id,value,buff_json FROM game_land_buffs WHERE room_id=? ORDER BY node_id,created_at,unique_id`, id)
	if err != nil {
		return Room{}, err
	}
	for landBuffRows.Next() {
		var landBuff LandBuffState
		var encoded string
		if err = landBuffRows.Scan(&landBuff.NodeID, &landBuff.Value, &encoded); err != nil {
			landBuffRows.Close()
			return Room{}, err
		}
		if err = json.Unmarshal([]byte(encoded), &landBuff.Buff); err != nil {
			landBuffRows.Close()
			return Room{}, fmt.Errorf("decode land buff for room %d node %d: %w", id, landBuff.NodeID, err)
		}
		r.LandBuffs = append(r.LandBuffs, landBuff)
	}
	if err = landBuffRows.Err(); err != nil {
		landBuffRows.Close()
		return Room{}, err
	}
	if err = landBuffRows.Close(); err != nil {
		return Room{}, err
	}
	bombRows, err := s.db.QueryContext(ctx, `SELECT owner_player_id,holder_player_id,card_id,is_open FROM game_bombs WHERE room_id=? ORDER BY id`, id)
	if err != nil {
		return Room{}, err
	}
	for bombRows.Next() {
		var bomb BombState
		var isOpen int
		if err = bombRows.Scan(&bomb.OwnerPlayerID, &bomb.PlayerID, &bomb.CardID, &isOpen); err != nil {
			bombRows.Close()
			return Room{}, err
		}
		bomb.IsOpen = isOpen != 0
		for index := range r.Players {
			if r.Players[index].ID == bomb.PlayerID {
				r.Players[index].Bombs = append(r.Players[index].Bombs, bomb)
				break
			}
		}
	}
	if err = bombRows.Err(); err != nil {
		bombRows.Close()
		return Room{}, err
	}
	if err = bombRows.Close(); err != nil {
		return Room{}, err
	}
	if currentRound > 0 {
		cardRows, queryErr := s.db.QueryContext(ctx, `SELECT player_id,use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND round=?`, id, currentRound)
		if queryErr != nil {
			return Room{}, queryErr
		}
		for cardRows.Next() {
			var playerID int64
			var used, max int32
			if queryErr = cardRows.Scan(&playerID, &used, &max); queryErr != nil {
				cardRows.Close()
				return Room{}, queryErr
			}
			for index := range r.Players {
				if r.Players[index].ID == playerID {
					r.Players[index].UseCardNum = used
					r.Players[index].UseCardMaxNum = max
					break
				}
			}
		}
		if queryErr = cardRows.Err(); queryErr != nil {
			cardRows.Close()
			return Room{}, queryErr
		}
		if queryErr = cardRows.Close(); queryErr != nil {
			return Room{}, queryErr
		}
		cooldownRows, queryErr := s.db.QueryContext(ctx, `SELECT player_id,skill_id,ready_round FROM game_skill_cooldowns WHERE room_id=? AND ready_round>?`, id, currentRound)
		if queryErr != nil {
			return Room{}, queryErr
		}
		for cooldownRows.Next() {
			var playerID int64
			var skillID, readyRound int32
			if queryErr = cooldownRows.Scan(&playerID, &skillID, &readyRound); queryErr != nil {
				cooldownRows.Close()
				return Room{}, queryErr
			}
			for index := range r.Players {
				if r.Players[index].ID == playerID {
					if r.Players[index].SkillCooldowns == nil {
						r.Players[index].SkillCooldowns = make(map[int32]int32)
					}
					r.Players[index].SkillCooldowns[skillID] = readyRound - currentRound
					break
				}
			}
		}
		if queryErr = cooldownRows.Err(); queryErr != nil {
			cooldownRows.Close()
			return Room{}, queryErr
		}
		cooldownRows.Close()
	}
	var battleJSON string
	err = s.db.QueryRowContext(ctx, `SELECT state_json FROM game_battles WHERE room_id=?`, id).Scan(&battleJSON)
	if err != nil && !errors.Is(err, sql.ErrNoRows) {
		return Room{}, err
	}
	if battleJSON != "" {
		var battle BattleState
		if err = json.Unmarshal([]byte(battleJSON), &battle); err != nil {
			return Room{}, fmt.Errorf("decode battle state for room %d: %w", id, err)
		}
		r.Battle = &battle
	}
	return r, nil
}
func (s *Store) ListRooms(ctx context.Context, limit int) ([]Room, error) {
	return s.listRooms(ctx, nil, limit)
}

// ListRoomsByMode returns waiting public rooms for one configured client mode.
// Room-list state is keyed by MapModeType in the client, so mixing modes in
// this response can render entries with the wrong player limits and settings.
func (s *Store) ListRoomsByMode(ctx context.Context, mapMode int32, limit int) ([]Room, error) {
	return s.listRooms(ctx, &mapMode, limit)
}

func (s *Store) listRooms(ctx context.Context, mapMode *int32, limit int) ([]Room, error) {
	if limit <= 0 || limit > 100 {
		limit = 50
	}
	query := `SELECT id FROM rooms WHERE state=1`
	args := make([]any, 0, 2)
	if mapMode != nil {
		query += ` AND mode=?`
		args = append(args, *mapMode)
	}
	query += ` ORDER BY updated_at DESC LIMIT ?`
	args = append(args, limit)
	rows, err := s.db.QueryContext(ctx, query, args...)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []Room
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		r, e := s.RoomSnapshot(ctx, id)
		if e == nil {
			out = append(out, r)
		}
	}
	return out, rows.Err()
}
func (s *Store) SetReady(ctx context.Context, roomID, playerID int64, ready bool) (Room, error) {
	var state int32
	if err := s.db.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return Room{}, err
	}
	if state != 1 {
		return Room{}, errors.New("room is not waiting")
	}
	v := 0
	if ready {
		v = 1
	}
	res, err := s.db.ExecContext(ctx, `UPDATE room_members SET ready=? WHERE room_id=? AND player_id=?`, v, roomID, playerID)
	if err != nil {
		return Room{}, err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return Room{}, errors.New("player not in room")
	}
	_, err = s.db.ExecContext(ctx, `UPDATE rooms SET updated_at=? WHERE id=?`, time.Now().Unix(), roomID)
	if err != nil {
		return Room{}, err
	}
	return s.RoomSnapshot(ctx, roomID)
}

func (s *Store) TransferRoomMaster(ctx context.Context, roomID, actorID, targetID int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var master int64
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT master_id,state FROM rooms WHERE id=?`, roomID).Scan(&master, &state); err != nil {
		return err
	}
	if state != 1 {
		return errors.New("room is not waiting")
	}
	if master != actorID || targetID == actorID {
		return errors.New("only room master can transfer")
	}
	var n int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND player_id=?`, roomID, targetID).Scan(&n); err != nil {
		return err
	}
	if n == 0 {
		return errors.New("target player not in room")
	}
	if _, err = tx.ExecContext(ctx, `UPDATE rooms SET master_id=?,updated_at=? WHERE id=?`, targetID, time.Now().Unix(), roomID); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE room_members SET ready=0 WHERE room_id=? AND player_id=?`, roomID, targetID); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) KickRoomPlayer(ctx context.Context, roomID, actorID, targetID int64) (Room, bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, false, err
	}
	defer tx.Rollback()
	var master int64
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT master_id,state FROM rooms WHERE id=?`, roomID).Scan(&master, &state); err != nil {
		return Room{}, false, err
	}
	if state != 1 {
		return Room{}, false, errors.New("room is not waiting")
	}
	if master != actorID || actorID == targetID {
		return Room{}, false, errors.New("only room master can kick another player")
	}
	res, err := tx.ExecContext(ctx, `DELETE FROM room_members WHERE room_id=? AND player_id=?`, roomID, targetID)
	if err != nil {
		return Room{}, false, err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return Room{}, false, errors.New("target player not in room")
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=NULL,slot=0 WHERE id=?`, targetID); err != nil {
		return Room{}, false, err
	}
	var remain int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=?`, roomID).Scan(&remain); err != nil {
		return Room{}, false, err
	}
	dissolved := remain == 0
	if dissolved {
		if _, err = tx.ExecContext(ctx, `DELETE FROM rooms WHERE id=?`, roomID); err != nil {
			return Room{}, false, err
		}
	} else if _, err = tx.ExecContext(ctx, `UPDATE rooms SET updated_at=? WHERE id=?`, time.Now().Unix(), roomID); err != nil {
		return Room{}, false, err
	}
	if err = tx.Commit(); err != nil {
		return Room{}, false, err
	}
	if dissolved {
		return Room{}, true, nil
	}
	room, err := s.RoomSnapshot(ctx, roomID)
	return room, false, err
}

func (s *Store) HasInventoryItem(ctx context.Context, playerID int64, itemID int32) (bool, error) {
	var n int
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM inventory WHERE player_id=? AND item_id=? AND count>0`, playerID, itemID).Scan(&n)
	return n > 0, err
}
func (s *Store) StartRoom(ctx context.Context, roomID, playerID int64, addBots bool) (Room, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, err
	}
	defer tx.Rollback()
	var master int64
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT master_id,state FROM rooms WHERE id=?`, roomID).Scan(&master, &state); err != nil {
		return Room{}, err
	}
	if master != playerID {
		return Room{}, errors.New("only room master can start")
	}
	if state != 1 {
		return Room{}, errors.New("room is not waiting")
	}
	var members, notReady int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*),COALESCE(SUM(CASE WHEN player_id<>? AND ready=0 THEN 1 ELSE 0 END),0) FROM room_members WHERE room_id=?`, playerID, roomID).Scan(&members, &notReady); err != nil {
		return Room{}, err
	}
	if members < 2 && !addBots {
		return Room{}, errors.New("room player too little")
	}
	if addBots {
		// Public rooms have four seats. Persistent bot identities let clients use
		// the normal room, hero, and asset-synchronization messages.
		for slot := int32(0); slot < 4; slot++ {
			var occupied int
			if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND slot=?`, roomID, slot).Scan(&occupied); err != nil {
				return Room{}, err
			}
			if occupied != 0 {
				continue
			}
			loginKey := fmt.Sprintf("bot:room:%d:slot:%d", roomID, slot)
			nick := fmt.Sprintf("Starling %02d", slot+1)
			now := time.Now().Unix()
			if _, err = tx.ExecContext(ctx, `INSERT INTO accounts(login_key,platform,nick,device_id,created_at,last_login_at) VALUES(?,?,?,?,?,?) ON CONFLICT(login_key) DO UPDATE SET platform='bot',nick=excluded.nick`, loginKey, "bot", nick, "server-bot", now, now); err != nil {
				return Room{}, err
			}
			var accountID, botID int64
			if err = tx.QueryRowContext(ctx, `SELECT id FROM accounts WHERE login_key=?`, loginKey).Scan(&accountID); err != nil {
				return Room{}, err
			}
			if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, accountID, nick, now); err != nil {
				return Room{}, err
			}
			if err = tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, accountID).Scan(&botID); err != nil {
				return Room{}, err
			}
			if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,?,1,1)`, roomID, botID, slot); err != nil {
				return Room{}, err
			}
			if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=?,node_id=0,back_node_id=0,gold=0,hp=10 WHERE id=?`, roomID, slot, botID); err != nil {
				return Room{}, err
			}
			members++
		}
	}
	if notReady > 0 {
		return Room{}, errors.New("room player not ready")
	}
	now := time.Now().Unix()
	if _, err = tx.ExecContext(ctx, `UPDATE rooms SET state=10,updated_at=? WHERE id=?`, now, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE room_members SET hero_id=0,hero_confirmed=0,pve_level=1,pve_talent_id=0,game_data_json='{}',use_adorn=0,skin_pendant=0,skin_confirmed=0,progress=0 WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_sessions WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_shops WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_divinations WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_skill_event_offers WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_skill_cooldowns WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_card_turns WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_bombs WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_land_buffs WHERE room_id=?`, roomID); err != nil {
		return Room{}, err
	}
	if err = tx.Commit(); err != nil {
		return Room{}, err
	}
	return s.RoomSnapshot(ctx, roomID)
}

// SetRoomHero stores the current preview. Confirmation is a separate protocol call.
func (s *Store) SetRoomHero(ctx context.Context, roomID, playerID int64, heroID, pveLevel, pveTalentID, useAdorn, skinPendant int32, gameData map[int32]int32, maxHP int32) error {
	if maxHP <= 0 {
		return errors.New("maximum HP must be positive")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return err
	}
	if state != 10 {
		return errors.New("room is not choosing hero")
	}
	if pveLevel <= 0 {
		pveLevel = 1
	}
	gameDataBytes, err := json.Marshal(gameData)
	if err != nil {
		return err
	}
	res, err := tx.ExecContext(ctx, `UPDATE room_members SET hero_id=?,hero_confirmed=0,pve_level=?,pve_talent_id=?,game_data_json=?,use_adorn=?,skin_pendant=?,skin_confirmed=0 WHERE room_id=? AND player_id=? AND hero_confirmed=0`, heroID, pveLevel, pveTalentID, string(gameDataBytes), useAdorn, skinPendant, roomID, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return errors.New("room hero not choosable")
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,hero_level=0 WHERE id=? AND room_id=?`, maxHP, playerID, roomID); err != nil {
		return err
	}
	return tx.Commit()
}

// ConfirmRoomHero returns conflict=true when another player has already locked the hero.
func (s *Store) ConfirmRoomHero(ctx context.Context, roomID, playerID int64) (room Room, conflict, allConfirmed bool, err error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, false, false, err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return Room{}, false, false, err
	}
	if state != 10 {
		return Room{}, false, false, errors.New("room is not choosing hero")
	}
	var heroID int32
	var confirmed int
	if err = tx.QueryRowContext(ctx, `SELECT hero_id,hero_confirmed FROM room_members WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&heroID, &confirmed); err != nil {
		return Room{}, false, false, errors.New("player not in room")
	}
	if heroID <= 0 {
		return Room{}, false, false, errors.New("hero not selected")
	}
	if confirmed != 0 {
		return Room{}, false, false, errors.New("hero already confirmed")
	}
	var used int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND player_id<>? AND hero_id=? AND hero_confirmed=1`, roomID, playerID, heroID).Scan(&used); err != nil {
		return Room{}, false, false, err
	}
	if used > 0 {
		return Room{}, true, false, nil
	}
	if _, err = tx.ExecContext(ctx, `UPDATE room_members SET hero_confirmed=1 WHERE room_id=? AND player_id=?`, roomID, playerID); err != nil {
		return Room{}, false, false, err
	}
	var pending int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND hero_confirmed=0`, roomID).Scan(&pending); err != nil {
		return Room{}, false, false, err
	}
	allConfirmed = pending == 0
	if allConfirmed {
		if _, err = tx.ExecContext(ctx, `UPDATE rooms SET state=15,updated_at=? WHERE id=?`, time.Now().Unix(), roomID); err != nil {
			return Room{}, false, false, err
		}
	}
	if err = tx.Commit(); err != nil {
		return Room{}, false, false, err
	}
	room, err = s.RoomSnapshot(ctx, roomID)
	return room, false, allConfirmed, err
}

// ChooseRoomSkin stores previews and confirmations; ready1 is entered only after all players confirm.
func (s *Store) ChooseRoomSkin(ctx context.Context, roomID, playerID int64, heroID, useAdorn, pendant int32, affirm bool) (room Room, allConfirmed bool, err error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, false, err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return Room{}, false, err
	}
	if state != 15 {
		return Room{}, false, errors.New("room is not choosing skin")
	}
	var selected int32
	var currentAdorn, currentPendant int32
	var heroConfirmed, skinConfirmed int
	if err = tx.QueryRowContext(ctx, `SELECT hero_id,hero_confirmed,skin_confirmed,use_adorn,skin_pendant FROM room_members WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&selected, &heroConfirmed, &skinConfirmed, &currentAdorn, &currentPendant); err != nil {
		return Room{}, false, errors.New("player not in room")
	}
	if heroConfirmed == 0 {
		return Room{}, false, errors.New("hero not confirmed")
	}
	if selected != heroID {
		return Room{}, false, errors.New("skin hero mismatch")
	}
	confirmed := 0
	if affirm {
		confirmed = 1
	}
	if skinConfirmed != 0 && !affirm {
		return Room{}, false, errors.New("skin already confirmed")
	}
	if affirm && useAdorn == 0 {
		useAdorn = currentAdorn
	}
	if pendant == 0 {
		pendant = currentPendant
	}
	if _, err = tx.ExecContext(ctx, `UPDATE room_members SET use_adorn=?,skin_pendant=?,skin_confirmed=? WHERE room_id=? AND player_id=?`, useAdorn, pendant, confirmed, roomID, playerID); err != nil {
		return Room{}, false, err
	}
	var pending int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND skin_confirmed=0`, roomID).Scan(&pending); err != nil {
		return Room{}, false, err
	}
	allConfirmed = pending == 0
	if allConfirmed {
		if _, err = tx.ExecContext(ctx, `UPDATE rooms SET state=20,updated_at=? WHERE id=?`, time.Now().Unix(), roomID); err != nil {
			return Room{}, false, err
		}
	}
	if err = tx.Commit(); err != nil {
		return Room{}, false, err
	}
	room, err = s.RoomSnapshot(ctx, roomID)
	return room, allConfirmed, err
}

// SetAssetProgress returns started=true exactly once, when every room member reaches 100%.
func (s *Store) SetAssetProgress(ctx context.Context, roomID, playerID int64, progress, gameMaxProgress int32, positions map[int64]InitialPosition, maxHPs map[int64]int32, initialGold map[int64]int32, initialCards map[int64][]CardState) (room Room, started bool, err error) {
	if progress < 0 {
		progress = 0
	} else if progress > 100 {
		progress = 100
	}
	if gameMaxProgress < 0 {
		gameMaxProgress = 0
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Room{}, false, err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return Room{}, false, err
	}
	if state < 20 {
		return Room{}, false, errors.New("room is not loading battle assets")
	}
	res, err := tx.ExecContext(ctx, `UPDATE room_members SET progress=? WHERE room_id=? AND player_id=?`, progress, roomID, playerID)
	if err != nil {
		return Room{}, false, err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return Room{}, false, errors.New("player not in room")
	}
	var pending int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND progress<100`, roomID).Scan(&pending); err != nil {
		return Room{}, false, err
	}
	if pending == 0 && state < 25 {
		var current int32
		if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&current); err != nil {
			return Room{}, false, err
		}
		if current < 25 {
			if _, err = tx.ExecContext(ctx, `DELETE FROM game_player_stats WHERE room_id=?`, roomID); err != nil {
				return Room{}, false, err
			}
			members, e := tx.QueryContext(ctx, `SELECT rm.player_id,a.platform FROM room_members rm JOIN players p ON p.id=rm.player_id JOIN accounts a ON a.id=p.account_id WHERE rm.room_id=? ORDER BY rm.slot`, roomID)
			if e != nil {
				return Room{}, false, e
			}
			memberIDs := make([]int64, 0, len(positions))
			humanIDs := make([]int64, 0, len(positions))
			for members.Next() {
				var id int64
				var platform string
				if e = members.Scan(&id, &platform); e != nil {
					members.Close()
					return Room{}, false, e
				}
				memberIDs = append(memberIDs, id)
				if platform != "bot" {
					humanIDs = append(humanIDs, id)
				}
			}
			if e = members.Err(); e != nil {
				members.Close()
				return Room{}, false, e
			}
			members.Close()
			if len(memberIDs) == 0 || len(memberIDs) != len(positions) {
				return Room{}, false, errors.New("missing board spawn positions")
			}
			for _, id := range memberIDs {
				pos, ok := positions[id]
				if !ok {
					return Room{}, false, errors.New("missing board spawn position")
				}
				maxHP, ok := maxHPs[id]
				if !ok || maxHP <= 0 {
					return Room{}, false, errors.New("missing hero maximum HP")
				}
				gold, ok := initialGold[id]
				if !ok || gold < 0 {
					return Room{}, false, errors.New("missing or invalid initial gold")
				}
				cards, ok := initialCards[id]
				if !ok {
					return Room{}, false, errors.New("missing initial battle cards")
				}
				cardsJSON, marshalErr := json.Marshal(cards)
				if marshalErr != nil {
					return Room{}, false, fmt.Errorf("encode initial cards for player %d: %w", id, marshalErr)
				}
				frontJSON, marshalErr := json.Marshal(pos.FrontNodeIDs)
				if marshalErr != nil {
					return Room{}, false, fmt.Errorf("encode initial exits for player %d: %w", id, marshalErr)
				}
				if _, err = tx.ExecContext(ctx, `UPDATE players SET node_id=?,back_node_id=?,front_node_ids_json=?,gold=?,hp=?,special_score=0,hospital_rounds=0,hero_level=0,battle_cards_json=?,battle_buffs_json='[]',lotterys_json='{}' WHERE id=? AND room_id=?`, pos.NodeID, pos.BackNodeID, string(frontJSON), gold, maxHP, string(cardsJSON), id, roomID); err != nil {
					return Room{}, false, err
				}
			}
			var firstPlayer int64
			if err = tx.QueryRowContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot LIMIT 1`, roomID).Scan(&firstPlayer); err != nil {
				return Room{}, false, err
			}
			now := time.Now().Unix()
			if _, err = tx.ExecContext(ctx, `UPDATE rooms SET state=25,updated_at=? WHERE id=? AND state<25`, now, roomID); err != nil {
				return Room{}, false, err
			}
			if _, err = tx.ExecContext(ctx, `DELETE FROM room_invites WHERE room_id=?`, roomID); err != nil {
				return Room{}, false, err
			}
			if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_sessions(room_id,round,current_player_id,pending_move,turn_phase,status,game_progress,game_max_progress,started_at) VALUES(?,1,?,0,?,'running',0,?,?)`, roomID, firstPlayer, TurnPhaseThrowDice, gameMaxProgress, now); err != nil {
				return Room{}, false, err
			}
			if _, err = tx.ExecContext(ctx, `UPDATE game_sessions SET special_score=0 WHERE room_id=?`, roomID); err != nil {
				return Room{}, false, err
			}
			if err = recordRecentPlayers(ctx, tx, humanIDs, now); err != nil {
				return Room{}, false, err
			}
			started = true
		}
	}
	if err = tx.Commit(); err != nil {
		return Room{}, false, err
	}
	room, err = s.RoomSnapshot(ctx, roomID)
	return room, started, err
}

// ConfigureRoomBots gives each bot a shipped starter hero and marks the bot's
// cosmetic and asset-loading phases complete. Humans still use the regular
// choice, confirmation, and loading RPCs.
func (s *Store) ConfigureRoomBots(ctx context.Context, roomID int64, heroIDs []int32, heroMaxHP map[int32]int32) error {
	if len(heroIDs) == 0 {
		return errors.New("no starter heroes configured for bots")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	rows, err := tx.QueryContext(ctx, `SELECT rm.player_id FROM room_members rm JOIN players p ON p.id=rm.player_id JOIN accounts a ON a.id=p.account_id WHERE rm.room_id=? AND a.platform='bot' ORDER BY rm.slot`, roomID)
	if err != nil {
		return err
	}
	var bots []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			rows.Close()
			return err
		}
		bots = append(bots, id)
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return err
	}
	rows.Close()
	for i, id := range bots {
		heroID := heroIDs[i%len(heroIDs)]
		maxHP, ok := heroMaxHP[heroID]
		if !ok || maxHP <= 0 {
			return errors.New("bot hero maximum HP is missing")
		}
		if _, err = tx.ExecContext(ctx, `UPDATE room_members SET ready=1,hero_id=?,hero_confirmed=1,skin_confirmed=1,progress=100,pve_level=1,pve_talent_id=0,game_data_json='{}' WHERE room_id=? AND player_id=?`, heroID, roomID, id); err != nil {
			return err
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,hero_level=0 WHERE id=? AND room_id=?`, maxHP, id, roomID); err != nil {
			return err
		}
	}
	return tx.Commit()
}

func (s *Store) ExitRoom(ctx context.Context, roomID, playerID int64) (int64, bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, false, err
	}
	defer tx.Rollback()
	var master int64
	var gameState int32
	if err = tx.QueryRowContext(ctx, `SELECT master_id,state FROM rooms WHERE id=?`, roomID).Scan(&master, &gameState); err != nil {
		return 0, false, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM room_members WHERE room_id=? AND player_id=?`, roomID, playerID); err != nil {
		return 0, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=NULL,slot=0 WHERE id=?`, playerID); err != nil {
		return 0, false, err
	}
	var remain int
	var nextMaster int64
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*),COALESCE(MIN(player_id),0) FROM room_members WHERE room_id=?`, roomID).Scan(&remain, &nextMaster); err != nil {
		return 0, false, err
	}
	dissolve := remain == 0
	if dissolve {
		_, err = tx.ExecContext(ctx, `DELETE FROM rooms WHERE id=?`, roomID)
	} else if master == playerID {
		_, err = tx.ExecContext(ctx, `UPDATE rooms SET master_id=?,updated_at=? WHERE id=?`, nextMaster, time.Now().Unix(), roomID)
	}
	if err != nil {
		return 0, false, err
	}
	if err = tx.Commit(); err != nil {
		return 0, false, err
	}
	return nextMaster, dissolve, nil
}
func (s *Store) PlayerForAccount(ctx context.Context, accountID int64) (Player, error) {
	var p Player
	var lotteriesJSON, frontJSON string
	err := s.db.QueryRowContext(ctx, `SELECT id,account_id,nick,level,exp,COALESCE(slot,0),COALESCE(node_id,0),COALESCE(back_node_id,0),front_node_ids_json,gold,hp,COALESCE(room_id,0),lotterys_json FROM players WHERE account_id=?`, accountID).Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &p.Slot, &p.NodeID, &p.BackNodeID, &frontJSON, &p.Gold, &p.HP, &p.RoomID, &lotteriesJSON)
	if err == nil {
		err = json.Unmarshal([]byte(frontJSON), &p.FrontNodeIDs)
	}
	if err == nil {
		err = json.Unmarshal([]byte(lotteriesJSON), &p.Lotterys)
	}
	if err == nil && p.Lotterys == nil {
		p.Lotterys = map[int32]bool{}
	}
	if err == nil {
		err = s.HydratePlayerGameplayData(ctx, &p)
	}
	return p, err
}
func (s *Store) PlayerInRoom(ctx context.Context, roomID, playerID int64) (bool, error) {
	var n int
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&n)
	return n > 0, err
}
func (s *Store) RoomForPlayer(ctx context.Context, playerID int64) (int64, error) {
	var id sql.NullInt64
	err := s.db.QueryRowContext(ctx, `SELECT room_id FROM players WHERE id=?`, playerID).Scan(&id)
	if err != nil {
		return 0, err
	}
	if !id.Valid {
		return 0, fmt.Errorf("player not in room")
	}
	return id.Int64, nil
}

func (s *Store) CurrentRoomID(ctx context.Context, playerID int64) (int64, error) {
	var roomID int64
	err := s.db.QueryRowContext(ctx, `SELECT COALESCE(room_id,0) FROM players WHERE id=?`, playerID).Scan(&roomID)
	return roomID, err
}

// ClearStaleRoomReference clears a player's persisted room pointer only when it
// still points at the expected room and the player is no longer a member. The
// membership guard prevents a concurrent room join from being overwritten.
func (s *Store) ClearStaleRoomReference(ctx context.Context, playerID, roomID int64) (bool, error) {
	result, err := s.db.ExecContext(ctx, `UPDATE players SET room_id=NULL,slot=0
		WHERE id=? AND room_id=?
		AND NOT EXISTS (SELECT 1 FROM room_members WHERE room_id=? AND player_id=?)`,
		playerID, roomID, roomID, playerID)
	if err != nil {
		return false, err
	}
	changed, err := result.RowsAffected()
	if err != nil {
		return false, err
	}
	return changed == 1, nil
}

func (s *Store) SetPosition(ctx context.Context, roomID, playerID int64, node int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE players SET node_id=? WHERE id=? AND room_id=?`, node, playerID, roomID)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("player not in room")
	}
	return nil
}
func (s *Store) AddAction(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte) (bool, error) {
	if upsn <= 0 {
		return true, nil
	}
	res, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
	if err != nil {
		return false, err
	}
	n, err := res.RowsAffected()
	return n > 0, err
}

// BeginMoveRoll validates the dice count against active one-shot battle buffs,
// consumes such a buff, records the action, grants movement points and applies
// configured gold/card rewards in one transaction. A retry cannot replace an
// active roll or consume the buff twice.
func (s *Store) BeginMoveRoll(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, dice []int32, requiredPhase string, goldBonus int32, rewardCards []CardState, goldResult *MoveGoldResult, cardResult *MoveCardsResult) (bool, *BuffState, error) {
	return s.BeginMoveRollWithPoint(ctx, roomID, playerID, cmd, upsn, payload, dice, 0, requiredPhase, goldBonus, rewardCards, goldResult, cardResult)
}

// BeginMoveRollWithPoint uses an optional, server-persisted controlled point
// while preserving the real dice values for the client's dice animation.
func (s *Store) BeginMoveRollWithPoint(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, dice []int32, movePointOverride int32, requiredPhase string, goldBonus int32, rewardCards []CardState, goldResult *MoveGoldResult, cardResult *MoveCardsResult) (bool, *BuffState, error) {
	if len(dice) < 1 || len(dice) > 2 {
		return false, nil, errors.New("dice count must be one or two")
	}
	if movePointOverride < 0 || movePointOverride > 6 || (movePointOverride > 0 && requiredPhase != TurnPhaseThrowDice) {
		return false, nil, errors.New("controlled movement point is invalid")
	}
	if len(rewardCards) > 0 && cardResult == nil {
		return false, nil, errors.New("card result is required when a movement reward is granted")
	}
	points := int32(0)
	for _, value := range dice {
		if value < 1 || value > 6 {
			return false, nil, errors.New("dice value must be between one and six")
		}
		points += value
	}
	controlled := movePointOverride > 0
	if controlled {
		points = movePointOverride
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return false, nil, err
	}
	defer tx.Rollback()

	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return false, nil, err
	}
	if currentPlayer != playerID {
		return false, nil, ErrTurnPlayerMismatch
	}
	if pending > 0 {
		return false, nil, ErrMoveAlreadyRolled
	}
	if phase != requiredPhase {
		return false, nil, ErrActionNotReady
	}
	if requiredPhase != TurnPhaseThrowDice && requiredPhase != TurnPhaseMoveAgain {
		return false, nil, ErrActionNotReady
	}
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return false, nil, e
		}
		created, e := res.RowsAffected()
		if e != nil {
			return false, nil, e
		}
		if created == 0 {
			return false, nil, nil
		}
	}
	var consumedBuff *BuffState
	if requiredPhase == TurnPhaseThrowDice {
		var storedPoint int32
		controlErr := tx.QueryRowContext(ctx, `SELECT selected_point FROM game_control_moves WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, currentRound).Scan(&storedPoint)
		if controlErr != nil && !errors.Is(controlErr, sql.ErrNoRows) {
			return false, nil, controlErr
		}
		if controlled {
			if controlErr != nil || storedPoint != movePointOverride {
				return false, nil, ErrActionNotReady
			}
		} else if controlErr == nil {
			return false, nil, ErrActionNotReady
		}
		var buffsJSON string
		if err = tx.QueryRowContext(ctx, `SELECT battle_buffs_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&buffsJSON); err != nil {
			return false, nil, err
		}
		var buffs []BuffState
		if buffsJSON != "" {
			if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
				return false, nil, fmt.Errorf("decode player battle buffs: %w", err)
			}
		}
		buffIndex := -1
		for i := range buffs {
			if buffs[i].BuffID == 3000201 {
				buffIndex = i
				break
			}
		}
		expectedDice := 1
		if buffIndex >= 0 {
			expectedDice = 2
		}
		if len(dice) != expectedDice {
			return false, nil, ErrActionNotReady
		}
		if buffIndex >= 0 {
			removed := buffs[buffIndex]
			consumedBuff = &removed
			buffs = append(buffs[:buffIndex], buffs[buffIndex+1:]...)
			encoded, marshalErr := json.Marshal(buffs)
			if marshalErr != nil {
				return false, nil, marshalErr
			}
			if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_buffs_json=? WHERE id=? AND room_id=?`, string(encoded), playerID, roomID); err != nil {
				return false, nil, err
			}
		}
	} else if len(dice) != 1 {
		return false, nil, ErrActionNotReady
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=?,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, points, TurnPhaseMoving, roomID, playerID, requiredPhase)
	if err != nil {
		return false, nil, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return false, nil, err
	}
	if updated == 0 {
		return false, nil, ErrMoveAlreadyRolled
	}
	if controlled {
		res, deleteErr := tx.ExecContext(ctx, `DELETE FROM game_control_moves WHERE room_id=? AND player_id=? AND round=? AND selected_point=?`, roomID, playerID, currentRound, movePointOverride)
		if deleteErr != nil {
			return false, nil, deleteErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return false, nil, changeErr
			}
			return false, nil, ErrActionNotReady
		}
	}
	if goldBonus > 0 {
		var oldGold int32
		if err = tx.QueryRowContext(ctx, `SELECT gold FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldGold); err != nil {
			return false, nil, err
		}
		maxGold := int32(1<<31 - 1)
		if goldBonus > maxGold-oldGold {
			goldBonus = maxGold - oldGold
		}
		if goldBonus > 0 {
			newGold := oldGold + goldBonus
			if _, err = tx.ExecContext(ctx, `UPDATE players SET gold=? WHERE id=? AND room_id=?`, newGold, playerID, roomID); err != nil {
				return false, nil, err
			}
			if goldResult != nil {
				goldResult.OldGold = oldGold
				goldResult.NewGold = newGold
				goldResult.Changed = true
			}
		}
	}
	if len(rewardCards) > 0 {
		var cardsJSON string
		if err = tx.QueryRowContext(ctx, `SELECT battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&cardsJSON); err != nil {
			return false, nil, err
		}
		var cards []CardState
		if cardsJSON != "" {
			if err = json.Unmarshal([]byte(cardsJSON), &cards); err != nil {
				return false, nil, fmt.Errorf("decode player cards for movement reward: %w", err)
			}
		}
		for _, card := range rewardCards {
			if card.UniqueID <= 0 || card.CardID <= 0 {
				return false, nil, errors.New("movement reward contains an invalid card")
			}
			cards = append(cards, card)
		}
		encoded, marshalErr := json.Marshal(cards)
		if marshalErr != nil {
			return false, nil, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encoded), playerID, roomID); err != nil {
			return false, nil, err
		}
		cardResult.Cards = append([]CardState(nil), cards...)
		cardResult.Changed = true
	}
	if err = tx.Commit(); err != nil {
		return false, nil, err
	}
	return true, consumedBuff, nil
}

// CommitMoveStep atomically stores one client-selected land, decrements the
// remaining movement points and advances the turn when the move ends.
func (s *Store) CommitMoveStep(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, fromNode, targetNode int32, end, moveAgain, rollGold, landHeal, landBloodLoss, landHospital, landDivination, landGamble, fillingStation, landEvent, landDestiny, landLottery, landPursuit, landBattery, landShop, landPveShop bool, shop *ShopState, divinationChoices []int32, gamble *GambleState, battle *BattleState, frontNodeIDs []int32, teleport *MoveTeleport, cardsToDraw []CardState, handLimit int32, cardResult *MoveCardsResult, placeResult *MovePlaceResult, goldResult *MoveGoldResult, pickupResult *LandBuffPickupResult) (nextPlayer int64, round int32, created bool, err error) {
	if (landHospital || landDivination || landGamble || landBattery) && !end {
		return 0, 0, false, errors.New("land action requires a completed move")
	}
	if landDivination {
		if len(divinationChoices) != 2 || divinationChoices[0] <= 0 || divinationChoices[1] <= 0 || divinationChoices[0] == divinationChoices[1] {
			return 0, 0, false, errors.New("Divination offer must contain two distinct IDs")
		}
	} else if len(divinationChoices) > 0 {
		return 0, 0, false, errors.New("Divination choices supplied for a non-Divination move")
	}
	if landGamble {
		if gamble == nil || gamble.LandID < 0 || gamble.BaseGold < 0 || gamble.BetGold <= 0 || len(gamble.Roles) == 0 {
			return 0, 0, false, errors.New("Gamble hall is invalid")
		}
		eligible := false
		seen := make(map[int64]struct{}, len(gamble.Roles))
		for _, role := range gamble.Roles {
			if role.PlayerID <= 0 {
				return 0, 0, false, errors.New("Gamble role has an invalid player")
			}
			if _, exists := seen[role.PlayerID]; exists {
				return 0, 0, false, errors.New("Gamble role player is duplicated")
			}
			seen[role.PlayerID] = struct{}{}
			eligible = eligible || !role.IsDie && !role.GoldLack
		}
		if !eligible {
			return 0, 0, false, errors.New("Gamble hall has no eligible player")
		}
	} else if gamble != nil {
		return 0, 0, false, errors.New("Gamble hall supplied for a non-Gamble move")
	}
	if battle != nil {
		if !end || battle.BattleID <= 0 || battle.Stage != TurnPhaseBattleChallenge || battle.Attacker.PlayerID != playerID || battle.Defender.PlayerID <= 0 || battle.Defender.PlayerID == playerID {
			return 0, 0, false, errors.New("battle state supplied for an invalid move")
		}
	}
	if landShop || landPveShop {
		if !end || shop == nil || shop.PlayerID != playerID || shop.PVE != landPveShop || len(shop.Cards) == 0 || shop.Gold < 0 {
			return 0, 0, false, errors.New("land shop offer is invalid")
		}
		if len(shop.Alreadys) != len(shop.Cards) {
			return 0, 0, false, errors.New("land shop sold flags do not match the offer")
		}
		if shop.PVE && len(shop.TalentSkillFreeCard) != len(shop.Cards) {
			return 0, 0, false, errors.New("PVE shop talent flags do not match the offer")
		}
	} else if shop != nil {
		return 0, 0, false, errors.New("shop state supplied for a non-shop move")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, 0, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return 0, 0, false, err
		}
		if existing > 0 {
			return 0, 0, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return 0, 0, false, err
	}
	if currentPlayer != playerID || pending <= 0 || phase != TurnPhaseMoving {
		return 0, round, false, errors.New("move is not active for player")
	}
	var currentNode, oldGold int32
	var buffsJSON, cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT node_id,gold,battle_buffs_json,battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&currentNode, &oldGold, &buffsJSON, &cardsJSON); err != nil {
		return 0, round, false, err
	}
	if currentNode != fromNode {
		return 0, round, false, errors.New("player position changed before move")
	}
	committedNode, committedBackNode := targetNode, fromNode
	if teleport != nil {
		if teleport.NodeID < 0 || teleport.NodeID == targetNode {
			return 0, round, false, errors.New("move teleport destination is invalid")
		}
		committedNode, committedBackNode = teleport.NodeID, targetNode
	}
	var buffs []BuffState
	if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
		return 0, round, false, fmt.Errorf("decode battle buffs for player %d: %w", playerID, err)
	}
	moveBuffIndex := -1
	for index := range buffs {
		if buffs[index].BuffID == 3001801 {
			moveBuffIndex = index
			break
		}
	}
	if moveBuffIndex >= 0 {
		buffs[moveBuffIndex].Progress++
	}
	var cards []CardState
	cardsChanged := false
	if len(cardsToDraw) > 0 {
		if handLimit <= 0 {
			return 0, round, false, errors.New("card draw has an invalid hand limit")
		}
		if cardsJSON != "" {
			if err = json.Unmarshal([]byte(cardsJSON), &cards); err != nil {
				return 0, round, false, fmt.Errorf("decode player battle cards: %w", err)
			}
		}
		if cards == nil {
			cards = []CardState{}
		}
		for _, card := range cardsToDraw {
			if card.UniqueID <= 0 || card.CardID <= 0 {
				return 0, round, false, errors.New("drawn card has an invalid identity")
			}
			for _, existing := range cards {
				if existing.UniqueID == card.UniqueID {
					return 0, round, false, errors.New("drawn card unique ID already exists")
				}
			}
			cards = append(cards, card)
			cardsChanged = true
		}
	}
	newGold := oldGold
	if end && moveBuffIndex >= 0 {
		steps := buffs[moveBuffIndex].Progress
		if steps < 1 {
			steps = 1
		}
		if steps > newGold {
			steps = newGold
		}
		newGold -= steps
		buffs = append(buffs[:moveBuffIndex], buffs[moveBuffIndex+1:]...)
	}
	entryGold := int32(0)
	if end && landShop && shop != nil && shop.EntryGold > 0 {
		entryGold = shop.EntryGold
		maxGold := int32(1<<31 - 1)
		if entryGold > maxGold-newGold {
			entryGold = maxGold - newGold
		}
		newGold += entryGold
	}
	var pickedLandBuff *LandBuffPickupResult
	var buffJSON string
	var buffValue int32
	var buffNodeID int32
	var buffUniqueID int64
	var buffID int32
	if err = tx.QueryRowContext(ctx, `SELECT unique_id,node_id,buff_id,value,buff_json FROM game_land_buffs WHERE room_id=? AND node_id=? ORDER BY created_at,unique_id LIMIT 1`, roomID, committedNode).Scan(&buffUniqueID, &buffNodeID, &buffID, &buffValue, &buffJSON); err == nil {
		var buff BuffState
		if err = json.Unmarshal([]byte(buffJSON), &buff); err != nil {
			return 0, round, false, fmt.Errorf("decode picked land buff %d: %w", buffUniqueID, err)
		}
		if buffUniqueID <= 0 || buffID <= 0 || buffNodeID != committedNode || buffValue <= 0 || buff.UniqueID != buffUniqueID || buff.BuffID != buffID || buff.NodeID != buffNodeID || buff.Source == nil || buff.Source.S != 4 || buff.Source.ID <= 0 {
			return 0, round, false, errors.New("picked land buff state is invalid")
		}
		res, deleteErr := tx.ExecContext(ctx, `DELETE FROM game_land_buffs WHERE room_id=? AND unique_id=? AND node_id=?`, roomID, buffUniqueID, committedNode)
		if deleteErr != nil {
			return 0, round, false, deleteErr
		}
		deleted, deleteErr := res.RowsAffected()
		if deleteErr != nil {
			return 0, round, false, deleteErr
		}
		if deleted != 1 {
			return 0, round, false, errors.New("picked land buff changed during movement")
		}
		pickupOldGold := newGold
		maxGold := int32(1<<31 - 1)
		if buffValue > maxGold-newGold {
			newGold = maxGold
		} else {
			newGold += buffValue
		}
		remaining, queryErr := landBuffsAtNodeTx(ctx, tx, roomID, committedNode)
		if queryErr != nil {
			return 0, round, false, queryErr
		}
		pickedLandBuff = &LandBuffPickupResult{
			LandBuff: LandBuffState{NodeID: buffNodeID, Value: buffValue, Buff: buff},
			OldGold:  pickupOldGold, NewGold: newGold, Remaining: remaining,
		}
	} else if !errors.Is(err, sql.ErrNoRows) {
		return 0, round, false, err
	}
	if teleport != nil {
		frontNodeIDs = teleport.FrontNodeIDs
	}
	frontJSON, marshalErr := json.Marshal(frontNodeIDs)
	if marshalErr != nil {
		return 0, round, false, marshalErr
	}
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return 0, round, false, e
		}
		n, e := res.RowsAffected()
		if e != nil {
			return 0, round, false, e
		}
		if n == 0 {
			return 0, round, false, nil
		}
	}
	if moveBuffIndex >= 0 {
		encoded, marshalErr := json.Marshal(buffs)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		_, err = tx.ExecContext(ctx, `UPDATE players SET back_node_id=?,node_id=?,front_node_ids_json=?,gold=?,battle_buffs_json=? WHERE id=? AND room_id=?`, committedBackNode, committedNode, string(frontJSON), newGold, string(encoded), playerID, roomID)
	} else if newGold != oldGold {
		_, err = tx.ExecContext(ctx, `UPDATE players SET back_node_id=?,node_id=?,front_node_ids_json=?,gold=? WHERE id=? AND room_id=?`, committedBackNode, committedNode, string(frontJSON), newGold, playerID, roomID)
	} else {
		_, err = tx.ExecContext(ctx, `UPDATE players SET back_node_id=?,node_id=?,front_node_ids_json=? WHERE id=? AND room_id=?`, committedBackNode, committedNode, string(frontJSON), playerID, roomID)
	}
	if err != nil {
		return 0, round, false, err
	}
	if cardsChanged {
		encoded, marshalErr := json.Marshal(cards)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encoded), playerID, roomID); err != nil {
			return 0, round, false, err
		}
	}
	if !end {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=?,turn_phase=? WHERE room_id=? AND current_player_id=?`, pending-1, TurnPhaseMoving, roomID, playerID)
		nextPlayer = playerID
	} else if len(cardsToDraw) > 0 && int32(len(cards)) > handLimit {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseAbandonCards, roomID, playerID)
		nextPlayer = playerID
	} else if landShop || landPveShop {
		stateJSON, marshalErr := json.Marshal(shop)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO game_shops(room_id,player_id,state_json,created_at) VALUES(?,?,?,?) ON CONFLICT(room_id) DO UPDATE SET player_id=excluded.player_id,state_json=excluded.state_json,created_at=excluded.created_at`, roomID, playerID, string(stateJSON), time.Now().Unix()); err != nil {
			return 0, round, false, err
		}
		shopPhase := TurnPhaseShop
		if landPveShop {
			shopPhase = TurnPhasePVEShop
		}
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, shopPhase, roomID, playerID)
		nextPlayer = playerID
	} else if moveAgain {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseMoveAgain, roomID, playerID)
		nextPlayer = playerID
	} else if rollGold {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseRollGold, roomID, playerID)
		nextPlayer = playerID
	} else if landHeal {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseLandHeal, roomID, playerID)
		nextPlayer = playerID
	} else if landBloodLoss {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseLandBloodLoss, roomID, playerID)
		nextPlayer = playerID
	} else if landHospital {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseHospital, roomID, playerID)
		nextPlayer = playerID
	} else if landDivination {
		choicesJSON, marshalErr := json.Marshal(divinationChoices)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO game_divinations(room_id,player_id,choices_json,created_at) VALUES(?,?,?,?) ON CONFLICT(room_id) DO UPDATE SET player_id=excluded.player_id,choices_json=excluded.choices_json,created_at=excluded.created_at`, roomID, playerID, string(choicesJSON), time.Now().Unix()); err != nil {
			return 0, round, false, err
		}
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseDivination, roomID, playerID)
		nextPlayer = playerID
	} else if landGamble {
		stateJSON, marshalErr := json.Marshal(gamble)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		now := time.Now().Unix()
		if _, err = tx.ExecContext(ctx, `INSERT INTO game_gambles(room_id,state_json,created_at,updated_at) VALUES(?,?,?,?) ON CONFLICT(room_id) DO UPDATE SET state_json=excluded.state_json,created_at=excluded.created_at,updated_at=excluded.updated_at`, roomID, string(stateJSON), now, now); err != nil {
			return 0, round, false, err
		}
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseGambleGuess, roomID, playerID)
		nextPlayer = playerID
	} else if battle != nil {
		stateJSON, marshalErr := json.Marshal(battle)
		if marshalErr != nil {
			return 0, round, false, marshalErr
		}
		now := time.Now().Unix()
		if _, err = tx.ExecContext(ctx, `INSERT INTO game_battles(room_id,state_json,created_at,updated_at) VALUES(?,?,?,?) ON CONFLICT(room_id) DO UPDATE SET state_json=excluded.state_json,created_at=excluded.created_at,updated_at=excluded.updated_at`, roomID, string(stateJSON), now, now); err != nil {
			return 0, round, false, err
		}
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseBattleChallenge, roomID, playerID)
		nextPlayer = playerID
	} else if fillingStation {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=?,turn_phase=? WHERE room_id=? AND current_player_id=?`, pending-1, TurnPhaseFillingStation, roomID, playerID)
		nextPlayer = playerID
	} else if landEvent {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseEvent, roomID, playerID)
		nextPlayer = playerID
	} else if landDestiny {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseDestiny, roomID, playerID)
		nextPlayer = playerID
	} else if landLottery {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseLottery, roomID, playerID)
		nextPlayer = playerID
	} else if landPursuit {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhasePursuit, roomID, playerID)
		nextPlayer = playerID
	} else if landBattery {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, TurnPhaseBattery, roomID, playerID)
		nextPlayer = playerID
	} else {
		rows, e := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
		if e != nil {
			return 0, round, false, e
		}
		var members []int64
		for rows.Next() {
			var id int64
			if e = rows.Scan(&id); e != nil {
				rows.Close()
				return 0, round, false, e
			}
			members = append(members, id)
		}
		if e = rows.Err(); e != nil {
			rows.Close()
			return 0, round, false, e
		}
		rows.Close()
		idx := -1
		for i, id := range members {
			if id == playerID {
				idx = i
				break
			}
		}
		if idx < 0 || len(members) == 0 {
			return 0, round, false, errors.New("current player is not in the room")
		}
		wrap := idx == len(members)-1
		nextPlayer = members[(idx+1)%len(members)]
		if wrap {
			round++
		}
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND current_player_id=?`, nextPlayer, round, TurnPhaseThrowDice, roomID, playerID)
	}
	if err != nil {
		return 0, round, false, err
	}
	if err = tx.Commit(); err != nil {
		return 0, round, false, err
	}
	if cardResult != nil && len(cardsToDraw) > 0 {
		cardResult.Cards = append([]CardState(nil), cards...)
		cardResult.Changed = cardsChanged
		cardResult.DiscardRequired = end && handLimit > 0 && int32(len(cards)) > handLimit
	}
	if placeResult != nil && teleport != nil {
		placeResult.Changed = true
		placeResult.NodeID = teleport.NodeID
		placeResult.BackNodeID = targetNode
		placeResult.FrontNodeIDs = append([]int32(nil), teleport.FrontNodeIDs...)
	}
	if goldResult != nil && entryGold > 0 {
		goldResult.OldGold = oldGold
		goldResult.NewGold = oldGold + entryGold
		goldResult.Changed = true
	}
	if pickupResult != nil && pickedLandBuff != nil {
		*pickupResult = *pickedLandBuff
		pickupResult.Remaining = append([]BuffState(nil), pickedLandBuff.Remaining...)
	}
	return nextPlayer, round, true, nil
}

// AddRoomGold credits in-room gold for one player (lottery payouts) and
// returns the previous and new balances.
func (s *Store) AddRoomGold(ctx context.Context, roomID, playerID int64, amount int32) (int32, int32, error) {
	if amount <= 0 {
		return 0, 0, errors.New("room gold amount must be positive")
	}
	var oldGold int32
	err := s.db.QueryRowContext(ctx, `SELECT gold FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldGold)
	if err != nil {
		return 0, 0, err
	}
	newGold := oldGold + amount
	if _, err := s.db.ExecContext(ctx, `UPDATE players SET gold=? WHERE id=? AND room_id=?`, newGold, playerID, roomID); err != nil {
		return 0, 0, err
	}
	return oldGold, newGold, nil
}

// CompleteLotteryChoice stores the configured lottery picks and releases the
// current turn atomically. The scheduled drawing is handled separately.
func (s *Store) CompleteLotteryChoice(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, vals []int32, chooseCount, numberLimit int32) (LotteryChoiceResult, bool, error) {
	if chooseCount <= 0 || numberLimit <= 0 || chooseCount > numberLimit || len(vals) != int(chooseCount) {
		return LotteryChoiceResult{}, false, ErrInvalidLotteryChoice
	}
	seen := make(map[int32]struct{}, len(vals))
	for _, value := range vals {
		if value < 1 || value > numberLimit {
			return LotteryChoiceResult{}, false, ErrInvalidLotteryChoice
		}
		if _, exists := seen[value]; exists {
			return LotteryChoiceResult{}, false, ErrInvalidLotteryChoice
		}
		seen[value] = struct{}{}
	}

	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return LotteryChoiceResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return LotteryChoiceResult{}, false, err
		}
		if existing > 0 {
			return LotteryChoiceResult{}, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	var result LotteryChoiceResult
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return LotteryChoiceResult{}, false, err
	}
	if currentPlayer != playerID {
		return LotteryChoiceResult{}, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != TurnPhaseLottery {
		return LotteryChoiceResult{}, false, ErrActionNotReady
	}
	var lotteriesJSON string
	if err = tx.QueryRowContext(ctx, `SELECT lotterys_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&lotteriesJSON); err != nil {
		return LotteryChoiceResult{}, false, err
	}
	lotterys := map[int32]bool{}
	if lotteriesJSON != "" {
		if err = json.Unmarshal([]byte(lotteriesJSON), &lotterys); err != nil {
			return LotteryChoiceResult{}, false, fmt.Errorf("decode lottery choices for player %d: %w", playerID, err)
		}
	}
	if lotterys == nil {
		lotterys = map[int32]bool{}
	}
	for _, value := range vals {
		if lotterys[value] {
			return LotteryChoiceResult{}, false, ErrInvalidLotteryChoice
		}
		lotterys[value] = true
	}
	encoded, err := json.Marshal(lotterys)
	if err != nil {
		return LotteryChoiceResult{}, false, err
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return LotteryChoiceResult{}, false, insertErr
		}
		inserted, rowsErr := res.RowsAffected()
		if rowsErr != nil {
			return LotteryChoiceResult{}, false, rowsErr
		}
		if inserted == 0 {
			return LotteryChoiceResult{}, false, nil
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET lotterys_json=? WHERE id=? AND room_id=?`, string(encoded), playerID, roomID); err != nil {
		return LotteryChoiceResult{}, false, err
	}
	rows, err := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return LotteryChoiceResult{}, false, err
	}
	var members []int64
	for rows.Next() {
		var memberID int64
		if err = rows.Scan(&memberID); err != nil {
			rows.Close()
			return LotteryChoiceResult{}, false, err
		}
		members = append(members, memberID)
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return LotteryChoiceResult{}, false, err
	}
	rows.Close()
	idx := -1
	for i, memberID := range members {
		if memberID == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return LotteryChoiceResult{}, false, errors.New("lottery player is not in the room")
	}
	result.NextPlayer = members[(idx+1)%len(members)]
	if idx == len(members)-1 {
		result.Round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseLottery)
	if err != nil {
		return LotteryChoiceResult{}, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return LotteryChoiceResult{}, false, err
	}
	if updated == 0 {
		return LotteryChoiceResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return LotteryChoiceResult{}, false, err
	}
	result.Lotterys = lotterys
	return result, true, nil
}

// CompletePursuit resolves the Pursuit tile choice and advances the turn in
// one transaction. A zero target exits without moving the player.
func (s *Store) CompletePursuit(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, targetID int64, frontNodeIDs []int32) (PursuitResult, bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return PursuitResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return PursuitResult{}, false, err
		}
		if existing > 0 {
			return PursuitResult{}, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	var result PursuitResult
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return PursuitResult{}, false, err
	}
	if currentPlayer != playerID {
		return PursuitResult{}, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != TurnPhasePursuit {
		return PursuitResult{}, false, ErrActionNotReady
	}
	var actorNode int32
	if err = tx.QueryRowContext(ctx, `SELECT node_id FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&actorNode); err != nil {
		return PursuitResult{}, false, err
	}
	result.NodeID = actorNode
	result.Exit = targetID == 0
	if targetID < 0 || targetID == playerID {
		return PursuitResult{}, false, ErrInvalidPursuitTarget
	}
	if targetID > 0 {
		var targetNode, targetHP int32
		err = tx.QueryRowContext(ctx, `SELECT node_id,hp FROM players WHERE id=? AND room_id=?`, targetID, roomID).Scan(&targetNode, &targetHP)
		if errors.Is(err, sql.ErrNoRows) {
			return PursuitResult{}, false, ErrInvalidPursuitTarget
		}
		if err != nil {
			return PursuitResult{}, false, err
		}
		if targetHP <= 0 {
			return PursuitResult{}, false, ErrInvalidPursuitTarget
		}
		frontJSON, marshalErr := json.Marshal(frontNodeIDs)
		if marshalErr != nil {
			return PursuitResult{}, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET node_id=?,back_node_id=?,front_node_ids_json=? WHERE id=? AND room_id=?`, targetNode, actorNode, string(frontJSON), playerID, roomID); err != nil {
			return PursuitResult{}, false, err
		}
		result.NodeID = targetNode
		result.BackNodeID = actorNode
		result.FrontNodeIDs = append([]int32(nil), frontNodeIDs...)
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return PursuitResult{}, false, insertErr
		}
		inserted, rowsErr := res.RowsAffected()
		if rowsErr != nil {
			return PursuitResult{}, false, rowsErr
		}
		if inserted == 0 {
			return PursuitResult{}, false, nil
		}
	}
	rows, err := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return PursuitResult{}, false, err
	}
	var members []int64
	for rows.Next() {
		var memberID int64
		if err = rows.Scan(&memberID); err != nil {
			rows.Close()
			return PursuitResult{}, false, err
		}
		members = append(members, memberID)
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return PursuitResult{}, false, err
	}
	rows.Close()
	idx := -1
	for i, memberID := range members {
		if memberID == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return PursuitResult{}, false, errors.New("pursuit player is not in the room")
	}
	result.NextPlayer = members[(idx+1)%len(members)]
	if idx == len(members)-1 {
		result.Round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, TurnPhasePursuit)
	if err != nil {
		return PursuitResult{}, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return PursuitResult{}, false, err
	}
	if updated == 0 {
		return PursuitResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return PursuitResult{}, false, err
	}
	return result, true, nil
}

// CompleteGameEvent applies a resource-backed event outcome and advances the
// active turn in one transaction. The event phase prevents duplicate requests
// from applying the same outcome twice.
func (s *Store) CompleteGameEvent(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, eventID int32, deltas []EventAttrDelta, progressDelta *int32) (GameEventResult, bool, error) {
	return s.CompleteGameAction(ctx, roomID, playerID, cmd, upsn, payload, eventID, TurnPhaseEvent, deltas, progressDelta)
}

// CompleteGameAction applies a resource-backed land outcome and advances the
// active turn in the same transaction. Only explicitly supported action phases
// may use this generic mutation path.
func (s *Store) CompleteGameAction(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, actionID int32, requiredPhase string, deltas []EventAttrDelta, progressDelta *int32) (GameActionResult, bool, error) {
	if requiredPhase != TurnPhaseEvent && requiredPhase != TurnPhaseDestiny && requiredPhase != TurnPhaseDivination && requiredPhase != TurnPhaseSelectEvent && requiredPhase != TurnPhaseBattery {
		return GameActionResult{}, false, errors.New("unsupported land action phase")
	}
	if progressDelta != nil && requiredPhase != TurnPhaseEvent && requiredPhase != TurnPhaseSelectEvent {
		return GameActionResult{}, false, errors.New("game progress change is only supported by events")
	}
	if actionID <= 0 || (len(deltas) == 0 && progressDelta == nil) {
		return GameActionResult{}, false, errors.New("land action outcome is empty")
	}
	for _, delta := range deltas {
		if delta.DiscardLimit < 0 {
			return GameActionResult{}, false, errors.New("land action discard hand limit is invalid")
		}
		if delta.DiscardLimit > 0 && (requiredPhase != TurnPhaseDestiny || (actionID != 40007 && actionID != 40008) || len(deltas) != 1 || delta.PlayerID != playerID) {
			return GameActionResult{}, false, errors.New("card discard phase is only supported for the active Destiny player")
		}
		if len(delta.LandBuffs) > 0 && (requiredPhase != TurnPhaseDestiny || actionID != 40012 || len(deltas) != 1 || delta.PlayerID != playerID) {
			return GameActionResult{}, false, errors.New("land summon drop is only supported for the active Destiny player")
		}
		for _, landBuff := range delta.LandBuffs {
			if landBuff.NodeID <= 0 || landBuff.Value <= 0 || landBuff.Buff.UniqueID <= 0 || landBuff.Buff.BuffID <= 0 || landBuff.Buff.NodeID != landBuff.NodeID || landBuff.Buff.Source == nil || landBuff.Buff.Source.S != 4 {
				return GameActionResult{}, false, errors.New("land summon state is invalid")
			}
		}
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return GameEventResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return GameEventResult{}, false, err
		}
		if existing > 0 {
			return GameEventResult{}, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	var round, gameProgress, gameMaxProgress int32
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase,game_progress,game_max_progress FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase, &gameProgress, &gameMaxProgress); err != nil {
		return GameEventResult{}, false, err
	}
	if currentPlayer != playerID {
		return GameEventResult{}, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != requiredPhase {
		return GameEventResult{}, false, ErrActionNotReady
	}
	if requiredPhase == TurnPhaseDivination {
		var choicesJSON string
		if err = tx.QueryRowContext(ctx, `SELECT choices_json FROM game_divinations WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&choicesJSON); err != nil {
			return GameActionResult{}, false, ErrActionNotReady
		}
		var choices []int32
		if err = json.Unmarshal([]byte(choicesJSON), &choices); err != nil {
			return GameActionResult{}, false, fmt.Errorf("decode Divination offer: %w", err)
		}
		found := false
		for _, choice := range choices {
			if choice == actionID {
				found = true
				break
			}
		}
		if !found {
			return GameActionResult{}, false, ErrInvalidDivinationChoice
		}
	}
	if requiredPhase == TurnPhaseSelectEvent {
		var choicesJSON string
		if err = tx.QueryRowContext(ctx, `SELECT choices_json FROM game_skill_event_offers WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&choicesJSON); err != nil {
			return GameActionResult{}, false, ErrActionNotReady
		}
		var choices []int32
		if err = json.Unmarshal([]byte(choicesJSON), &choices); err != nil {
			return GameActionResult{}, false, fmt.Errorf("decode skill event offer: %w", err)
		}
		found := false
		for _, choice := range choices {
			if choice == actionID {
				found = true
				break
			}
		}
		if len(choices) != 2 || !found {
			return GameActionResult{}, false, ErrInvalidSkillEventChoice
		}
	}
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return GameEventResult{}, false, e
		}
		inserted, e := res.RowsAffected()
		if e != nil {
			return GameEventResult{}, false, e
		}
		if inserted == 0 {
			return GameEventResult{}, false, nil
		}
	}
	result := GameActionResult{EventID: actionID, Round: round, GameProgress: gameProgress, GameMaxProgress: gameMaxProgress}
	if progressDelta != nil {
		if gameMaxProgress <= 0 {
			return GameEventResult{}, false, errors.New("game progress change has no configured limit")
		}
		newProgress := gameProgress + *progressDelta
		if newProgress < 0 {
			newProgress = 0
		} else if newProgress > gameMaxProgress {
			newProgress = gameMaxProgress
		}
		result.ProgressChanged = newProgress != gameProgress
		result.GameProgress = newProgress
	}
	seen := make(map[int64]struct{}, len(deltas))
	for _, delta := range deltas {
		if delta.PlayerID <= 0 {
			return GameEventResult{}, false, errors.New("event target player is invalid")
		}
		if _, exists := seen[delta.PlayerID]; exists {
			return GameEventResult{}, false, errors.New("event target player is duplicated")
		}
		seen[delta.PlayerID] = struct{}{}
		var oldHP, oldGold, oldNodeID, oldBackNodeID int32
		var cardsJSON, buffsJSON, oldFrontJSON string
		if err = tx.QueryRowContext(ctx, `SELECT hp,gold,battle_cards_json,battle_buffs_json,node_id,back_node_id,front_node_ids_json FROM players WHERE id=? AND room_id=?`, delta.PlayerID, roomID).Scan(&oldHP, &oldGold, &cardsJSON, &buffsJSON, &oldNodeID, &oldBackNodeID, &oldFrontJSON); err != nil {
			return GameEventResult{}, false, err
		}
		for _, landBuff := range delta.LandBuffs {
			if landBuff.NodeID != oldNodeID {
				return GameActionResult{}, false, errors.New("land summon must be dropped at the active player's current node")
			}
			encoded, marshalErr := json.Marshal(landBuff.Buff)
			if marshalErr != nil {
				return GameActionResult{}, false, marshalErr
			}
			if _, err = tx.ExecContext(ctx, `INSERT INTO game_land_buffs(room_id,unique_id,node_id,buff_id,value,buff_json,created_at) VALUES(?,?,?,?,?,?,?)`, roomID, landBuff.Buff.UniqueID, landBuff.NodeID, landBuff.Buff.BuffID, landBuff.Value, string(encoded), time.Now().Unix()); err != nil {
				return GameActionResult{}, false, err
			}
			buffsAtNode, queryErr := landBuffsAtNodeTx(ctx, tx, roomID, landBuff.NodeID)
			if queryErr != nil {
				return GameActionResult{}, false, queryErr
			}
			result.LandBuffChanges = append(result.LandBuffChanges, LandBuffUpdate{NodeID: landBuff.NodeID, Buffs: buffsAtNode})
		}
		var cards []CardState
		if cardsJSON != "" {
			if err = json.Unmarshal([]byte(cardsJSON), &cards); err != nil {
				return GameEventResult{}, false, fmt.Errorf("decode player battle cards: %w", err)
			}
		}
		if cards == nil {
			cards = []CardState{}
		}
		if delta.ReplaceCards {
			cards = append([]CardState{}, delta.Cards...)
		}
		if !delta.ReplaceCards {
			for _, card := range delta.Cards {
				if card.UniqueID <= 0 || card.CardID <= 0 {
					return GameEventResult{}, false, errors.New("event card has an invalid identity")
				}
				for _, existing := range cards {
					if existing.UniqueID == card.UniqueID {
						return GameEventResult{}, false, errors.New("event card unique id already exists")
					}
				}
				cards = append(cards, card)
			}
		}
		if delta.DiscardLimit > 0 && len(cards) > int(delta.DiscardLimit) {
			result.DiscardRequired = true
		}
		var buffs []BuffState
		if buffsJSON != "" {
			if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
				return GameEventResult{}, false, fmt.Errorf("decode player battle buffs: %w", err)
			}
		}
		if buffs == nil {
			buffs = []BuffState{}
		}
		buffsChanged := false
		var removedBuffs []BuffState
		if !delta.SetHP && delta.HPChange < 0 && oldHP > 0 {
			damage, remaining, consumed := ApplyDestinyDamageBuffs(-delta.HPChange, buffs)
			if len(consumed) > 0 {
				delta.HPChange = -damage
				buffs = remaining
				removedBuffs = append(removedBuffs, consumed...)
				buffsChanged = true
			}
		}
		newBuffs := make([]BuffState, 0, len(delta.Buffs))
		for _, buff := range delta.Buffs {
			if buff.UniqueID <= 0 || buff.BuffID <= 0 {
				return GameEventResult{}, false, errors.New("event buff has an invalid identity")
			}
			duplicate := false
			for _, existing := range buffs {
				if existing.UniqueID == buff.UniqueID || existing.BuffID == buff.BuffID {
					duplicate = true
					break
				}
			}
			if !duplicate {
				buffs = append(buffs, buff)
				newBuffs = append(newBuffs, buff)
				buffsChanged = true
			}
		}
		newHP := oldHP
		if delta.SetHP {
			newHP = delta.HPValue
		} else if delta.HPChange != 0 {
			if delta.MaxHP <= 0 {
				return GameEventResult{}, false, errors.New("event HP change has no maximum HP")
			}
			newHP += delta.HPChange
		}
		if newHP < 0 {
			newHP = 0
		}
		if delta.MaxHP > 0 && newHP > delta.MaxHP {
			newHP = delta.MaxHP
		}
		newGold64 := int64(oldGold) + int64(delta.GoldChange)
		if newGold64 < 0 {
			newGold64 = 0
		}
		if newGold64 > int64(1<<31-1) {
			newGold64 = int64(1<<31 - 1)
		}
		newGold := int32(newGold64)
		newNodeID, newBackNodeID := oldNodeID, oldBackNodeID
		newFrontJSON := oldFrontJSON
		if delta.PlaceChanged {
			if delta.NodeID < 0 {
				return GameEventResult{}, false, errors.New("event place change has an invalid node")
			}
			newNodeID, newBackNodeID = delta.NodeID, delta.BackNodeID
			frontJSON, marshalErr := json.Marshal(delta.FrontNodeIDs)
			if marshalErr != nil {
				return GameEventResult{}, false, marshalErr
			}
			newFrontJSON = string(frontJSON)
		}
		cardsJSONBytes, marshalErr := json.Marshal(cards)
		if marshalErr != nil {
			return GameEventResult{}, false, marshalErr
		}
		buffsJSONBytes, marshalErr := json.Marshal(buffs)
		if marshalErr != nil {
			return GameEventResult{}, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,gold=?,battle_cards_json=?,battle_buffs_json=?,node_id=?,back_node_id=?,front_node_ids_json=? WHERE id=? AND room_id=?`, newHP, newGold, string(cardsJSONBytes), string(buffsJSONBytes), newNodeID, newBackNodeID, newFrontJSON, delta.PlayerID, roomID); err != nil {
			return GameEventResult{}, false, err
		}
		if err = recordMatchHPChangeTx(ctx, tx, roomID, delta.PlayerID, oldHP, newHP); err != nil {
			return GameEventResult{}, false, err
		}
		result.Changes = append(result.Changes, EventAttrResult{
			PlayerID: delta.PlayerID,
			OldHP:    oldHP, NewHP: newHP, MaxHP: delta.MaxHP,
			OldGold: oldGold, NewGold: newGold, Cards: cards, CardsChanged: delta.ReplaceCards || len(delta.Cards) > 0,
			Buffs: buffs, BuffsChanged: buffsChanged, NewBuffs: newBuffs, RemovedBuffs: removedBuffs,
			PlaceChanged: delta.PlaceChanged, NodeID: newNodeID, BackNodeID: newBackNodeID,
			FrontNodeIDs: append([]int32(nil), delta.FrontNodeIDs...),
		})
	}
	if requiredPhase == TurnPhaseDivination {
		res, deleteErr := tx.ExecContext(ctx, `DELETE FROM game_divinations WHERE room_id=? AND player_id=?`, roomID, playerID)
		if deleteErr != nil {
			return GameActionResult{}, false, deleteErr
		}
		deleted, deleteErr := res.RowsAffected()
		if deleteErr != nil {
			return GameActionResult{}, false, deleteErr
		}
		if deleted == 0 {
			return GameActionResult{}, false, ErrActionNotReady
		}
	}
	if requiredPhase == TurnPhaseSelectEvent {
		res, deleteErr := tx.ExecContext(ctx, `DELETE FROM game_skill_event_offers WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round)
		if deleteErr != nil {
			return GameActionResult{}, false, deleteErr
		}
		deleted, deleteErr := res.RowsAffected()
		if deleteErr != nil {
			return GameActionResult{}, false, deleteErr
		}
		if deleted != 1 {
			return GameActionResult{}, false, ErrActionNotReady
		}
		result.NextPlayer = playerID
		res, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=?,game_progress=?,game_max_progress=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseThrowDice, result.GameProgress, result.GameMaxProgress, roomID, playerID, round, TurnPhaseSelectEvent)
		if err != nil {
			return GameActionResult{}, false, err
		}
		updated, err := res.RowsAffected()
		if err != nil {
			return GameActionResult{}, false, err
		}
		if updated != 1 {
			return GameActionResult{}, false, ErrActionNotReady
		}
		if err = tx.Commit(); err != nil {
			return GameActionResult{}, false, err
		}
		return result, true, nil
	}
	if result.DiscardRequired {
		result.NextPlayer = playerID
		res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=?,game_progress=?,game_max_progress=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, playerID, round, TurnPhaseAbandonCards, result.GameProgress, result.GameMaxProgress, roomID, playerID, requiredPhase)
		if err != nil {
			return GameActionResult{}, false, err
		}
		updated, err := res.RowsAffected()
		if err != nil {
			return GameActionResult{}, false, err
		}
		if updated != 1 {
			return GameActionResult{}, false, ErrActionNotReady
		}
		if err = tx.Commit(); err != nil {
			return GameActionResult{}, false, err
		}
		return result, true, nil
	}
	rows, err := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return GameEventResult{}, false, err
	}
	var members []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			rows.Close()
			return GameEventResult{}, false, err
		}
		members = append(members, id)
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return GameEventResult{}, false, err
	}
	rows.Close()
	idx := -1
	for i, id := range members {
		if id == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return GameEventResult{}, false, errors.New("current player is not in the room")
	}
	result.NextPlayer = members[(idx+1)%len(members)]
	if idx == len(members)-1 {
		result.Round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=?,game_progress=?,game_max_progress=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, result.GameProgress, result.GameMaxProgress, roomID, playerID, requiredPhase)
	if err != nil {
		return GameEventResult{}, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return GameEventResult{}, false, err
	}
	if updated == 0 {
		return GameEventResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return GameEventResult{}, false, err
	}
	return result, true, nil
}

func landBuffsAtNodeTx(ctx context.Context, tx *sql.Tx, roomID int64, nodeID int32) ([]BuffState, error) {
	rows, err := tx.QueryContext(ctx, `SELECT buff_json FROM game_land_buffs WHERE room_id=? AND node_id=? ORDER BY created_at,unique_id`, roomID, nodeID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var buffs []BuffState
	for rows.Next() {
		var encoded string
		if err = rows.Scan(&encoded); err != nil {
			return nil, err
		}
		var buff BuffState
		if err = json.Unmarshal([]byte(encoded), &buff); err != nil {
			return nil, fmt.Errorf("decode land buff at node %d: %w", nodeID, err)
		}
		buffs = append(buffs, buff)
	}
	if err = rows.Err(); err != nil {
		return nil, err
	}
	return buffs, nil
}

// DivinationChoices returns the pair selected when the current player landed
// on a Divination tile. The pair is persisted with the move for reconnects.
func (s *Store) DivinationChoices(ctx context.Context, roomID, playerID int64) ([]int32, bool, error) {
	var encoded string
	err := s.db.QueryRowContext(ctx, `SELECT choices_json FROM game_divinations WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&encoded)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, false, nil
	}
	if err != nil {
		return nil, false, err
	}
	var choices []int32
	if err = json.Unmarshal([]byte(encoded), &choices); err != nil {
		return nil, false, fmt.Errorf("decode Divination offer: %w", err)
	}
	if len(choices) != 2 || choices[0] <= 0 || choices[1] <= 0 || choices[0] == choices[1] {
		return nil, false, errors.New("stored Divination offer is invalid")
	}
	return choices, true, nil
}

// CompleteLandHPChange applies a configured land HP effect and releases the
// turn atomically. The phase makes recovery after a process restart safe.
func (s *Store) CompleteLandHPChange(ctx context.Context, roomID, playerID int64, amount, maxHP int32, requiredPhase string) (oldHP, newHP int32, nextPlayer int64, round int32, created bool, err error) {
	if requiredPhase != TurnPhaseLandHeal && requiredPhase != TurnPhaseLandBloodLoss {
		return 0, 0, 0, 0, false, errors.New("unsupported land HP phase")
	}
	if maxHP <= 0 {
		return 0, 0, 0, 0, false, errors.New("maximum HP must be positive")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, 0, 0, 0, false, err
	}
	defer tx.Rollback()
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return 0, 0, 0, 0, false, err
	}
	if currentPlayer != playerID {
		return 0, 0, 0, round, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != requiredPhase {
		return 0, 0, 0, round, false, ErrActionNotReady
	}
	var buffsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT hp,battle_buffs_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldHP, &buffsJSON); err != nil {
		return 0, 0, 0, round, false, err
	}
	if amount < 0 && oldHP > 0 {
		buffs := []BuffState{}
		if buffsJSON != "" {
			if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
				return 0, 0, 0, round, false, fmt.Errorf("decode battle buffs for player %d: %w", playerID, err)
			}
		}
		damage, remaining, consumed := ApplyDestinyDamageBuffs(-amount, buffs)
		if len(consumed) > 0 {
			amount = -damage
			encoded, marshalErr := json.Marshal(remaining)
			if marshalErr != nil {
				return 0, 0, 0, round, false, marshalErr
			}
			buffsJSON = string(encoded)
		}
	}
	newHP64 := int64(oldHP) + int64(amount)
	if newHP64 < 0 {
		newHP64 = 0
	}
	if newHP64 > int64(maxHP) {
		newHP64 = int64(maxHP)
	}
	newHP = int32(newHP64)
	if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_buffs_json=? WHERE id=? AND room_id=?`, newHP, buffsJSON, playerID, roomID); err != nil {
		return 0, 0, 0, round, false, err
	}
	if err = recordMatchHPChangeTx(ctx, tx, roomID, playerID, oldHP, newHP); err != nil {
		return 0, 0, 0, round, false, err
	}
	rows, e := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if e != nil {
		return 0, 0, 0, round, false, e
	}
	var members []int64
	for rows.Next() {
		var id int64
		if e = rows.Scan(&id); e != nil {
			rows.Close()
			return 0, 0, 0, round, false, e
		}
		members = append(members, id)
	}
	if e = rows.Err(); e != nil {
		rows.Close()
		return 0, 0, 0, round, false, e
	}
	rows.Close()
	idx := -1
	for i, id := range members {
		if id == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return 0, 0, 0, round, false, errors.New("current player is not in the room")
	}
	wrap := idx == len(members)-1
	nextPlayer = members[(idx+1)%len(members)]
	if wrap {
		round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, nextPlayer, round, TurnPhaseThrowDice, roomID, playerID, requiredPhase)
	if err != nil {
		return 0, 0, 0, round, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return 0, 0, 0, round, false, err
	}
	if updated == 0 {
		return 0, 0, 0, round, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return 0, 0, 0, round, false, err
	}
	return oldHP, newHP, nextPlayer, round, true, nil
}

// CompleteLandHeal keeps the heal-specific invariant at the store boundary.
func (s *Store) CompleteLandHeal(ctx context.Context, roomID, playerID int64, amount, maxHP int32) (oldHP, newHP int32, nextPlayer int64, round int32, created bool, err error) {
	if amount < 0 {
		return 0, 0, 0, 0, false, errors.New("land heal amount must not be negative")
	}
	return s.CompleteLandHPChange(ctx, roomID, playerID, amount, maxHP, TurnPhaseLandHeal)
}

// CompleteLandBloodLoss applies the configured signed HP change and releases
// the turn atomically.
func (s *Store) CompleteLandBloodLoss(ctx context.Context, roomID, playerID int64, amount, maxHP int32) (oldHP, newHP int32, nextPlayer int64, round int32, created bool, err error) {
	if amount > 0 {
		return 0, 0, 0, 0, false, errors.New("blood-loss amount must not be positive")
	}
	return s.CompleteLandHPChange(ctx, roomID, playerID, amount, maxHP, TurnPhaseLandBloodLoss)
}

// CompleteFillingStation resolves the stop/continue choice and its rewards in
// one transaction. A continue choice preserves the remaining movement points;
// stopping grants the station's fixed heal and at most one configured upgrade.
func (s *Store) CompleteFillingStation(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, stop bool, heal, maxHP int32, upgrades []FillingStationUpgrade, mode, teamID, landType, giftCount int32) (result FillingStationResult, created bool, err error) {
	if heal < 0 || maxHP <= 0 {
		return result, false, errors.New("invalid filling station heal configuration")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return result, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return result, false, err
		}
		if existing > 0 {
			return result, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return result, false, err
	}
	if currentPlayer != playerID {
		return result, false, ErrTurnPlayerMismatch
	}
	if pending < 0 || phase != TurnPhaseFillingStation {
		return result, false, ErrActionNotReady
	}
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return result, false, e
		}
		inserted, e := res.RowsAffected()
		if e != nil {
			return result, false, e
		}
		if inserted == 0 {
			return result, false, nil
		}
	}
	result.Pending = pending
	result.Asymmetrical = mode == 7
	result.MaxHP = maxHP
	result.NextPlayer = playerID
	result.Completed = stop || pending == 0
	if !result.Completed {
		res, e := tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=? AND turn_phase=?`, TurnPhaseMoving, roomID, playerID, pending, TurnPhaseFillingStation)
		if e != nil {
			return result, false, e
		}
		updated, e := res.RowsAffected()
		if e != nil {
			return result, false, e
		}
		if updated == 0 {
			return result, false, ErrActionNotReady
		}
		if err = tx.Commit(); err != nil {
			return result, false, err
		}
		return result, true, nil
	}
	if stop {
		if mode == 7 {
			if err = tx.QueryRowContext(ctx, `SELECT special_score FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldSpecialScore); err != nil {
				return result, false, err
			}
			if err = tx.QueryRowContext(ctx, `SELECT special_score FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&result.OldGameScore); err != nil {
				return result, false, err
			}
			result.NewSpecialScore = result.OldSpecialScore
			result.NewGameScore = result.OldGameScore
			switch {
			case landType == 23 && teamID == 10 && giftCount > 0:
				result.NewSpecialScore += giftCount
			case landType == 23 && teamID == 20 && giftCount > 0:
				result.NewGameScore -= giftCount
				if result.NewGameScore < 0 {
					result.NewGameScore = 0
				}
			case (landType == 1 || landType == 2) && teamID == 10 && result.OldSpecialScore > 0:
				result.NewGameScore += result.OldSpecialScore
				result.NewSpecialScore = 0
			}
			if result.NewSpecialScore != result.OldSpecialScore {
				if _, err = tx.ExecContext(ctx, `UPDATE players SET special_score=? WHERE id=? AND room_id=?`, result.NewSpecialScore, playerID, roomID); err != nil {
					return result, false, err
				}
			}
			if result.NewGameScore != result.OldGameScore {
				if _, err = tx.ExecContext(ctx, `UPDATE game_sessions SET special_score=? WHERE room_id=? AND status='running'`, result.NewGameScore, roomID); err != nil {
					return result, false, err
				}
			}
		} else {
			result.HealAmount = heal
			if err = tx.QueryRowContext(ctx, `SELECT hp,gold,hero_level FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldHP, &result.OldGold, &result.OldLevel); err != nil {
				return result, false, err
			}
			result.NewHP = result.OldHP + heal
			if result.NewHP > maxHP {
				result.NewHP = maxHP
			}
			if result.NewHP < 0 {
				result.NewHP = 0
			}
			result.NewGold = result.OldGold
			result.NewLevel = result.OldLevel
			if result.OldLevel >= 0 && int(result.OldLevel) < len(upgrades) {
				upgrade := upgrades[result.OldLevel]
				if upgrade.Gold >= 0 && upgrade.Star > result.OldLevel && result.OldGold >= upgrade.Gold {
					result.NewGold -= upgrade.Gold
					result.NewLevel = upgrade.Star
					result.Upgraded = true
				}
			}
			if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,gold=?,hero_level=? WHERE id=? AND room_id=?`, result.NewHP, result.NewGold, result.NewLevel, playerID, roomID); err != nil {
				return result, false, err
			}
			if err = recordMatchHPChangeTx(ctx, tx, roomID, playerID, result.OldHP, result.NewHP); err != nil {
				return result, false, err
			}
		}
	}
	result.NextPlayer, result.Round, err = nextRoomMemberTx(ctx, tx, roomID, playerID, result.Round)
	if err != nil {
		return result, false, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=? AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, pending, TurnPhaseFillingStation)
	if err != nil {
		return result, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return result, false, err
	}
	if updated == 0 {
		return result, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return result, false, err
	}
	return result, true, nil
}

func nextRoomMemberTx(ctx context.Context, tx *sql.Tx, roomID, playerID int64, round int32) (int64, int32, error) {
	rows, err := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return 0, round, err
	}
	defer rows.Close()
	var members []int64
	for rows.Next() {
		var member int64
		if err = rows.Scan(&member); err != nil {
			return 0, round, err
		}
		members = append(members, member)
	}
	if err = rows.Err(); err != nil {
		return 0, round, err
	}
	idx := -1
	for i, member := range members {
		if member == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return 0, round, errors.New("current player is not in the room")
	}
	wrap := idx == len(members)-1
	if wrap {
		round++
	}
	return members[(idx+1)%len(members)], round, nil
}

// CompleteHospitalExam records the hospital action, applies its configured
// recovery, and releases the current turn atomically.
func (s *Store) CompleteHospitalExam(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, sickThreshold, healAmount, maxHP int32) (HospitalResult, bool, error) {
	if sickThreshold < 0 || healAmount < 0 || maxHP <= 0 {
		return HospitalResult{}, false, errors.New("hospital settings are invalid")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return HospitalResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return HospitalResult{}, false, err
		}
		if existing > 0 {
			return HospitalResult{}, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	var result HospitalResult
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return HospitalResult{}, false, err
	}
	if currentPlayer != playerID {
		return HospitalResult{}, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != TurnPhaseHospital {
		return HospitalResult{}, false, ErrActionNotReady
	}
	if err = tx.QueryRowContext(ctx, `SELECT hp,node_id FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldHP, &result.NodeID); err != nil {
		return HospitalResult{}, false, err
	}
	result.MaxHP = maxHP
	result.InHospital = result.OldHP <= sickThreshold
	result.NewHP = result.OldHP
	if result.InHospital {
		newHP := int64(result.NewHP) + int64(healAmount)
		if newHP > int64(maxHP) {
			result.NewHP = maxHP
		} else {
			result.NewHP = int32(newHP)
		}
	}
	if upsn > 0 {
		if _, err = tx.ExecContext(ctx, `INSERT INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix()); err != nil {
			return HospitalResult{}, false, err
		}
	}
	hospitalRounds := int32(0)
	if result.InHospital {
		hospitalRounds = 1
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,hospital_rounds=? WHERE id=? AND room_id=?`, result.NewHP, hospitalRounds, playerID, roomID); err != nil {
		return HospitalResult{}, false, err
	}
	if err = recordMatchHPChangeTx(ctx, tx, roomID, playerID, result.OldHP, result.NewHP); err != nil {
		return HospitalResult{}, false, err
	}
	result.NextPlayer, result.Round, err = nextRoomTurnInTx(ctx, tx, roomID, playerID, result.Round)
	if err != nil {
		return HospitalResult{}, false, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseHospital)
	if err != nil {
		return HospitalResult{}, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return HospitalResult{}, false, err
	}
	if updated == 0 {
		return HospitalResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return HospitalResult{}, false, err
	}
	return result, true, nil
}

// CompleteHospitalizedTurn consumes one hospitalization turn and advances the
// room in the same transaction so a restart cannot grant the skipped action.
func (s *Store) CompleteHospitalizedTurn(ctx context.Context, roomID, playerID int64) (nextPlayer int64, round int32, skipped bool, err error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, 0, false, err
	}
	defer tx.Rollback()
	var currentPlayer int64
	var pending, hospitalRounds int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return 0, 0, false, err
	}
	if currentPlayer != playerID || pending != 0 || phase != TurnPhaseThrowDice {
		return currentPlayer, round, false, nil
	}
	if err = tx.QueryRowContext(ctx, `SELECT hospital_rounds FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&hospitalRounds); err != nil {
		return 0, round, false, err
	}
	if hospitalRounds <= 0 {
		return playerID, round, false, nil
	}
	nextPlayer, round, err = nextRoomTurnInTx(ctx, tx, roomID, playerID, round)
	if err != nil {
		return 0, round, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET hospital_rounds=? WHERE id=? AND room_id=?`, hospitalRounds-1, playerID, roomID); err != nil {
		return 0, round, false, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, nextPlayer, round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseThrowDice)
	if err != nil {
		return 0, round, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return 0, round, false, err
	}
	if updated == 0 {
		return 0, round, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return 0, round, false, err
	}
	return nextPlayer, round, true, nil
}

func nextRoomTurnInTx(ctx context.Context, tx *sql.Tx, roomID, playerID int64, round int32) (int64, int32, error) {
	rows, err := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return 0, round, err
	}
	var members []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			rows.Close()
			return 0, round, err
		}
		members = append(members, id)
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return 0, round, err
	}
	rows.Close()
	idx := -1
	for i, id := range members {
		if id == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return 0, round, errors.New("current player is not in the room")
	}
	if idx == len(members)-1 {
		round++
	}
	return members[(idx+1)%len(members)], round, nil
}

// ReviveAndSkipDeadTurn follows the tutorial loop: a hero that starts a turn
// at zero HP is restored to max HP and loses that action. HP, configured
// death-clear buffs, and the turn advance commit together so a restart cannot
// duplicate the revival or leave the client-visible buff state stale.
func (s *Store) ReviveAndSkipDeadTurn(ctx context.Context, roomID, playerID int64, maxHP int32, deathClearBuffIDs []int32) (oldHP, newHP int32, nextPlayer int64, round int32, removedBuffs []BuffState, created bool, err error) {
	if maxHP <= 0 {
		return 0, 0, 0, 0, nil, false, errors.New("maximum HP must be positive")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, 0, 0, 0, nil, false, err
	}
	defer tx.Rollback()
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return 0, 0, 0, 0, nil, false, err
	}
	if currentPlayer != playerID || pending != 0 || phase != TurnPhaseThrowDice {
		return 0, 0, currentPlayer, round, nil, false, nil
	}
	if err = tx.QueryRowContext(ctx, `SELECT hp FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldHP); err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	if oldHP > 0 {
		return oldHP, oldHP, playerID, round, nil, false, nil
	}
	newHP = maxHP
	var buffsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT battle_buffs_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&buffsJSON); err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	var buffs []BuffState
	if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
		return 0, 0, 0, round, nil, false, fmt.Errorf("decode battle buffs for player %d: %w", playerID, err)
	}
	clearSet := make(map[int32]struct{}, len(deathClearBuffIDs))
	for _, buffID := range deathClearBuffIDs {
		clearSet[buffID] = struct{}{}
	}
	remainingBuffs := make([]BuffState, 0, len(buffs))
	for _, buff := range buffs {
		if _, clear := clearSet[buff.BuffID]; clear {
			removedBuffs = append(removedBuffs, buff)
			continue
		}
		remainingBuffs = append(remainingBuffs, buff)
	}
	updatedBuffsJSON, err := json.Marshal(remainingBuffs)
	if err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_buffs_json=? WHERE id=? AND room_id=?`, newHP, string(updatedBuffsJSON), playerID, roomID); err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM game_bombs WHERE room_id=? AND holder_player_id=?`, roomID, playerID); err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	rows, e := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if e != nil {
		return 0, 0, 0, round, nil, false, e
	}
	var members []int64
	for rows.Next() {
		var id int64
		if e = rows.Scan(&id); e != nil {
			rows.Close()
			return 0, 0, 0, round, nil, false, e
		}
		members = append(members, id)
	}
	if e = rows.Err(); e != nil {
		rows.Close()
		return 0, 0, 0, round, nil, false, e
	}
	rows.Close()
	idx := -1
	for i, id := range members {
		if id == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return 0, 0, 0, round, nil, false, errors.New("current player is not in the room")
	}
	wrap := idx == len(members)-1
	nextPlayer = members[(idx+1)%len(members)]
	if wrap {
		round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, nextPlayer, round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseThrowDice)
	if err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	if updated == 0 {
		return 0, 0, 0, round, nil, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return 0, 0, 0, round, nil, false, err
	}
	return oldHP, newHP, nextPlayer, round, removedBuffs, true, nil
}

// ProcessRoundBuffsTurnStart applies poison and expires configured round-count
// buffs at most once for a player in a room round. The marker, HP change, and
// buff countdown share one transaction so SyncRoom cannot repeat an effect.
func (s *Store) ProcessRoundBuffsTurnStart(ctx context.Context, roomID, playerID int64, round, maxHP, poisonDamage int32, roundBuffIDs []int32) (TurnStartEffectResult, bool, error) {
	if round <= 0 {
		return TurnStartEffectResult{}, false, errors.New("turn round must be positive")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return TurnStartEffectResult{}, false, err
	}
	defer tx.Rollback()
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return TurnStartEffectResult{}, false, err
	}
	if currentPlayer != playerID || currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return TurnStartEffectResult{}, false, nil
	}
	marker, err := tx.ExecContext(ctx, `INSERT OR IGNORE INTO turn_start_effects(room_id,round,player_id,created_at) VALUES(?,?,?,?)`, roomID, round, playerID, time.Now().Unix())
	if err != nil {
		return TurnStartEffectResult{}, false, err
	}
	inserted, err := marker.RowsAffected()
	if err != nil {
		return TurnStartEffectResult{}, false, err
	}
	if inserted == 0 {
		return TurnStartEffectResult{}, false, nil
	}
	var result TurnStartEffectResult
	var buffsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT hp,battle_buffs_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldHP, &buffsJSON); err != nil {
		return TurnStartEffectResult{}, false, err
	}
	result.NewHP = result.OldHP
	var buffs []BuffState
	if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
		return TurnStartEffectResult{}, false, fmt.Errorf("decode battle buffs for player %d: %w", playerID, err)
	}
	remaining := make([]BuffState, 0, len(buffs))
	changed := false
	poisonDamageReady := false
	countdownIDs := make(map[int32]struct{}, len(roundBuffIDs))
	for _, buffID := range roundBuffIDs {
		if buffID > 0 {
			countdownIDs[buffID] = struct{}{}
		}
	}
	for _, buff := range buffs {
		if _, configured := countdownIDs[buff.BuffID]; !configured || buff.KeepRound <= 0 {
			remaining = append(remaining, buff)
			continue
		}
		if buff.BuffID == 3000601 && (maxHP <= 0 || poisonDamage <= 0) {
			return TurnStartEffectResult{}, false, errors.New("maximum HP and poison damage must be positive")
		}
		changed = true
		if buff.DelayRound > 0 {
			buff.DelayRound--
			result.UpdatedBuffs = append(result.UpdatedBuffs, buff)
			remaining = append(remaining, buff)
			continue
		}
		if buff.BuffID == 3000601 && result.NewHP > 0 {
			poisonDamageReady = true
		}
		buff.KeepRound--
		if buff.KeepRound == 0 {
			result.RemovedBuffs = append(result.RemovedBuffs, buff)
			continue
		}
		result.UpdatedBuffs = append(result.UpdatedBuffs, buff)
		remaining = append(remaining, buff)
	}
	if poisonDamageReady && result.OldHP > 0 {
		damage, nextBuffs, consumed := ApplyDestinyDamageBuffs(poisonDamage, remaining)
		remaining = nextBuffs
		if len(consumed) > 0 {
			changed = true
			result.RemovedBuffs = append(result.RemovedBuffs, consumed...)
		}
		result.NewHP = result.OldHP - damage
		if result.NewHP < 0 {
			result.NewHP = 0
		}
	}
	if changed {
		encoded, marshalErr := json.Marshal(remaining)
		if marshalErr != nil {
			return TurnStartEffectResult{}, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_buffs_json=? WHERE id=? AND room_id=?`, result.NewHP, string(encoded), playerID, roomID); err != nil {
			return TurnStartEffectResult{}, false, err
		}
	}
	if err = tx.Commit(); err != nil {
		return TurnStartEffectResult{}, false, err
	}
	return result, true, nil
}

// CompleteRollGold records the result and releases the pending land action in
// one transaction, so a reconnect or duplicate request cannot award it twice.
func (s *Store) CompleteRollGold(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, amount int32) (oldGold, newGold int32, nextPlayer int64, round int32, created bool, err error) {
	if amount < 0 {
		return 0, 0, 0, 0, false, errors.New("roll-gold amount must not be negative")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, 0, 0, 0, false, err
	}
	defer tx.Rollback()

	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return 0, 0, 0, 0, false, err
		}
		if existing > 0 {
			return 0, 0, 0, 0, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return 0, 0, 0, 0, false, err
	}
	if currentPlayer != playerID {
		return 0, 0, 0, round, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != TurnPhaseRollGold {
		return 0, 0, 0, round, false, ErrActionNotReady
	}
	if err = tx.QueryRowContext(ctx, `SELECT gold FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldGold); err != nil {
		return 0, 0, 0, round, false, err
	}
	const maxInt32 = int32(1<<31 - 1)
	if amount > 0 && oldGold > maxInt32-amount {
		return 0, 0, 0, round, false, errors.New("RollGold reward would overflow player gold")
	}
	newGold = oldGold + amount
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return 0, 0, 0, round, false, e
		}
		n, e := res.RowsAffected()
		if e != nil {
			return 0, 0, 0, round, false, e
		}
		if n == 0 {
			return 0, 0, 0, round, false, nil
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET gold=? WHERE id=? AND room_id=?`, newGold, playerID, roomID); err != nil {
		return 0, 0, 0, round, false, err
	}

	rows, e := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if e != nil {
		return 0, 0, 0, round, false, e
	}
	var members []int64
	for rows.Next() {
		var id int64
		if e = rows.Scan(&id); e != nil {
			rows.Close()
			return 0, 0, 0, round, false, e
		}
		members = append(members, id)
	}
	if e = rows.Err(); e != nil {
		rows.Close()
		return 0, 0, 0, round, false, e
	}
	rows.Close()
	idx := -1
	for i, id := range members {
		if id == playerID {
			idx = i
			break
		}
	}
	if idx < 0 || len(members) == 0 {
		return 0, 0, 0, round, false, errors.New("current player is not in the room")
	}
	wrap := idx == len(members)-1
	nextPlayer = members[(idx+1)%len(members)]
	if wrap {
		round++
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, nextPlayer, round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseRollGold)
	if err != nil {
		return 0, 0, 0, round, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return 0, 0, 0, round, false, err
	}
	if updated == 0 {
		return 0, 0, 0, round, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return 0, 0, 0, round, false, err
	}
	return oldGold, newGold, nextPlayer, round, true, nil
}

func (s *Store) CurrentTurn(ctx context.Context, roomID int64) (int64, int32, int32, error) {
	var playerID int64
	var round, pending int32
	err := s.db.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&playerID, &round, &pending)
	return playerID, round, pending, err
}
func (s *Store) CurrentTurnPhase(ctx context.Context, roomID int64) (string, error) {
	var phase string
	err := s.db.QueryRowContext(ctx, `SELECT turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&phase)
	return phase, err
}
func (s *Store) SetPendingMove(ctx context.Context, roomID int64, value int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE game_sessions SET pending_move=?,turn_phase=? WHERE room_id=? AND status='running'`, value, TurnPhaseMoving, roomID)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("game not running")
	}
	return nil
}
func (s *Store) AdvanceTurn(ctx context.Context, roomID, currentPlayerID int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	rows, err := tx.QueryContext(ctx, `SELECT player_id,slot FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return err
	}
	type seat struct {
		id   int64
		slot int32
	}
	var seats []seat
	for rows.Next() {
		var x seat
		if err = rows.Scan(&x.id, &x.slot); err != nil {
			rows.Close()
			return err
		}
		seats = append(seats, x)
	}
	rows.Close()
	if len(seats) == 0 {
		return errors.New("room has no players")
	}
	idx := -1
	for i, x := range seats {
		if x.id == currentPlayerID {
			idx = i
			break
		}
	}
	if idx < 0 {
		return errors.New("current player not in room")
	}
	next := seats[(idx+1)%len(seats)].id
	wrap := idx == len(seats)-1
	if wrap {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=round+1,pending_move=0,turn_phase=? WHERE room_id=?`, next, TurnPhaseThrowDice, roomID)
	} else {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,pending_move=0,turn_phase=? WHERE room_id=?`, next, TurnPhaseThrowDice, roomID)
	}
	if err != nil {
		return err
	}
	return tx.Commit()
}
func (s *Store) MemberIDs(ctx context.Context, roomID int64) ([]int64, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var ids []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		ids = append(ids, id)
	}
	return ids, rows.Err()
}
func (s *Store) LookupPlayer(ctx context.Context, playerID int64) (Player, error) {
	var p Player
	var lotteriesJSON, frontJSON string
	err := s.db.QueryRowContext(ctx, `SELECT p.id,p.account_id,p.nick,p.level,p.exp,COALESCE(p.slot,0),COALESCE(p.node_id,0),COALESCE(p.back_node_id,0),p.front_node_ids_json,p.gold,p.hp,p.hero_level,COALESCE(p.room_id,0),COALESCE(rm.hero_id,0),p.lotterys_json FROM players p LEFT JOIN room_members rm ON rm.player_id=p.id AND rm.room_id=p.room_id WHERE p.id=?`, playerID).Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &p.Slot, &p.NodeID, &p.BackNodeID, &frontJSON, &p.Gold, &p.HP, &p.HeroLevel, &p.RoomID, &p.HeroID, &lotteriesJSON)
	if err == nil {
		err = json.Unmarshal([]byte(frontJSON), &p.FrontNodeIDs)
	}
	if err == nil {
		err = json.Unmarshal([]byte(lotteriesJSON), &p.Lotterys)
	}
	if err == nil && p.Lotterys == nil {
		p.Lotterys = map[int32]bool{}
	}
	if err == nil {
		err = s.HydratePlayerGameplayData(ctx, &p)
	}
	return p, err
}

func (s *Store) InsertChat(ctx context.Context, roomID, fromPlayer, toPlayer int64, expressionID, shortIndex int32) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO chat_messages(room_id,from_player,to_player,expression_id,short_index,created_at) VALUES(?,?,?,?,?,?)`, roomID, fromPlayer, toPlayer, expressionID, shortIndex, time.Now().Unix())
	return err
}
func (s *Store) ListFriends(ctx context.Context, playerID int64) ([]int64, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT friend_id FROM friendships WHERE player_id=? AND state=1 ORDER BY created_at DESC`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		out = append(out, id)
	}
	return out, rows.Err()
}

type MatchTeam struct {
	ID                      int64
	Mode, MapID, Difficulty int32
	LeaderID                int64
	State                   int32
	CreatedAt               int64
	Players                 []Player
	Ready                   map[int64]bool
}

func (s *Store) CreateMatchTeam(ctx context.Context, p Player, mode, mapID, difficulty int32) (MatchTeam, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return MatchTeam{}, err
	}
	defer tx.Rollback()
	now := time.Now().Unix()
	res, err := tx.ExecContext(ctx, `INSERT INTO match_teams(mode,map_id,difficulty,leader_id,state,created_at) VALUES(?,?,?,?,1,?)`, mode, mapID, difficulty, p.ID, now)
	if err != nil {
		return MatchTeam{}, err
	}
	id, err := res.LastInsertId()
	if err != nil {
		return MatchTeam{}, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO match_team_members(team_id,player_id,ready) VALUES(?,?,1)`, id, p.ID); err != nil {
		return MatchTeam{}, err
	}
	if err = tx.Commit(); err != nil {
		return MatchTeam{}, err
	}
	return s.MatchTeamSnapshot(ctx, id)
}

// MatchTeamForPlayer returns the id of the waiting/matching team the player
// belongs to, or 0 when the player is not in a team.
func (s *Store) MatchTeamForPlayer(ctx context.Context, playerID int64) (int64, error) {
	if playerID <= 0 {
		return 0, nil
	}
	var teamID int64
	err := s.db.QueryRowContext(ctx, `SELECT mtm.team_id FROM match_team_members mtm JOIN match_teams mt ON mt.id=mtm.team_id WHERE mtm.player_id=? AND mt.state IN (1,2) ORDER BY mtm.team_id LIMIT 1`, playerID).Scan(&teamID)
	if err == sql.ErrNoRows {
		return 0, nil
	}
	if err != nil {
		return 0, err
	}
	return teamID, nil
}
func (s *Store) MatchTeamSnapshot(ctx context.Context, id int64) (MatchTeam, error) {
	var t MatchTeam
	var state int32
	err := s.db.QueryRowContext(ctx, `SELECT id,mode,map_id,difficulty,leader_id,state,created_at FROM match_teams WHERE id=?`, id).Scan(&t.ID, &t.Mode, &t.MapID, &t.Difficulty, &t.LeaderID, &state, &t.CreatedAt)
	if err != nil {
		return MatchTeam{}, err
	}
	t.State = state
	t.Ready = make(map[int64]bool)
	rows, err := s.db.QueryContext(ctx, `SELECT p.id,p.account_id,p.nick,p.level,p.exp,mtm.ready,p.node_id,p.gold,p.hp,COALESCE(p.room_id,0) FROM match_team_members mtm JOIN players p ON p.id=mtm.player_id WHERE mtm.team_id=? ORDER BY p.id`, id)
	if err != nil {
		return MatchTeam{}, err
	}
	defer rows.Close()
	for rows.Next() {
		var p Player
		var ready int
		if err = rows.Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &ready, &p.NodeID, &p.Gold, &p.HP, &p.RoomID); err != nil {
			return MatchTeam{}, err
		}
		p.Ready = ready != 0
		t.Players = append(t.Players, p)
		t.Ready[p.ID] = p.Ready
	}
	if err = rows.Err(); err != nil {
		return MatchTeam{}, err
	}
	if err = rows.Close(); err != nil {
		return MatchTeam{}, err
	}
	for i := range t.Players {
		if err = s.HydratePlayerGameplayData(ctx, &t.Players[i]); err != nil {
			return MatchTeam{}, err
		}
	}
	return t, nil
}
func (s *Store) IsMatchTeamMember(ctx context.Context, teamID, playerID int64) (bool, error) {
	var exists int
	err := s.db.QueryRowContext(ctx, `SELECT EXISTS(SELECT 1 FROM match_team_members WHERE team_id=? AND player_id=?)`, teamID, playerID).Scan(&exists)
	return exists == 1, err
}
func (s *Store) JoinMatchTeam(ctx context.Context, id int64, p Player) (MatchTeam, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return MatchTeam{}, err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM match_teams WHERE id=?`, id).Scan(&state); err != nil {
		return MatchTeam{}, err
	}
	if state != 1 {
		return MatchTeam{}, errors.New("team not waiting")
	}
	var n int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM match_team_members WHERE team_id=?`, id).Scan(&n); err != nil {
		return MatchTeam{}, err
	}
	if n >= 4 {
		return MatchTeam{}, errors.New("team full")
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO match_team_members(team_id,player_id,ready) VALUES(?,?,0)`, id, p.ID); err != nil {
		return MatchTeam{}, err
	}
	if err = tx.Commit(); err != nil {
		return MatchTeam{}, err
	}
	return s.MatchTeamSnapshot(ctx, id)
}
func (s *Store) ExitMatchTeam(ctx context.Context, id, playerID int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var leader int64
	if err = tx.QueryRowContext(ctx, `SELECT leader_id FROM match_teams WHERE id=?`, id).Scan(&leader); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM match_team_members WHERE team_id=? AND player_id=?`, id, playerID); err != nil {
		return err
	}
	var n int
	var next int64
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*),COALESCE(MIN(player_id),0) FROM match_team_members WHERE team_id=?`, id).Scan(&n, &next); err != nil {
		return err
	}
	if n == 0 {
		_, err = tx.ExecContext(ctx, `DELETE FROM match_teams WHERE id=?`, id)
	} else if leader == playerID {
		_, err = tx.ExecContext(ctx, `UPDATE match_teams SET leader_id=? WHERE id=?`, next, id)
	}
	if err != nil {
		return err
	}
	return tx.Commit()
}
func (s *Store) SetMatchTeamReady(ctx context.Context, id, playerID int64, ready bool) (MatchTeam, error) {
	v := 0
	if ready {
		v = 1
	}
	res, err := s.db.ExecContext(ctx, `UPDATE match_team_members SET ready=? WHERE team_id=? AND player_id=?`, v, id, playerID)
	if err != nil {
		return MatchTeam{}, err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return MatchTeam{}, errors.New("not a team member")
	}
	return s.MatchTeamSnapshot(ctx, id)
}
func (s *Store) SetMatchTeamState(ctx context.Context, id, leaderID int64, state int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE match_teams SET state=? WHERE id=? AND leader_id=?`, state, id, leaderID)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("not team leader")
	}
	return nil
}

func (s *Store) UpdateMatchTeam(ctx context.Context, id, leaderID int64, mode, mapID, difficulty int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE match_teams SET mode=?,map_id=?,difficulty=? WHERE id=? AND leader_id=? AND state=1`, mode, mapID, difficulty, id, leaderID)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("team not waiting or not leader")
	}
	return nil
}

func (s *Store) SetPlayerNick(ctx context.Context, playerID int64, nick string) error {
	_, err := s.db.ExecContext(ctx, `UPDATE players SET nick=? WHERE id=?`, nick, playerID)
	return err
}
func (s *Store) SetOnlineStatus(ctx context.Context, playerID int64, status int32) error {
	_, err := s.db.ExecContext(ctx, `UPDATE players SET online_status=? WHERE id=?`, status, playerID)
	return err
}
func (s *Store) SetPlayerHarmony(ctx context.Context, playerID int64, isHarmony bool, harmonyType int32) error {
	_, err := s.db.ExecContext(ctx, `UPDATE players SET is_harmony=?, harmony_type=? WHERE id=?`, isHarmony, harmonyType, playerID)
	return err
}
func (s *Store) SetPlayerGoldLobby(ctx context.Context, playerID int64, gold int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE players SET gold=? WHERE id=? AND room_id IS NULL`, gold, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		var exists int
		if err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM players WHERE id=?`, playerID).Scan(&exists); err == nil && exists == 0 {
			return fmt.Errorf("player %d not found", playerID)
		}
		return errors.New("player is in a room; lobby-only update rejected")
	}
	return nil
}
func (s *Store) SetPlayerLevelLobby(ctx context.Context, playerID int64, level int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE players SET level=? WHERE id=? AND room_id IS NULL`, level, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		var exists int
		if err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM players WHERE id=?`, playerID).Scan(&exists); err == nil && exists == 0 {
			return fmt.Errorf("player %d not found", playerID)
		}
		return errors.New("player is in a room; lobby-only update rejected")
	}
	return nil
}

// AdminDeletePlayer removes a player together with its account. Foreign keys
// are enabled, so every child row that references the player is deleted first.
// Tables or columns absent from the current schema are skipped on purpose.
func (s *Store) AdminDeletePlayer(ctx context.Context, playerID int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var accountID int64
	if err := tx.QueryRowContext(ctx, `SELECT account_id FROM players WHERE id=?`, playerID).Scan(&accountID); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return errors.New("player not found")
		}
		return err
	}
	children := []struct {
		table   string
		columns []string
	}{
		{"game_player_stats", []string{"player_id"}},
		{"turn_start_effects", []string{"player_id"}},
		{"game_card_turns", []string{"player_id"}},
		{"game_skill_cooldowns", []string{"player_id"}},
		{"game_skill_event_offers", []string{"player_id"}},
		{"game_bombs", []string{"owner_player_id", "holder_player_id"}},
		{"game_shops", []string{"player_id"}},
		{"game_divinations", []string{"player_id"}},
		{"gacha_draws", []string{"player_id"}},
		{"gacha_records", []string{"player_id"}},
		{"gacha_progress", []string{"player_id"}},
		{"player_sign_in", []string{"player_id"}},
		{"player_pve_heroes", []string{"player_id"}},
		{"task_rewards", []string{"player_id"}},
		{"task_progress", []string{"player_id"}},
		{"task_counters", []string{"player_id"}},
		{"inventory", []string{"player_id"}},
		{"mail", []string{"player_id"}},
		{"chat_messages", []string{"player_id"}},
		{"chat_history_cursors", []string{"owner_id", "target_player_id"}},
		{"recent_players", []string{"player_id", "target_player_id"}},
		{"room_invites", []string{"inviter_id", "invitee_id"}},
		{"match_team_members", []string{"player_id"}},
		{"friend_blocks", []string{"player_id", "target_player_id"}},
		{"friend_requests", []string{"player_id", "target_player_id"}},
		{"friendships", []string{"player_id", "target_player_id"}},
		{"room_members", []string{"player_id"}},
	}
	for _, child := range children {
		for _, column := range child.columns {
			if _, err := tx.ExecContext(ctx, "DELETE FROM "+child.table+" WHERE "+column+"=?", playerID); err != nil {
				continue
			}
		}
	}
	if _, err := tx.ExecContext(ctx, `UPDATE rooms SET master_id=0 WHERE master_id=?`, playerID); err != nil {
		// rooms.master_id may be NOT NULL in older schemas; the account delete
		// still succeeds because rooms does not reference players with a
		// cascading constraint.
	}
	if _, err := tx.ExecContext(ctx, `DELETE FROM auth_sessions WHERE account_id=?`, accountID); err != nil {
		return err
	}
	if _, err := tx.ExecContext(ctx, `DELETE FROM players WHERE id=?`, playerID); err != nil {
		return err
	}
	if _, err := tx.ExecContext(ctx, `DELETE FROM accounts WHERE id=?`, accountID); err != nil {
		return err
	}
	return tx.Commit()
}
func (s *Store) ApplyFriend(ctx context.Context, requester, target int64) error {
	if requester == target {
		return errors.New("cannot friend self")
	}
	var count int
	if err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM players WHERE id=?`, target).Scan(&count); err != nil {
		return err
	}
	if count == 0 {
		return errors.New("player not found")
	}
	blocked, err := s.IsFriendBlocked(ctx, requester, target)
	if err != nil {
		return err
	}
	if blocked {
		return errors.New("friend request blocked")
	}
	_, err = s.db.ExecContext(ctx, `INSERT OR IGNORE INTO friend_requests(requester_id,target_id,state,created_at) VALUES(?,?,0,?)`, requester, target, time.Now().Unix())
	return err
}
func (s *Store) ListFriendRequests(ctx context.Context, target int64) ([]Player, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT p.id,p.account_id,p.nick,p.level,p.exp,COALESCE(p.slot,0),COALESCE(p.node_id,0),p.gold,p.hp,COALESCE(p.room_id,0) FROM friend_requests fr JOIN players p ON p.id=fr.requester_id WHERE fr.target_id=? AND fr.state=0 ORDER BY fr.created_at DESC`, target)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []Player
	for rows.Next() {
		var p Player
		if err = rows.Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &p.Slot, &p.NodeID, &p.Gold, &p.HP, &p.RoomID); err != nil {
			return nil, err
		}
		out = append(out, p)
	}
	return out, rows.Err()
}
func (s *Store) AcceptFriend(ctx context.Context, requester, target int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var n int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM friend_requests WHERE requester_id=? AND target_id=? AND state=0`, requester, target).Scan(&n); err != nil {
		return err
	}
	if n == 0 {
		return errors.New("friend request not found")
	}
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM friend_blocks WHERE (player_id=? AND blocked_id=?) OR (player_id=? AND blocked_id=?)`, requester, target, target, requester).Scan(&n); err != nil {
		return err
	}
	if n > 0 {
		return errors.New("friend request blocked")
	}
	now := time.Now().Unix()
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO friendships(player_id,friend_id,state,created_at) VALUES(?,?,1,?)`, requester, target, now); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO friendships(player_id,friend_id,state,created_at) VALUES(?,?,1,?)`, target, requester, now); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM friend_requests WHERE requester_id=? AND target_id=?`, requester, target); err != nil {
		return err
	}
	return tx.Commit()
}

type ChatMessage struct {
	SenderID, ReceiverID int64
	Message              string
	Time                 int64
}

func (s *Store) InsertPrivateChat(ctx context.Context, from, to int64, text string) (int64, error) {
	now := time.Now().Unix()
	res, err := s.db.ExecContext(ctx, `INSERT INTO chat_messages(room_id,from_player,to_player,message,created_at) VALUES(0,?,?,?,?)`, from, to, text, now)
	if err != nil {
		return 0, err
	}
	return res.LastInsertId()
}
func (s *Store) ListPrivateChat(ctx context.Context, a, b int64) ([]ChatMessage, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT m.from_player,m.to_player,m.message,m.created_at FROM chat_messages m
		WHERE m.room_id=0 AND ((m.from_player=? AND m.to_player=?) OR (m.from_player=? AND m.to_player=?))
		AND m.id>COALESCE((SELECT deleted_through_id FROM chat_history_cursors WHERE owner_id=? AND target_player_id=?),0)
		ORDER BY m.id DESC LIMIT 50`, a, b, b, a, a, b)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []ChatMessage
	for rows.Next() {
		var m ChatMessage
		if err = rows.Scan(&m.SenderID, &m.ReceiverID, &m.Message, &m.Time); err != nil {
			return nil, err
		}
		out = append(out, m)
	}
	for i, j := 0, len(out)-1; i < j; i, j = i+1, j-1 {
		out[i], out[j] = out[j], out[i]
	}
	return out, rows.Err()
}
func (s *Store) AreFriends(ctx context.Context, a, b int64) (bool, error) {
	var n int
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM friendships WHERE player_id=? AND friend_id=? AND state=1`, a, b).Scan(&n)
	return n > 0, err
}

func (s *Store) RejectFriend(ctx context.Context, requester, target int64) error {
	res, err := s.db.ExecContext(ctx, `DELETE FROM friend_requests WHERE requester_id=? AND target_id=?`, requester, target)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("friend request not found")
	}
	return nil
}
func (s *Store) ClearIncomingFriendRequests(ctx context.Context, target int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM friend_requests WHERE target_id=?`, target)
	return err
}
func (s *Store) SetFriendNote(ctx context.Context, owner, friend int64, note string) error {
	res, err := s.db.ExecContext(ctx, `UPDATE friendships SET note=? WHERE player_id=? AND friend_id=? AND state=1`, note, owner, friend)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("friend not found")
	}
	return nil
}

func (s *Store) ConsumeItem(ctx context.Context, playerID int64, itemID, count int32) error {
	if count <= 0 {
		return errors.New("invalid item count")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var have int
	if err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&have); err != nil {
		return err
	}
	if have < int(count) {
		return errors.New("item not enough")
	}
	if _, err = tx.ExecContext(ctx, `UPDATE inventory SET count=count-?,updated_at=? WHERE player_id=? AND item_id=?`, count, time.Now().Unix(), playerID, itemID); err != nil {
		return err
	}
	return tx.Commit()
}
func (s *Store) AddItem(ctx context.Context, playerID int64, itemID, count int32) error {
	if count <= 0 {
		return nil
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, itemID, count, time.Now().Unix())
	return err
}
func (s *Store) MarkMailRead(ctx context.Context, pid int64, id int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE mail SET is_read=1 WHERE player_id=? AND mail_id=?`, pid, id)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("mail not found")
	}
	return nil
}
func (s *Store) StarMail(ctx context.Context, pid int64, id int32, star bool) error {
	v := 0
	if star {
		v = 1
	}
	res, err := s.db.ExecContext(ctx, `UPDATE mail SET is_star=? WHERE player_id=? AND mail_id=?`, v, pid, id)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("mail not found")
	}
	return nil
}
func (s *Store) ClaimMail(ctx context.Context, pid int64, id int32) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var rewarded, read int
	var rewards string
	if err = tx.QueryRowContext(ctx, `SELECT is_rewarded,is_read,rewards_json FROM mail WHERE player_id=? AND mail_id=?`, pid, id).Scan(&rewarded, &read, &rewards); err != nil {
		return err
	}
	if rewarded != 0 {
		return errors.New("mail already rewarded")
	}
	if _, err = tx.ExecContext(ctx, `UPDATE mail SET is_rewarded=1,is_read=1 WHERE player_id=? AND mail_id=?`, pid, id); err != nil {
		return err
	}
	_ = rewards
	return tx.Commit()
}
func (s *Store) DeleteReadMail(ctx context.Context, pid int64, id int32) error {
	res, err := s.db.ExecContext(ctx, `DELETE FROM mail WHERE player_id=? AND mail_id=? AND is_read=1`, pid, id)
	if err != nil {
		return err
	}
	n, _ := res.RowsAffected()
	if n == 0 {
		return errors.New("mail not found or unread")
	}
	return nil
}
func (s *Store) ChangeName(ctx context.Context, pid int64, nick string) (int64, error) {
	now := time.Now().Unix()
	var next int64
	if err := s.db.QueryRowContext(ctx, `SELECT next_change_name_at FROM players WHERE id=?`, pid).Scan(&next); err != nil {
		return 0, err
	}
	if next > now {
		return next, errors.New("name cooldown")
	}
	newNext := now + 30*24*60*60
	if _, err := s.db.ExecContext(ctx, `UPDATE players SET nick=?,next_change_name_at=? WHERE id=?`, nick, newNext, pid); err != nil {
		return 0, err
	}
	return newNext, nil
}

func (s *Store) FinishGame(ctx context.Context, roomID, winnerID int64) error {
	_, err := s.FinishGameWithStats(ctx, roomID, winnerID)
	return err
}

// FinishGameWithStats atomically closes a running game and returns the
// authoritative room-scoped combat totals for its final result message.
func (s *Store) FinishGameWithStats(ctx context.Context, roomID, winnerID int64) (map[int64]BattlePlayerStats, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer tx.Rollback()
	var state int32
	if err = tx.QueryRowContext(ctx, `SELECT state FROM rooms WHERE id=?`, roomID).Scan(&state); err != nil {
		return nil, err
	}
	if state != 25 {
		return nil, errors.New("room is not running")
	}
	statsByPlayer := make(map[int64]BattlePlayerStats)
	rows, err := tx.QueryContext(ctx, `SELECT rm.player_id,
		COALESCE(s.kill_count,0),COALESCE(s.total_die,0),COALESCE(s.total_damage,0),
		COALESCE(s.total_injured,0),COALESCE(s.treatment_score,0)
		FROM room_members rm LEFT JOIN game_player_stats s
		ON s.room_id=rm.room_id AND s.player_id=rm.player_id
		WHERE rm.room_id=? ORDER BY rm.slot`, roomID)
	if err != nil {
		return nil, err
	}
	for rows.Next() {
		var playerID int64
		var stats BattlePlayerStats
		if err = rows.Scan(&playerID, &stats.KillCount, &stats.TotalDie,
			&stats.TotalDamage, &stats.TotalInjured, &stats.TreatmentScore); err != nil {
			rows.Close()
			return nil, err
		}
		statsByPlayer[playerID] = stats
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return nil, err
	}
	if err = rows.Close(); err != nil {
		return nil, err
	}
	now := time.Now().Unix()
	result, err := tx.ExecContext(ctx, `UPDATE rooms SET state=30,updated_at=? WHERE id=? AND state=25`, now, roomID)
	if err != nil {
		return nil, err
	}
	if affected, affectedErr := result.RowsAffected(); affectedErr != nil {
		return nil, affectedErr
	} else if affected != 1 {
		return nil, errors.New("room is not running")
	}
	result, err = tx.ExecContext(ctx, `UPDATE game_sessions SET status='finished',current_player_id=? WHERE room_id=? AND status='running'`, winnerID, roomID)
	if err != nil {
		return nil, err
	}
	if affected, affectedErr := result.RowsAffected(); affectedErr != nil {
		return nil, affectedErr
	} else if affected != 1 {
		return nil, errors.New("game session is not running")
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return statsByPlayer, nil
}

func (s *Store) EnsureStarterItem(ctx context.Context, playerID int64, itemID, count int32) error {
	if count <= 0 {
		return nil
	}
	_, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?)`, playerID, itemID, count, time.Now().Unix())
	return err
}
