package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"sort"
	"time"
)

var (
	ErrInventoryInsufficient = errors.New("inventory item count is insufficient")
	ErrPveProgressInvalid    = errors.New("invalid PVE hero progression")
	ErrPveTalentLocked       = errors.New("PVE hero talent is locked")
)

func (s *Store) PveHeroProfiles(ctx context.Context, playerID int64) (map[int32]PveHeroProfile, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT hero_id,level,exp,talents_json FROM player_pve_heroes WHERE player_id=? ORDER BY hero_id`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	profiles := make(map[int32]PveHeroProfile)
	for rows.Next() {
		var profile PveHeroProfile
		var talentsJSON string
		if err = rows.Scan(&profile.HeroID, &profile.Level, &profile.Exp, &talentsJSON); err != nil {
			return nil, err
		}
		if err = json.Unmarshal([]byte(talentsJSON), &profile.Talents); err != nil {
			return nil, fmt.Errorf("decode PVE hero talents for player %d hero %d: %w", playerID, profile.HeroID, err)
		}
		if profile.Level < 1 {
			profile.Level = 1
		}
		if profile.Talents == nil {
			profile.Talents = []int32{}
		}
		profiles[profile.HeroID] = profile
	}
	return profiles, rows.Err()
}

func (s *Store) PveHeroProfile(ctx context.Context, playerID int64, heroID int32) (PveHeroProfile, error) {
	var profile PveHeroProfile
	var talentsJSON string
	err := s.db.QueryRowContext(ctx, `SELECT hero_id,level,exp,talents_json FROM player_pve_heroes WHERE player_id=? AND hero_id=?`, playerID, heroID).
		Scan(&profile.HeroID, &profile.Level, &profile.Exp, &talentsJSON)
	if errors.Is(err, sql.ErrNoRows) {
		return PveHeroProfile{HeroID: heroID, Level: 1, Talents: []int32{}}, nil
	}
	if err != nil {
		return PveHeroProfile{}, err
	}
	if err = json.Unmarshal([]byte(talentsJSON), &profile.Talents); err != nil {
		return PveHeroProfile{}, fmt.Errorf("decode PVE hero talents for player %d hero %d: %w", playerID, heroID, err)
	}
	if profile.Level < 1 {
		profile.Level = 1
	}
	if profile.Talents == nil {
		profile.Talents = []int32{}
	}
	return profile, nil
}

func (s *Store) PlayerInventory(ctx context.Context, playerID int64) ([]InventoryItem, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT item_id,count FROM inventory WHERE player_id=? AND count>0 ORDER BY item_id`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	items := make([]InventoryItem, 0)
	for rows.Next() {
		var item InventoryItem
		if err = rows.Scan(&item.ItemID, &item.Count); err != nil {
			return nil, err
		}
		items = append(items, item)
	}
	return items, rows.Err()
}

func (s *Store) LoadPlayerGameplayData(ctx context.Context, playerID int64) (map[int32]PveHeroProfile, []InventoryItem, error) {
	profiles, err := s.PveHeroProfiles(ctx, playerID)
	if err != nil {
		return nil, nil, err
	}
	items, err := s.PlayerInventory(ctx, playerID)
	if err != nil {
		return nil, nil, err
	}
	return profiles, items, nil
}

func (s *Store) HydratePlayerGameplayData(ctx context.Context, player *Player) error {
	if player == nil || player.ID <= 0 {
		return ErrPlayerNotFound
	}
	profiles, items, err := s.LoadPlayerGameplayData(ctx, player.ID)
	if err != nil {
		return err
	}
	gachaProgress, err := s.GachaProgressForPlayer(ctx, player.ID)
	if err != nil {
		return err
	}
	fashionPlans, usePlan, err := s.LoadFashionState(ctx, player.ID)
	if err != nil {
		return err
	}
	player.PveHeroes = profiles
	player.Inventory = items
	player.GachaProgress = gachaProgress
	player.FashionPlans = fashionPlans
	player.UsePlan = usePlan
	return nil
}

func (s *Store) UpgradePveHero(ctx context.Context, playerID int64, heroID int32,
	useItems, itemExps, levelExps map[int32]int32) (PveHeroProfile, map[int32]int32, error) {
	if playerID <= 0 || heroID <= 0 || len(useItems) == 0 || len(itemExps) == 0 || len(levelExps) == 0 {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	defer tx.Rollback()
	profile, err := pveHeroProfileTx(ctx, tx, playerID, heroID)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	maxLevel := int32(0)
	for level := range levelExps {
		if level > maxLevel {
			maxLevel = level
		}
	}
	if profile.Level < 1 || profile.Level > maxLevel || profile.Exp < 0 {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	totalExp := int64(profile.Exp)
	for itemID, count := range useItems {
		itemExp, ok := itemExps[itemID]
		if !ok || itemExp <= 0 || count <= 0 {
			return PveHeroProfile{}, nil, ErrPveProgressInvalid
		}
		totalExp += int64(itemExp) * int64(count)
		if totalExp > int64(1<<62) {
			return PveHeroProfile{}, nil, ErrPveProgressInvalid
		}
	}
	remaining := -int64(profile.Exp)
	for level := profile.Level; level < maxLevel; level++ {
		required, ok := levelExps[level]
		if !ok || required <= 0 {
			return PveHeroProfile{}, nil, ErrPveProgressInvalid
		}
		remaining += int64(required)
	}
	if remaining < 0 || totalExp-int64(profile.Exp) > remaining {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	level := profile.Level
	exp := totalExp
	for level < maxLevel {
		required := int64(levelExps[level])
		if exp < required {
			break
		}
		exp -= required
		level++
	}
	if exp > int64(^uint32(0)>>1) {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	if err = consumeInventoryItems(ctx, tx, playerID, useItems); err != nil {
		return PveHeroProfile{}, nil, err
	}
	profile.Level = level
	profile.Exp = int32(exp)
	if err = savePveHeroProfile(ctx, tx, playerID, profile); err != nil {
		return PveHeroProfile{}, nil, err
	}
	counts, err := inventoryCountsTx(ctx, tx, playerID, useItems)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	if err = tx.Commit(); err != nil {
		return PveHeroProfile{}, nil, err
	}
	return profile, counts, nil
}

func (s *Store) UnlockPveHeroTalent(ctx context.Context, playerID int64, heroID, talentID,
	requiredLevel int32, orderedTalents []int32, materials map[int32]int32) (PveHeroProfile, map[int32]int32, error) {
	if playerID <= 0 || heroID <= 0 || talentID <= 0 || requiredLevel <= 0 || len(orderedTalents) == 0 || len(materials) == 0 {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	defer tx.Rollback()
	profile, err := pveHeroProfileTx(ctx, tx, playerID, heroID)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	if profile.Level < requiredLevel {
		return PveHeroProfile{}, nil, ErrPveTalentLocked
	}
	owned := make(map[int32]bool, len(profile.Talents))
	for _, id := range profile.Talents {
		owned[id] = true
	}
	nextTalent := int32(0)
	for _, id := range orderedTalents {
		if !owned[id] {
			nextTalent = id
			break
		}
	}
	if nextTalent == 0 || nextTalent != talentID {
		return PveHeroProfile{}, nil, ErrPveProgressInvalid
	}
	if err = consumeInventoryItems(ctx, tx, playerID, materials); err != nil {
		return PveHeroProfile{}, nil, err
	}
	profile.Talents = append(profile.Talents, talentID)
	if err = savePveHeroProfile(ctx, tx, playerID, profile); err != nil {
		return PveHeroProfile{}, nil, err
	}
	counts, err := inventoryCountsTx(ctx, tx, playerID, materials)
	if err != nil {
		return PveHeroProfile{}, nil, err
	}
	if err = tx.Commit(); err != nil {
		return PveHeroProfile{}, nil, err
	}
	return profile, counts, nil
}

func pveHeroProfileTx(ctx context.Context, tx *sql.Tx, playerID int64, heroID int32) (PveHeroProfile, error) {
	profile := PveHeroProfile{HeroID: heroID, Level: 1, Talents: []int32{}}
	var talentsJSON string
	err := tx.QueryRowContext(ctx, `SELECT level,exp,talents_json FROM player_pve_heroes WHERE player_id=? AND hero_id=?`, playerID, heroID).
		Scan(&profile.Level, &profile.Exp, &talentsJSON)
	if errors.Is(err, sql.ErrNoRows) {
		return profile, nil
	}
	if err != nil {
		return PveHeroProfile{}, err
	}
	if err = json.Unmarshal([]byte(talentsJSON), &profile.Talents); err != nil {
		return PveHeroProfile{}, fmt.Errorf("decode PVE hero talents for player %d hero %d: %w", playerID, heroID, err)
	}
	if profile.Level < 1 || profile.Exp < 0 {
		return PveHeroProfile{}, ErrPveProgressInvalid
	}
	if profile.Talents == nil {
		profile.Talents = []int32{}
	}
	return profile, nil
}

func savePveHeroProfile(ctx context.Context, tx *sql.Tx, playerID int64, profile PveHeroProfile) error {
	talentsJSON, err := json.Marshal(profile.Talents)
	if err != nil {
		return err
	}
	_, err = tx.ExecContext(ctx, `INSERT INTO player_pve_heroes(player_id,hero_id,level,exp,talents_json,updated_at) VALUES(?,?,?,?,?,?) ON CONFLICT(player_id,hero_id) DO UPDATE SET level=excluded.level,exp=excluded.exp,talents_json=excluded.talents_json,updated_at=excluded.updated_at`, playerID, profile.HeroID, profile.Level, profile.Exp, string(talentsJSON), time.Now().Unix())
	return err
}

func consumeInventoryItems(ctx context.Context, tx *sql.Tx, playerID int64, items map[int32]int32) error {
	ids := make([]int, 0, len(items))
	for itemID := range items {
		ids = append(ids, int(itemID))
	}
	sort.Ints(ids)
	for _, rawID := range ids {
		itemID := int32(rawID)
		count := items[itemID]
		if itemID <= 0 || count <= 0 {
			return ErrPveProgressInvalid
		}
		result, err := tx.ExecContext(ctx, `UPDATE inventory SET count=count-?,updated_at=? WHERE player_id=? AND item_id=? AND count>=?`, count, time.Now().Unix(), playerID, itemID, count)
		if err != nil {
			return err
		}
		changed, err := result.RowsAffected()
		if err != nil {
			return err
		}
		if changed == 0 {
			return ErrInventoryInsufficient
		}
	}
	return nil
}

func inventoryCountsTx(ctx context.Context, tx *sql.Tx, playerID int64, items map[int32]int32) (map[int32]int32, error) {
	counts := make(map[int32]int32, len(items))
	for itemID := range items {
		var count int32
		err := tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&count)
		if errors.Is(err, sql.ErrNoRows) {
			counts[itemID] = 0
			continue
		}
		if err != nil {
			return nil, err
		}
		counts[itemID] = count
	}
	return counts, nil
}
