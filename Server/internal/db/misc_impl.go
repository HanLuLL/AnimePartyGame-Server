package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

// This file backs internal/gateway/misc_impl.go: small single-purpose
// persistence helpers for the remaining client commands (role cards, charge
// orders, activity claims, spectator mode bookkeeping, single-player data).

// ErrChargeOrderUnknown / ErrChargeOrderAlreadyDone are charge-flow errors.
var (
	ErrChargeOrderUnknown     = errors.New("charge order not found")
	ErrChargeOrderAlreadyDone = errors.New("charge order already completed")
)

// boolInt maps a bool onto the 0/1 column convention.
func boolInt(v bool) int {
	if v {
		return 1
	}
	return 0
}

// Role cards ---------------------------------------------------------------

type roleCardRow struct {
	DefID     int64
	Lv        int32
	Exp       int32
	IsSuper   bool
	Collected bool
}

// SaveRoleCard upserts the level/exp record for one role card.
func (s *Store) SaveRoleCard(ctx context.Context, playerID, defID int64, lv, exp int32) error {
	if playerID <= 0 || defID <= 0 || lv <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO role_cards(player_id,def_id,lv,exp,updated_at)
		VALUES(?,?,?,?,?) ON CONFLICT(player_id,def_id) DO UPDATE SET lv=excluded.lv, exp=excluded.exp, updated_at=excluded.updated_at`,
		playerID, defID, lv, exp, time.Now().Unix())
	return err
}

// SetRoleCardSuper persists the breakthrough flag.
func (s *Store) SetRoleCardSuper(ctx context.Context, playerID, defID int64, isSuper bool) error {
	if playerID <= 0 || defID <= 0 {
		return ErrPlayerNotFound
	}
	v := 0
	if isSuper {
		v = 1
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO role_cards(player_id,def_id,lv,is_super,updated_at)
		VALUES(?,?,1,?,?) ON CONFLICT(player_id,def_id) DO UPDATE SET is_super=excluded.is_super, updated_at=excluded.updated_at`,
		playerID, defID, v, time.Now().Unix())
	return err
}

// SetRoleCardCollected persists the collection flag.
func (s *Store) SetRoleCardCollected(ctx context.Context, playerID, defID int64, collected bool) error {
	if playerID <= 0 || defID <= 0 {
		return ErrPlayerNotFound
	}
	v := 0
	if collected {
		v = 1
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO role_cards(player_id,def_id,lv,collected,updated_at)
		VALUES(?,?,1,?,?) ON CONFLICT(player_id,def_id) DO UPDATE SET collected=excluded.collected, updated_at=excluded.updated_at`,
		playerID, defID, v, time.Now().Unix())
	return err
}

// LoadRoleCards returns every stored role card row for a player.
func (s *Store) LoadRoleCards(ctx context.Context, playerID int64) ([]roleCardRow, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT def_id,lv,exp,is_super,collected FROM role_cards WHERE player_id=?`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []roleCardRow
	for rows.Next() {
		var r roleCardRow
		var isSuper, collected int
		if err := rows.Scan(&r.DefID, &r.Lv, &r.Exp, &isSuper, &collected); err != nil {
			return out, err
		}
		r.IsSuper = isSuper != 0
		r.Collected = collected != 0
		out = append(out, r)
	}
	return out, rows.Err()
}

// Charge orders ------------------------------------------------------------

type ChargeOrder struct {
	ID             int64
	PlayerID       int64
	GoodsID        int32
	Count          int32
	GoodsItemID    int32
	GoodsItemCount int32
	Type           int32
	Completed      bool
}

// CreateChargeOrder records a pending order and returns its id. goodsItem /
// goodsItemCount come from the Recharge_infos table when the caller resolved
// the goods row; zero values keep the order valid but non-granting.
func (s *Store) CreateChargeOrder(ctx context.Context, playerID int64, goodsID, count int32, now int64) (int64, error) {
	if playerID <= 0 || goodsID <= 0 {
		return 0, ErrChargeOrderUnknown
	}
	res, err := s.db.ExecContext(ctx, `INSERT INTO charge_orders(player_id,goods_id,count,created_at) VALUES(?,?,?,?)`,
		playerID, goodsID, count, now)
	if err != nil {
		return 0, err
	}
	return res.LastInsertId()
}

// CompleteChargeOrder marks an order paid exactly once and returns it.
func (s *Store) CompleteChargeOrder(ctx context.Context, playerID, orderID int64) (ChargeOrder, error) {
	var order ChargeOrder
	var completed int
	err := s.db.QueryRowContext(ctx, `SELECT id,player_id,goods_id,count,completed FROM charge_orders WHERE id=?`, orderID).
		Scan(&order.ID, &order.PlayerID, &order.GoodsID, &order.Count, &completed)
	if errors.Is(err, sql.ErrNoRows) {
		return order, ErrChargeOrderUnknown
	}
	if err != nil {
		return order, err
	}
	if order.PlayerID != playerID {
		return order, ErrChargeOrderUnknown
	}
	if completed != 0 || order.Completed {
		return order, ErrChargeOrderAlreadyDone
	}
	if _, err := s.db.ExecContext(ctx, `UPDATE charge_orders SET completed=1,completed_at=? WHERE id=?`, time.Now().Unix(), orderID); err != nil {
		return order, err
	}
	order.Completed = true
	return order, nil
}

// Settings / CDK ------------------------------------------------------------

// ClaimCdkCode records one redemption per player per code; returns false when
// the player already redeemed the code.
func (s *Store) ClaimCdkCode(ctx context.Context, playerID int64, code string) (bool, error) {
	res, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO cdk_claims(player_id,code,claimed_at) VALUES(?,?,?)`,
		playerID, code, time.Now().Unix())
	if err != nil {
		return false, err
	}
	n, _ := res.RowsAffected()
	return n > 0, nil
}

// Activity claims -----------------------------------------------------------

// ClaimActivityTask records a claimed activity/mission/acquisition reward;
// returns false when already claimed.
func (s *Store) ClaimActivityTask(ctx context.Context, playerID int64, infoID, taskID int32) (bool, error) {
	if playerID <= 0 || infoID <= 0 {
		return false, ErrPlayerNotFound
	}
	res, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO activity_claims(player_id,info_id,task_id,claimed_at) VALUES(?,?,?,?)`,
		playerID, infoID, taskID, time.Now().Unix())
	if err != nil {
		return false, err
	}
	n, _ := res.RowsAffected()
	return n > 0, nil
}

// Scratch cards ---------------------------------------------------------------

// RecordScratchCard persists one scratched cell and its server-rolled result.
func (s *Store) RecordScratchCard(ctx context.Context, playerID int64, activityID, index int32, result int64) error {
	if playerID <= 0 || activityID <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT OR REPLACE INTO scratch_cards(player_id,activity_id,index_value,result,created_at) VALUES(?,?,?,?,?)`,
		playerID, activityID, index, result, time.Now().Unix())
	return err
}

// AdvanceScratchCardPool bumps the pool cursor for the activity and returns
// the new pool number.
func (s *Store) AdvanceScratchCardPool(ctx context.Context, playerID int64, activityID int32) (int32, error) {
	if playerID <= 0 || activityID <= 0 {
		return 0, ErrPlayerNotFound
	}
	if _, err := s.db.ExecContext(ctx, `INSERT INTO scratch_card_pools(player_id,activity_id,pool,updated_at) VALUES(?,?,1,?)
		ON CONFLICT(player_id,activity_id) DO UPDATE SET pool=pool+1, updated_at=excluded.updated_at`,
		playerID, activityID, time.Now().Unix()); err != nil {
		return 0, err
	}
	var pool int32
	if err := s.db.QueryRowContext(ctx, `SELECT pool FROM scratch_card_pools WHERE player_id=? AND activity_id=?`,
		playerID, activityID).Scan(&pool); err != nil {
		return 0, err
	}
	return pool, nil
}

// Flip cards ------------------------------------------------------------------

type FlipCardResult struct {
	Index       int32
	RewardIndex int32
}

// FlipCardCell flips one cell exactly once, storing a stable reward index.
func (s *Store) FlipCardCell(ctx context.Context, playerID int64, activityID, index int32) (FlipCardResult, error) {
	if playerID <= 0 || activityID <= 0 || index <= 0 {
		return FlipCardResult{}, ErrPlayerNotFound
	}
	var reward FlipCardResult
	var existing int64
	err := s.db.QueryRowContext(ctx,
		`SELECT reward_index FROM flip_cards WHERE player_id=? AND activity_id=? AND index_value=?`,
		playerID, activityID, index).Scan(&existing)
	switch {
	case err == nil:
		reward.Index, reward.RewardIndex = index, int32(existing)
		return reward, nil
	case errors.Is(err, sql.ErrNoRows):
		reward.RewardIndex = int32(time.Now().UnixNano()%6) + 1
		if _, err = s.db.ExecContext(ctx,
			`INSERT OR IGNORE INTO flip_cards(player_id,activity_id,index_value,reward_index,created_at) VALUES(?,?,?,?,?)`,
			playerID, activityID, index, reward.RewardIndex, time.Now().Unix()); err != nil {
			return reward, err
		}
		reward.Index = index
		return reward, nil
	default:
		return reward, err
	}
}

// ClaimFlipCardProgress records a claimed flip-card milestone; false when
// already claimed.
func (s *Store) ClaimFlipCardProgress(ctx context.Context, playerID int64, activityID int32) (bool, error) {
	if playerID <= 0 || activityID <= 0 {
		return false, ErrPlayerNotFound
	}
	res, err := s.db.ExecContext(ctx,
		`INSERT OR IGNORE INTO flip_card_progress(player_id,activity_id,claimed_at) VALUES(?,?,?)`,
		playerID, activityID, time.Now().Unix())
	if err != nil {
		return false, err
	}
	n, _ := res.RowsAffected()
	return n > 0, nil
}

// Light gift ------------------------------------------------------------------

type LightGiftState struct {
	Index     int32
	ConfIndex int32
}

// LightGiftState loads (or initializes) the gift state.
func (s *Store) LightGiftState(ctx context.Context, playerID int64, activityID int32) (LightGiftState, error) {
	state := LightGiftState{}
	err := s.db.QueryRowContext(ctx,
		`SELECT index_value, conf_index, bought FROM light_gifts WHERE player_id=? AND activity_id=?`,
		playerID, activityID).Scan(&state.Index, &state.ConfIndex, new(int))
	if errors.Is(err, sql.ErrNoRows) {
		return state, nil
	}
	return state, err
}

// BuyLightGift marks the gift bought at the requested tier.
func (s *Store) BuyLightGift(ctx context.Context, playerID int64, activityID, index int32) error {
	if playerID <= 0 || activityID <= 0 || index <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO light_gifts(player_id,activity_id,index_value,conf_index,bought,updated_at)
		VALUES(?,?,?,0,1,?) ON CONFLICT(player_id,activity_id) DO UPDATE SET index_value=excluded.index_value, bought=1, updated_at=excluded.updated_at`,
		playerID, activityID, index, time.Now().Unix())
	return err
}

// Praise / accuse / misc counters --------------------------------------------

// PraisePlayer adds one like from source to target, one per day per pair.
func (s *Store) PraisePlayer(ctx context.Context, sourceID, targetID int64) error {
	if sourceID <= 0 || targetID <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO praises(source_player_id,target_player_id,day,created_at) VALUES(?,?,?,?)`,
		sourceID, targetID, time.Now().Format("2006-01-02"), time.Now().Unix())
	return err
}

// RecordAccuse stores a player report in the audit trail.
func (s *Store) RecordAccuse(ctx context.Context, reporterID, targetID int64, now int64) error {
	if reporterID <= 0 || targetID <= 0 {
		return ErrPlayerNotFound
	}
	detail, _ := json.Marshal(map[string]int64{"reporter": reporterID, "target": targetID})
	_, err := s.db.ExecContext(ctx, `INSERT INTO admin_audit(action,detail,created_at) VALUES('accuse',?,?)`,
		string(detail), now)
	return err
}

// RecordClientCheckTask records a client-reported task/mission touch.
func (s *Store) RecordClientCheckTask(ctx context.Context, playerID int64, taskID int32) error {
	if playerID <= 0 || taskID <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT OR IGNORE INTO client_check_tasks(player_id,task_id,created_at) VALUES(?,?,?)`,
		playerID, taskID, time.Now().Unix())
	return err
}

// SetAgeVerify persists the age-verification answer.
func (s *Store) SetAgeVerify(ctx context.Context, playerID int64, agePosition int32, now int64) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "age_position", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "age_verified_at", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	_, err := s.db.ExecContext(ctx, `UPDATE players SET age_position=?, age_verified_at=? WHERE id=?`, agePosition, now, playerID)
	return err
}

// AddIdleTime accumulates the reported idle seconds.
func (s *Store) AddIdleTime(ctx context.Context, playerID int64, seconds int32) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "idle_time", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	_, err := s.db.ExecContext(ctx, `UPDATE players SET idle_time=idle_time+? WHERE id=?`, seconds, playerID)
	return err
}

// Card alt art ----------------------------------------------------------------

// SetCardAltArt persists the chosen face for a card the player owns.
func (s *Store) SetCardAltArt(ctx context.Context, playerID int64, cardID, faceID int32) error {
	if playerID <= 0 || cardID <= 0 {
		return ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "alt_art_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	var blob string
	err := s.db.QueryRowContext(ctx, `SELECT alt_art_json FROM players WHERE id=?`, playerID).Scan(&blob)
	if errors.Is(err, sql.ErrNoRows) {
		return ErrPlayerNotFound
	}
	if err != nil {
		return err
	}
	mapping := map[int32]int32{}
	if blob != "" && blob != "{}" {
		_ = json.Unmarshal([]byte(blob), &mapping)
	}
	mapping[cardID] = faceID
	encoded, err := json.Marshal(mapping)
	if err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, `UPDATE players SET alt_art_json=? WHERE id=?`, string(encoded), playerID)
	return err
}

// Single-player data ----------------------------------------------------------

// SaveSingleGameData persists the client's single-player progress blob.
func (s *Store) SaveSingleGameData(ctx context.Context, playerID int64, blob any) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	encoded, err := json.Marshal(blob)
	if err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "single_game_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, `UPDATE players SET single_game_json=? WHERE id=?`, string(encoded), playerID)
	return err
}

// LoadSingleGameData returns the stored single-player blob for a level; the
// level filter is applied client-side because the blob is one document.
func (s *Store) LoadSingleGameData(ctx context.Context, playerID int64) (map[string]json.RawMessage, error) {
	out := map[string]json.RawMessage{}
	var blob string
	err := s.db.QueryRowContext(ctx, `SELECT single_game_json FROM players WHERE id=?`, playerID).Scan(&blob)
	if errors.Is(err, sql.ErrNoRows) {
		return out, nil
	}
	if err != nil {
		return out, err
	}
	if blob != "" && blob != "{}" {
		_ = json.Unmarshal([]byte(blob), &out)
	}
	return out, nil
}

// Acquisition -----------------------------------------------------------------

// AcquisitionCode returns (creating on first use) the player's invite code
// for an acquisition activity: a deterministic base36 token from the player
// and activity IDs.
func (s *Store) AcquisitionCode(ctx context.Context, playerID int64, activityID int32) (string, error) {
	if playerID <= 0 || activityID <= 0 {
		return "", ErrPlayerNotFound
	}
	code := fmt.Sprintf("%X%d", playerID, activityID)
	if err := s.ensureColumn(ctx, "players", "acquisition_code", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return "", err
	}
	var existing string
	err := s.db.QueryRowContext(ctx, `SELECT acquisition_code FROM players WHERE id=?`, playerID).Scan(&existing)
	if err != nil {
		return "", err
	}
	if existing == "" {
		if _, err := s.db.ExecContext(ctx, `UPDATE players SET acquisition_code=? WHERE id=?`, code, playerID); err != nil {
			return "", err
		}
		return code, nil
	}
	return existing, nil
}

// Return-player flow ------------------------------------------------------------

// ClaimReturnGift marks the return gift claimed; false when already taken.
func (s *Store) ClaimReturnGift(ctx context.Context, playerID int64) (bool, error) {
	if playerID <= 0 {
		return false, ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "return_gift_claimed", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return false, err
	}
	res, err := s.db.ExecContext(ctx, `UPDATE players SET return_gift_claimed=1 WHERE id=? AND return_gift_claimed=0`, playerID)
	if err != nil {
		return false, err
	}
	n, _ := res.RowsAffected()
	return n > 0, nil
}

// ClaimReturnSignIn advances the return sign-in counter and returns the
// protobuf-ready state.
func (s *Store) ClaimReturnSignIn(ctx context.Context, playerID int64, actID int64, advanced bool) (int32, error) {
	if playerID <= 0 || actID <= 0 {
		return 0, ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "return_sign_in_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return 0, err
	}
	var blob string
	err := s.db.QueryRowContext(ctx, `SELECT return_sign_in_json FROM players WHERE id=?`, playerID).Scan(&blob)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, ErrPlayerNotFound
	}
	if err != nil {
		return 0, err
	}
	state := map[string]int64{}
	if blob != "" && blob != "{}" {
		_ = json.Unmarshal([]byte(blob), &state)
	}
	key := fmt.Sprintf("%d", actID)
	state[key]++
	encoded, _ := json.Marshal(state)
	if _, err := s.db.ExecContext(ctx, `UPDATE players SET return_sign_in_json=? WHERE id=?`, string(encoded), playerID); err != nil {
		return 0, err
	}
	return int32(state[key]), nil
}

// SetReturnSurvey records the survey completion state.
func (s *Store) SetReturnSurvey(ctx context.Context, playerID int64, state int64) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "return_survey_state", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	_, err := s.db.ExecContext(ctx, `UPDATE players SET return_survey_state=? WHERE id=?`, state, playerID)
	return err
}

// Labor dice --------------------------------------------------------------------

// RecordLaborDice stores one activity dice roll.
func (s *Store) RecordLaborDice(ctx context.Context, playerID int64, diceType int32, point int64) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO labor_dice(player_id,dice_type,point,created_at) VALUES(?,?,?,?)`,
		playerID, diceType, point, time.Now().Unix())
	return err
}

// SaveClientData stores the typed client data payload (the gateway passes
// the decoded model) after a JSON round-trip. The blob is kept verbatim so
// the client's next Connect snapshot can echo it back without re-mapping.
func (s *Store) SaveClientData(ctx context.Context, playerID int64, payload any) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	if err := s.ensureColumn(ctx, "players", "client_data_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	encoded, err := json.Marshal(payload)
	if err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, `UPDATE players SET client_data_json=? WHERE id=?`, string(encoded), playerID)
	return err
}

// LoadRoleCard returns one stored role-card row (zero values when absent).
func (s *Store) LoadRoleCard(ctx context.Context, playerID, defID int64) (roleCardRow, error) {
	var r roleCardRow
	var isSuper, collected int
	err := s.db.QueryRowContext(ctx, `SELECT COALESCE(lv,1),COALESCE(exp,0),is_super,collected
		FROM role_cards WHERE player_id=? AND def_id=?`, playerID, defID).
		Scan(&r.Lv, &r.Exp, &isSuper, &collected)
	if errors.Is(err, sql.ErrNoRows) {
		return roleCardRow{DefID: defID, Lv: 1}, nil
	}
	if err != nil {
		return r, err
	}
	r.DefID = defID
	r.IsSuper = isSuper != 0
	r.Collected = collected != 0
	return r, nil
}
