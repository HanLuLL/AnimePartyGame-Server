package gdconf

import (
	"encoding/json"
	"fmt"
	"io/fs"
	"os"
	"path/filepath"
	"sort"
	"strconv"
	"strings"
	"sync"
)

type Store struct {
	mu     sync.RWMutex
	rows   map[string]map[int64]json.RawMessage
	order  map[string][]int64
	tables int
	root   string
}

// LandNode is the server-relevant part of a serialized UnitLand from the
// client's Addressables scene. IDs and neighbor lists are authoritative map
// topology; visual transforms stay client-side.
type LandNode struct {
	ID                 int32         `json:"id"`
	LandType           int32         `json:"landType"`
	InitDirLandID      int32         `json:"initDirLandId"`
	PlayerSerialNumber int32         `json:"playerSerialNumber"`
	JumpNodeID         int32         `json:"jumpNodeId"`
	Gimmicks           []LandGimmick `json:"gimmicks"`
	NeighborLandIDs    []int32       `json:"neighborLandIds"`
	Disabled           bool          `json:"disabled"`
	RuntimeLandType    int32         `json:"runtimeLandType"`
	ObjectName         string        `json:"objectName,omitempty"`
}

type LandGimmick struct {
	GroupID  int32 `json:"groupId"`
	StepSize int32 `json:"stepSize"`
}

type BoardGraph struct {
	ID        int32      `json:"id"`
	AssetName string     `json:"assetName"`
	Nodes     []LandNode `json:"nodes"`
}

type UpgradeItem struct {
	Star int32 `json:"star"`
	Gold int32 `json:"gold"`
}

// BattleCardInfo contains the resource fields used by the PvP battle card
// phase. Other card effects are resolved by the regular card flow.
type BattleCardInfo struct {
	EffectType int32
	Cost       int32
	MinBonus   int32
	MaxBonus   int32
}

type ControlMoveCardInfo struct {
	MaxPoint           int32
	SkillCooldownDelta int32
	UseCardLimitDelta  int32
}

// DirectDamageCardInfo describes the small, target-one damage cards whose
// complete selection contract is visible in the client and Card_infos.
type DirectDamageCardInfo struct {
	Range  int32
	Damage int32
}

type AsymmetricalRules struct {
	DefenderWinRound int32 `json:"asymmetricalDefenderWinRound"`
	AttackerWinScore int32 `json:"asymmetricalAttackerWinScore"`
	SpeedRound       int32 `json:"asymmetricalSpeedRound"`
}

func Load(root string) (*Store, error) {
	s := &Store{rows: make(map[string]map[int64]json.RawMessage), order: make(map[string][]int64), root: root}
	if root == "" {
		return s, nil
	}
	err := filepath.WalkDir(root, func(path string, d fs.DirEntry, err error) error {
		if err != nil {
			return err
		}
		if d.IsDir() || !strings.EqualFold(filepath.Ext(path), ".json") {
			return nil
		}
		b, e := os.ReadFile(path)
		if e != nil {
			return e
		}
		var values []json.RawMessage
		if json.Unmarshal(b, &values) != nil {
			var wrapped map[string]json.RawMessage
			if json.Unmarshal(b, &wrapped) != nil {
				return nil
			}
			for _, key := range []string{"rows", "items", "data", "Infos", "infos"} {
				if raw, ok := wrapped[key]; ok && json.Unmarshal(raw, &values) == nil {
					break
				}
			}
		}
		if len(values) == 0 {
			return nil
		}
		name := strings.TrimSuffix(filepath.Base(path), filepath.Ext(path))
		idx := make(map[int64]json.RawMessage, len(values))
		ids := make([]int64, 0, len(values))
		for _, raw := range values {
			var row map[string]json.RawMessage
			if json.Unmarshal(raw, &row) != nil {
				continue
			}
			var id int64
			for _, key := range []string{"id", "Id", "ID"} {
				if r, ok := row[key]; ok {
					_ = json.Unmarshal(r, &id)
					break
				}
			}
			if id == 0 {
				if field := resourceIDField(name); field != "" {
					if value, ok := row[field]; ok {
						if json.Unmarshal(value, &id) != nil {
							var typed struct {
								Value int64 `json:"value"`
							}
							_ = json.Unmarshal(value, &typed)
							id = typed.Value
						}
					}
				}
			}
			if id == 0 {
				for _, typeKey := range []string{
					"landType", "divinationTargetType", "mapModeType", "choosingTimeType", "gameSpeedType",
					"gameDifficultyType",
				} {
					rawType, ok := row[typeKey]
					if !ok {
						continue
					}
					var landType struct {
						Value int64
					}
					if json.Unmarshal(rawType, &landType) == nil {
						id = landType.Value
						break
					}
				}
			}
			if id != 0 {
				if _, exists := idx[id]; !exists {
					ids = append(ids, id)
				}
				idx[id] = raw
			}
		}
		if len(idx) > 0 {
			s.rows[name] = idx
			s.rows[strings.ToLower(name)] = idx
			s.order[name] = ids
			s.order[strings.ToLower(name)] = ids
			s.tables++
		}
		return nil
	})
	if err != nil && !os.IsNotExist(err) {
		return nil, err
	}
	return s, nil
}

func resourceIDField(table string) string {
	switch strings.ToLower(table) {
	case "gacha_backstages":
		return "gachaType"
	case "gacha_pools":
		return "poolID"
	case "gacha_combs":
		return "combID"
	case "gacha_groups":
		return "groupID"
	case "gacha_progresss":
		return "poolID"
	default:
		return ""
	}
}

func (s *Store) Available() bool { s.mu.RLock(); defer s.mu.RUnlock(); return s.tables > 0 }
func (s *Store) TableCount() int { s.mu.RLock(); defer s.mu.RUnlock(); return s.tables }
func (s *Store) Get(table string, id int64) (json.RawMessage, bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	idx, ok := s.rows[table]
	if !ok {
		idx, ok = s.rows[strings.ToLower(table)]
	}
	if !ok {
		return nil, false
	}
	v, ok := idx[id]
	return v, ok
}

// IDs returns the configured row IDs in a stable order. Keeping this in the
// resource layer avoids gameplay code depending on Go's randomized map order.
func (s *Store) IDs(table string) []int64 {
	s.mu.RLock()
	defer s.mu.RUnlock()
	idx, ok := s.rows[table]
	if !ok {
		idx, ok = s.rows[strings.ToLower(table)]
	}
	if !ok {
		return nil
	}
	ids := make([]int64, 0, len(idx))
	for id := range idx {
		ids = append(ids, id)
	}
	sort.Slice(ids, func(i, j int) bool { return ids[i] < ids[j] })
	return ids
}

// DefaultFashionPlan mirrors the first item in each client Fashion config
// list and the row marked current in Fashion_kVs. The fallback IDs belong to
// the bundled 3.2.0 resources and keep the login snapshot renderable when a
// minimal resource fixture omits cosmetic tables.
func (s *Store) DefaultFashionPlan() map[int32]int32 {
	defaults := map[int32]struct {
		table string
		id    int32
	}{
		1: {table: "Fashion_accountHeadShots", id: 70001},
		2: {table: "Fashion_accountBackgrounds", id: 71001},
		3: {table: "Fashion_cardBacks", id: 75001},
		4: {table: "Fashion_dices", id: 72099},
		5: {table: "Fashion_effects", id: 73001},
		6: {table: "Fashion_kVs", id: 76129},
	}
	plan := make(map[int32]int32, len(defaults))
	for slot, fallback := range defaults {
		if slot == 6 {
			if id, ok := s.currentID(fallback.table, "current"); ok {
				plan[slot] = id
				continue
			}
		} else if id, ok := s.firstID(fallback.table); ok {
			plan[slot] = id
			continue
		}
		plan[slot] = fallback.id
	}
	return plan
}

// FashionSlotForItem maps the account Item_infos category to the fashion slot
// accepted by SetFashionC2S. Slot IDs follow the client's ShowingFashion map.
func (s *Store) FashionSlotForItem(itemID int32) (int32, bool) {
	raw, ok := s.Get("Item_infos", int64(itemID))
	if !ok {
		return 0, false
	}
	var row map[string]json.RawMessage
	if json.Unmarshal(raw, &row) != nil {
		return 0, false
	}
	encodedType, ok := row["itemType"]
	if !ok {
		return 0, false
	}
	var itemType int32
	if json.Unmarshal(encodedType, &itemType) != nil {
		var enum struct {
			Value int32 `json:"value"`
		}
		if json.Unmarshal(encodedType, &enum) != nil {
			return 0, false
		}
		itemType = enum.Value
	}
	switch itemType {
	case 9:
		return 1, true // account headshot
	case 10:
		return 2, true // account background
	case 14:
		return 3, true // card back
	case 11:
		return 4, true // dice
	case 22:
		return 5, true // effect
	case 32:
		return 6, true // home KV
	default:
		return 0, false
	}
}

func (s *Store) firstID(table string) (int32, bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	ids, ok := s.order[table]
	if !ok {
		ids = s.order[strings.ToLower(table)]
	}
	if len(ids) == 0 || ids[0] > int64(^uint32(0)>>1) {
		return 0, false
	}
	return int32(ids[0]), true
}

func (s *Store) currentID(table, field string) (int32, bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	ids, ok := s.order[table]
	if !ok {
		ids = s.order[strings.ToLower(table)]
	}
	rows := s.rows[table]
	if rows == nil {
		rows = s.rows[strings.ToLower(table)]
	}
	for _, id := range ids {
		var row map[string]json.RawMessage
		if json.Unmarshal(rows[id], &row) != nil {
			continue
		}
		var enabled bool
		if json.Unmarshal(row[field], &enabled) == nil && enabled && id <= int64(^uint32(0)>>1) {
			return int32(id), true
		}
	}
	return 0, false
}
func (s *Store) ValidateMap(mapID int64) (known, dataLoaded bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	dataLoaded = false
	for _, name := range []string{"Map_infos", "MapInfoConfigure", "Map_infos.json"} {
		idx, ok := s.rows[name]
		if !ok {
			idx, ok = s.rows[strings.ToLower(name)]
		}
		if ok {
			dataLoaded = true
			_, found := idx[mapID]
			return found, true
		}
	}
	return false, false
}
func (s *Store) ReadRaw(table string, id int64) (map[string]json.RawMessage, error) {
	raw, ok := s.Get(table, id)
	if !ok {
		return nil, fmt.Errorf("resource %s/%d not found", table, id)
	}
	var row map[string]json.RawMessage
	if err := json.Unmarshal(raw, &row); err != nil {
		return nil, err
	}
	return row, nil
}

// LandParams returns the configured parameters for a land type. Land_infos
// rows use landType.value as their key instead of an id field.
func (s *Store) LandParams(landType int32) ([]int32, bool) {
	raw, ok := s.Get("Land_infos", int64(landType))
	if !ok {
		return nil, false
	}
	var row struct {
		Params []int32
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Params) == 0 {
		return nil, false
	}
	return row.Params, true
}

// UpgradeItems returns the ordered station-upgrade prices and target levels
// from the selected room upgrade plan.
func (s *Store) UpgradeItems(planID int32) ([]UpgradeItem, bool) {
	raw, ok := s.Get("Upgrade_datas", int64(planID))
	if !ok {
		return nil, false
	}
	var row struct {
		Items []UpgradeItem `json:"upgradeDataConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Items) == 0 {
		return nil, false
	}
	return row.Items, true
}

// MapEventIDs returns the map-event references embedded in a loaded Map_infos row.
// It is intentionally empty when table rows are not provided; the server never invents IDs.
func (s *Store) MapEventIDs(mapID int64) []int32 {
	raw, ok := s.Get("Map_infos", mapID)
	if !ok {
		return nil
	}
	var row map[string]json.RawMessage
	if json.Unmarshal(raw, &row) != nil {
		return nil
	}
	for _, key := range []string{"mapeventIDs", "mapEventIDs", "map_event_ids"} {
		if b, exists := row[key]; exists {
			var ids []int32
			if json.Unmarshal(b, &ids) == nil {
				return ids
			}
		}
	}
	return nil
}

// EventPoolForMapMode resolves the event pool selected by the client map
// library. The pool key is mapID*1000+mode (for example, map 81003 in
// Standard mode 1 uses key 81003001); EventPool then points to Event_periods.
// This returns configuration only and does not imply that an event's gameplay
// effect has been implemented.
func (s *Store) EventPoolForMapMode(mapID int64, mode int32) (int32, bool) {
	if mapID <= 0 || mode < 0 {
		return 0, false
	}
	raw, ok := s.Get("Map_mapPools", mapID*1000+int64(mode))
	if !ok {
		return 0, false
	}
	var row struct {
		EventPool int32 `json:"eventPool"`
	}
	if json.Unmarshal(raw, &row) != nil || row.EventPool <= 0 {
		return 0, false
	}
	return row.EventPool, true
}

// MapProgressLimit returns the PVE progress limit for a map and difficulty
// index. The default difficulty omits its zero-valued index in the resource.
func (s *Store) MapProgressLimit(mapID int64, difficulty int32) (int32, bool) {
	if mapID <= 0 || difficulty < 0 {
		return 0, false
	}
	raw, ok := s.Get("Map_gameDifficultys", mapID)
	if !ok {
		return 0, false
	}
	var row struct {
		Items []struct {
			Index         int32 `json:"index"`
			ProgressLimit int32 `json:"progressLimit"`
		} `json:"mapGameDifficultyConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return 0, false
	}
	for _, item := range row.Items {
		if item.Index == difficulty && item.ProgressLimit > 0 {
			return item.ProgressLimit, true
		}
	}
	return 0, false
}

// AsymmetricalBattleRules loads the shared attacker/defender thresholds used
// by asymmetrical matches. Gift pickup counts remain client constants in this
// build, so callers derive the stage from SpeedRound and the decompiled rules.
func (s *Store) AsymmetricalBattleRules() (AsymmetricalRules, bool) {
	raw, ok := s.Get("GameMode_asymmetricalBattles", 1)
	if !ok {
		return AsymmetricalRules{}, false
	}
	var rules AsymmetricalRules
	if json.Unmarshal(raw, &rules) != nil || rules.DefenderWinRound <= 0 || rules.AttackerWinScore <= 0 {
		return AsymmetricalRules{}, false
	}
	return rules, true
}

// CardPoolsForMapMode returns the card pools selected by Map_mapPools for a
// map and mode. The key uses the same mapID*1000+mode convention as event pools.
func (s *Store) CardPoolsForMapMode(mapID int64, mode int32) (effectPool, battlePool int32, ok bool) {
	if mapID <= 0 || mode < 0 {
		return 0, 0, false
	}
	poolMode := mode
	raw, ok := s.Get("Map_mapPools", mapID*1000+int64(poolMode))
	if !ok {
		// Practice modes reuse the standard card pools for the same maps;
		// novice mode uses the novice map's standard pool. The resource bundle
		// has no rows for these mode IDs, but has matching base-mode rows.
		switch mode {
		case 2, 8:
			poolMode = 1
		case 9:
			poolMode = 4
		default:
			return 0, 0, false
		}
		raw, ok = s.Get("Map_mapPools", mapID*1000+int64(poolMode))
	}
	if !ok {
		return 0, 0, false
	}
	var row struct {
		EffectCardPool int32 `json:"effectCardPool"`
		BattleCardPool int32 `json:"battleCardPool"`
	}
	if json.Unmarshal(raw, &row) != nil || row.EffectCardPool <= 0 || row.BattleCardPool <= 0 {
		return 0, 0, false
	}
	return row.EffectCardPool, row.BattleCardPool, true
}

// CardInHandLimit returns the hand limit configured for a game mode.
func (s *Store) CardInHandLimit(mode int32) (int32, bool) {
	for _, raw := range s.Rows("GameMode_infos") {
		var row struct {
			MapModeType struct {
				Value int32 `json:"value"`
			} `json:"mapModeType"`
			Limit int32 `json:"cardInHandLimit"`
		}
		if json.Unmarshal(raw, &row) == nil && row.MapModeType.Value == mode && row.Limit > 0 {
			return row.Limit, true
		}
	}
	return 0, false
}

// GlobalInt reads a numeric setting from the Global_datas resource table.
func (s *Store) GlobalInt(key string) (int32, bool) {
	for _, raw := range s.Rows("Global_datas") {
		var row struct {
			Key   string `json:"key"`
			Value string `json:"value"`
		}
		if json.Unmarshal(raw, &row) != nil || row.Key != key {
			continue
		}
		value, err := strconv.ParseInt(row.Value, 10, 32)
		if err != nil {
			return 0, false
		}
		return int32(value), true
	}
	return 0, false
}

// CampaignInitialGold returns the resource-defined starting gold for a
// campaign slot. Slot zero is the protagonist; later slots use InitialGold.
func (s *Store) CampaignInitialGold(levelID int64, mode, slot int32) (int32, bool) {
	if (mode != 5 && mode != 6 && mode != 10) || levelID <= 0 || slot < 0 {
		return 0, false
	}
	raw, ok := s.Get("Campaign_levels", levelID)
	if !ok {
		return 0, false
	}
	var row struct {
		MapModeType struct {
			Value int32 `json:"value"`
		} `json:"mapModeType"`
		ProtagonistInitialGold int32   `json:"protagonistInitialGold"`
		InitialGold            []int32 `json:"initialGold"`
	}
	if json.Unmarshal(raw, &row) != nil || row.MapModeType.Value != mode {
		return 0, false
	}
	if slot == 0 {
		return row.ProtagonistInitialGold, true
	}
	index := int(slot - 1)
	if index >= len(row.InitialGold) {
		return 0, false
	}
	return row.InitialGold[index], true
}

// CampaignInitialCardIDs returns configured protagonist or hero-specific
// starter cards for a campaign level. Other campaign players use their map pool.
func (s *Store) CampaignInitialCardIDs(levelID int64, mode, heroID, slot int32) ([]int32, bool) {
	if (mode != 5 && mode != 6 && mode != 10) || levelID <= 0 || slot < 0 {
		return nil, false
	}
	raw, ok := s.Get("Campaign_levels", levelID)
	if !ok {
		return nil, false
	}
	var row struct {
		MapModeType struct {
			Value int32 `json:"value"`
		} `json:"mapModeType"`
		ProtagonistInitialCard []int32 `json:"protagonistInitialCard"`
		InitialCard            map[string]struct {
			Values []int32 `json:"values"`
		} `json:"initialCard"`
	}
	if json.Unmarshal(raw, &row) != nil || row.MapModeType.Value != mode {
		return nil, false
	}
	if slot == 0 && len(row.ProtagonistInitialCard) > 0 {
		return row.ProtagonistInitialCard, true
	}
	if cards, exists := row.InitialCard[strconv.FormatInt(int64(heroID), 10)]; exists && len(cards.Values) > 0 {
		return cards.Values, true
	}
	return nil, false
}

// EventIDsForPool returns the configured event IDs in their resource order.
// Callers must still validate each event's effect before resolving a live
// game action; Event_periods is a candidate list, not a rules implementation.
func (s *Store) EventIDsForPool(poolID int32) ([]int32, bool) {
	if poolID <= 0 {
		return nil, false
	}
	raw, ok := s.Get("Event_periods", int64(poolID))
	if !ok {
		return nil, false
	}
	var row struct {
		Items []struct {
			EventID int32 `json:"eventId"`
		} `json:"eventPeriodConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Items) == 0 {
		return nil, false
	}
	ids := make([]int32, 0, len(row.Items))
	for _, item := range row.Items {
		if item.EventID > 0 {
			ids = append(ids, item.EventID)
		}
	}
	return ids, len(ids) > 0
}

// EventInfoExists verifies that a configured candidate has a corresponding
// Event_infos row without interpreting its parameters or effects.
func (s *Store) EventInfoExists(eventID int32) bool {
	_, ok := s.Get("Event_infos", int64(eventID))
	return ok
}

// EventParams returns the configured numeric parameters for a land event.
func (s *Store) EventParams(eventID int32) ([]int32, bool) {
	if eventID <= 0 {
		return nil, false
	}
	raw, ok := s.Get("Event_infos", int64(eventID))
	if !ok {
		return nil, false
	}
	var row struct {
		Params []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return nil, false
	}
	return row.Params, true
}

func (s *Store) EventBuffIDs(eventID int32) ([]int32, bool) {
	if eventID <= 0 {
		return nil, false
	}
	raw, ok := s.Get("Event_infos", int64(eventID))
	if !ok {
		return nil, false
	}
	var row struct {
		BuffIDs []int32 `json:"buffIds"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.BuffIDs) == 0 {
		return nil, false
	}
	return row.BuffIDs, true
}

func (s *Store) BuffTiming(buffID int32) (keepRound, delayRound int32, ok bool) {
	if buffID <= 0 {
		return 0, 0, false
	}
	raw, ok := s.Get("Buff_infos", int64(buffID))
	if !ok {
		return 0, 0, false
	}
	var row struct {
		KeepRound  int32 `json:"keepRound"`
		DelayRound int32 `json:"delayRound"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return 0, 0, false
	}
	return row.KeepRound, row.DelayRound, true
}

// BuffRoundCountType returns the configured clock used to expire a buff.
// Enum values are serialized as objects with a numeric value in resources.
func (s *Store) BuffRoundCountType(buffID int32) (int32, bool) {
	if buffID <= 0 {
		return 0, false
	}
	raw, ok := s.Get("Buff_infos", int64(buffID))
	if !ok {
		return 0, false
	}
	var row struct {
		RoundCountType struct {
			Value int32 `json:"value"`
		} `json:"buffRoundCountType"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return 0, false
	}
	return row.RoundCountType.Value, true
}

func (s *Store) BuffClearsOnDeath(buffID int32) bool {
	if buffID <= 0 {
		return false
	}
	raw, ok := s.Get("Buff_infos", int64(buffID))
	if !ok {
		return false
	}
	var row struct {
		IsDeathClear bool `json:"isDeathClear"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return false
	}
	return row.IsDeathClear
}

// BattleCardIDs returns card IDs in the configured battle pool order.
func (s *Store) BattleCardIDs(poolID int32) ([]int32, bool) {
	if poolID <= 0 {
		return nil, false
	}
	raw, ok := s.Get("Card_battlePools", int64(poolID))
	if !ok {
		return nil, false
	}
	var row struct {
		Items []struct {
			CardID int32 `json:"cardId"`
		} `json:"cardBattlePoolConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Items) == 0 {
		return nil, false
	}
	ids := make([]int32, 0, len(row.Items))
	for _, item := range row.Items {
		if item.CardID > 0 {
			ids = append(ids, item.CardID)
		}
	}
	return ids, len(ids) > 0
}

// EffectCardIDs returns card IDs in the configured effect pool order.
func (s *Store) EffectCardIDs(poolID int32) ([]int32, bool) {
	if poolID <= 0 {
		return nil, false
	}
	raw, ok := s.Get("Card_effectPools", int64(poolID))
	if !ok {
		return nil, false
	}
	var row struct {
		Items []struct {
			CardID int32 `json:"cardId"`
		} `json:"cardEffectPoolConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Items) == 0 {
		return nil, false
	}
	ids := make([]int32, 0, len(row.Items))
	for _, item := range row.Items {
		if item.CardID > 0 {
			ids = append(ids, item.CardID)
		}
	}
	return ids, len(ids) > 0
}

// BoardGraphForMap resolves the selected Map_infos row and index through its
// mids reference to Map_scenes, then loads the graph extracted from the matching
// Addressables scene. The returned graph is a copy owned by the caller.
func (s *Store) BoardGraphForMap(mapID int64, mapIndex int32) (BoardGraph, bool) {
	sceneID := mapID
	var fallbackAsset string
	if raw, ok := s.Get("Map_infos", mapID); ok {
		var row struct {
			MIDs          []int32 `json:"mids"`
			MapSceneImage string  `json:"mapSceneImage"`
		}
		if json.Unmarshal(raw, &row) != nil {
			return BoardGraph{}, false
		}
		fallbackAsset = strings.Replace(row.MapSceneImage, "UT_MapScene_", "Map_", 1)
		if mapIndex >= 0 && int(mapIndex) < len(row.MIDs) {
			sceneID = int64(row.MIDs[mapIndex])
		} else {
			sceneID = 0
		}
	}
	if sceneID != 0 {
		if raw, ok := s.Get("Map_boardGraphs", sceneID); ok {
			var graph BoardGraph
			if json.Unmarshal(raw, &graph) == nil && len(graph.Nodes) > 0 {
				return graph, true
			}
		}
	}
	if fallbackAsset != "" {
		for _, raw := range s.Rows("Map_boardGraphs") {
			var graph BoardGraph
			if json.Unmarshal(raw, &graph) == nil && graph.AssetName == fallbackAsset && len(graph.Nodes) > 0 {
				return graph, true
			}
		}
	}
	return BoardGraph{}, false
}

func (s *Store) FirstID(table string) (int64, bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	idx, ok := s.rows[table]
	if !ok {
		idx, ok = s.rows[strings.ToLower(table)]
	}
	if !ok || len(idx) == 0 {
		return 0, false
	}
	var first int64
	for id := range idx {
		if first == 0 || id < first {
			first = id
		}
	}
	return first, true
}

// DefaultHeroIDs reads the starter roster directly from Character_infos. The
// client uses RoleCard presence to mark a hero as owned, so returning this
// curated set keeps the account's initial roster tied to the shipped data.
func (s *Store) DefaultHeroIDs() []int32 {
	rows := s.Rows("Character_infos")
	ids := make([]int32, 0, 4)
	for _, raw := range rows {
		var row struct {
			ID        int32 `json:"id"`
			IsDefault bool  `json:"isDefault"`
			HeroType  struct {
				Value int32 `json:"value"`
			} `json:"heroType"`
		}
		if json.Unmarshal(raw, &row) == nil && row.ID > 0 && row.IsDefault && row.HeroType.Value == 1 {
			ids = append(ids, row.ID)
		}
	}
	sort.Slice(ids, func(i, j int) bool { return ids[i] < ids[j] })
	return ids
}

// IsHero checks that an ID exists in the extracted character configuration and
// is a playable hero, rather than trusting arbitrary client supplied IDs.
func (s *Store) IsHero(heroID int32) bool {
	if heroID <= 0 {
		return false
	}
	raw, ok := s.Get("Character_infos", int64(heroID))
	if !ok {
		return false
	}
	var row struct {
		HeroType struct {
			Value int32 `json:"value"`
		} `json:"heroType"`
	}
	return json.Unmarshal(raw, &row) == nil && row.HeroType.Value == 1
}

// HeroMaxHP returns a playable character's configured base blood value. The
// fallback keeps unselected or unknown characters consistent with the server's
// initial player defaults until a valid hero is assigned.
func (s *Store) HeroMaxHP(heroID int32) int32 {
	if heroID <= 0 {
		return 10
	}
	raw, ok := s.Get("Character_infos", int64(heroID))
	if !ok {
		return 10
	}
	var row struct {
		Blood int32 `json:"blood"`
	}
	if json.Unmarshal(raw, &row) != nil || row.Blood <= 0 {
		return 10
	}
	return row.Blood
}

// HeroCombatAttributes returns the configured base attack and defense values
// for a playable character. Unknown IDs keep the server's safe defaults.
func (s *Store) HeroCombatAttributes(heroID int32) (int32, int32) {
	if heroID <= 0 {
		return 1, 1
	}
	raw, ok := s.Get("Character_infos", int64(heroID))
	if !ok {
		return 1, 1
	}
	var row struct {
		Attack  int32 `json:"attack"`
		Defense int32 `json:"defense"`
	}
	if json.Unmarshal(raw, &row) != nil || row.Attack < 0 || row.Defense < 0 {
		return 1, 1
	}
	return row.Attack, row.Defense
}

// HeroHasPassiveSkill checks the mode-specific passive skill list in the
// character resource. PVE rows can override the normal passive list.
func (s *Store) HeroHasPassiveSkill(heroID, skillID int32, pve bool) bool {
	if heroID <= 0 || skillID <= 0 {
		return false
	}
	raw, ok := s.Get("Character_infos", int64(heroID))
	if !ok {
		return false
	}
	var row struct {
		PassiveSkills    []int32 `json:"passiveSkills"`
		PVEPassiveSkills []int32 `json:"pvePassiveSkills"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return false
	}
	skills := row.PassiveSkills
	if pve && len(row.PVEPassiveSkills) > 0 {
		skills = row.PVEPassiveSkills
	}
	for _, id := range skills {
		if id == skillID {
			return true
		}
	}
	return false
}

// HeroPassiveSkillParams returns the configured parameters for a passive that
// is active in the selected mode. The returned slice is copied so callers
// cannot mutate the shared resource data.
func (s *Store) HeroPassiveSkillParams(heroID, skillID int32, pve bool) []int32 {
	if !s.HeroHasPassiveSkill(heroID, skillID, pve) {
		return nil
	}
	raw, ok := s.Get("Skill_infos", int64(skillID))
	if !ok {
		return nil
	}
	var row struct {
		Params []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil || len(row.Params) == 0 {
		return nil
	}
	return append([]int32(nil), row.Params...)
}

// BattleCardInfo returns the configured attack or defense value for a battle
// card. Cards with non-combat effects are intentionally excluded.
func (s *Store) BattleCardInfo(cardID int32) (BattleCardInfo, bool) {
	if cardID <= 0 {
		return BattleCardInfo{}, false
	}
	raw, ok := s.Get("Card_infos", int64(cardID))
	if !ok {
		return BattleCardInfo{}, false
	}
	var row struct {
		Cost       int32 `json:"cost"`
		EffectType struct {
			Value int32 `json:"value"`
		} `json:"effectType"`
		Params []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil || (row.EffectType.Value != 1 && row.EffectType.Value != 2) || row.Cost < 0 || len(row.Params) < 2 || row.Params[0] < 0 || row.Params[1] < row.Params[0] {
		return BattleCardInfo{}, false
	}
	return BattleCardInfo{EffectType: row.EffectType.Value, Cost: row.Cost, MinBonus: row.Params[0], MaxBonus: row.Params[1]}, true
}

// EffectCardHealAmount returns the configured amount for a targetless, playable
// self-heal effect card. The client dump confirms these cards use the normal
// UseEffectCard request path; resource metadata supplies their heal value.
func (s *Store) EffectCardHealAmount(cardID int32) (int32, bool) {
	if cardID <= 0 {
		return 0, false
	}
	raw, ok := s.Get("Card_infos", int64(cardID))
	if !ok {
		return 0, false
	}
	var row struct {
		EffectType struct {
			Value int32 `json:"value"`
		} `json:"effectType"`
		CardType struct {
			Value int32 `json:"value"`
		} `json:"cardType"`
		TargetType struct {
			Value int32 `json:"value"`
		} `json:"cardTargetType"`
		Playable bool    `json:"isPlayable"`
		Params   []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil || row.EffectType.Value != 4 || row.CardType.Value != 3 || row.TargetType.Value != 1 || !row.Playable || len(row.Params) == 0 || row.Params[0] <= 0 {
		return 0, false
	}
	return row.Params[0], true
}

// EffectCardControlMoveInfo exposes the two resource-backed cards whose
// client action opens the controlled-movement point picker (Action 5067).
func (s *Store) EffectCardControlMoveInfo(cardID int32) (ControlMoveCardInfo, bool) {
	if cardID != 20005 && cardID != 20032 {
		return ControlMoveCardInfo{}, false
	}
	raw, ok := s.Get("Card_infos", int64(cardID))
	if !ok {
		return ControlMoveCardInfo{}, false
	}
	var row struct {
		EffectType struct {
			Value int32 `json:"value"`
		} `json:"effectType"`
		CardType struct {
			Value int32 `json:"value"`
		} `json:"cardType"`
		TargetType struct {
			Value int32 `json:"value"`
		} `json:"cardTargetType"`
		Playable bool    `json:"isPlayable"`
		Params   []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil || row.EffectType.Value != 7 || row.CardType.Value != 3 || row.TargetType.Value != 1 || !row.Playable || len(row.Params) == 0 || row.Params[0] < 1 || row.Params[0] > 6 {
		return ControlMoveCardInfo{}, false
	}
	info := ControlMoveCardInfo{MaxPoint: row.Params[0]}
	if cardID == 20032 {
		if len(row.Params) < 3 || row.Params[1] > 0 || row.Params[2] < 0 {
			return ControlMoveCardInfo{}, false
		}
		info.SkillCooldownDelta = row.Params[1]
		info.UseCardLimitDelta = row.Params[2]
	}
	return info, true
}

// EffectCardDirectDamageInfo exposes only the three baseline direct-damage
// cards whose client handlers and resource rows agree on one hostile player
// target, graph range, and fixed damage. Cards with any additional effect stay
// outside this path until their server-side rules are recovered.
func (s *Store) EffectCardDirectDamageInfo(cardID int32) (DirectDamageCardInfo, bool) {
	switch cardID {
	case 20001, 20012, 20013:
	default:
		return DirectDamageCardInfo{}, false
	}
	raw, ok := s.Get("Card_infos", int64(cardID))
	if !ok {
		return DirectDamageCardInfo{}, false
	}
	var row struct {
		ID         int32 `json:"id"`
		EffectType struct {
			Value int32 `json:"value"`
		} `json:"effectType"`
		CardType struct {
			Value int32 `json:"value"`
		} `json:"cardType"`
		TargetType struct {
			Value int32 `json:"value"`
		} `json:"cardTargetType"`
		Playable       bool    `json:"isPlayable"`
		ContainPlayer  bool    `json:"isContainPlayer"`
		ContainMonster bool    `json:"isContainMonster"`
		Hostile        bool    `json:"isHostile"`
		Friendly       bool    `json:"isFriendly"`
		Params         []int32 `json:"params"`
	}
	if json.Unmarshal(raw, &row) != nil || row.ID != cardID || row.EffectType.Value != 3 || row.CardType.Value != 3 || row.TargetType.Value != 2 || !row.Playable || !row.ContainPlayer || row.ContainMonster || !row.Hostile || row.Friendly || len(row.Params) != 3 || row.Params[0] <= 0 || row.Params[1] != 1 || row.Params[2] >= 0 {
		return DirectDamageCardInfo{}, false
	}
	return DirectDamageCardInfo{Range: row.Params[0], Damage: -row.Params[2]}, true
}

func (s *Store) IsDefaultHero(heroID int32) bool {
	for _, id := range s.DefaultHeroIDs() {
		if id == heroID {
			return true
		}
	}
	return false
}

func (s *Store) IsHeroSkin(heroID, skinID int32) bool {
	if heroID <= 0 || skinID <= 0 {
		return skinID == 0
	}
	raw, ok := s.Get("Skin_standingPaintings", int64(heroID))
	if !ok {
		return false
	}
	var row struct {
		Items []struct {
			ItemID int32 `json:"itemID"`
		} `json:"skinStandingPaintingConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return false
	}
	for _, item := range row.Items {
		if item.ItemID == skinID {
			return true
		}
	}
	return false
}

func (s *Store) IsDefaultHeroSkin(heroID, skinID int32) bool {
	if heroID <= 0 || skinID <= 0 {
		return false
	}
	raw, ok := s.Get("Skin_standingPaintings", int64(heroID))
	if !ok {
		return false
	}
	var row struct {
		Items []struct {
			ItemID    int32 `json:"itemID"`
			IsDefault bool  `json:"isDefault"`
		} `json:"skinStandingPaintingConfigureItems"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return false
	}
	for _, item := range row.Items {
		if item.ItemID == skinID {
			return item.IsDefault
		}
	}
	return false
}

func (s *Store) Rows(table string) []json.RawMessage {
	s.mu.RLock()
	defer s.mu.RUnlock()
	idx, ok := s.rows[table]
	if !ok {
		idx, ok = s.rows[strings.ToLower(table)]
	}
	if !ok {
		return nil
	}
	out := make([]json.RawMessage, 0, len(idx))
	for _, raw := range idx {
		out = append(out, raw)
	}
	return out
}
