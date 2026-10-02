package db

import (
	"context"
	"fmt"
	"time"
)

const latestSchemaVersion = 12

func (s *Store) Migrate(ctx context.Context) error {
	if _, err := s.db.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS schema_migrations (
		version INTEGER PRIMARY KEY, name TEXT NOT NULL, applied_at INTEGER NOT NULL
	)`); err != nil {
		return err
	}
	var version int
	if err := s.db.QueryRowContext(ctx, `SELECT COALESCE(MAX(version),0) FROM schema_migrations`).Scan(&version); err != nil {
		return err
	}
	if version > latestSchemaVersion {
		return fmt.Errorf("database schema version %d is newer than supported version %d", version, latestSchemaVersion)
	}
	if version == latestSchemaVersion {
		return nil
	}
	if version == 1 {
		if err := s.migrateRoomInvitesAndRecentPlayers(ctx); err != nil {
			return err
		}
		version = 2
	}
	if version == 2 {
		if err := s.migratePrivateChatHistory(ctx); err != nil {
			return err
		}
		version = 3
	}
	if version == 3 {
		if err := s.migrateLandSummons(ctx); err != nil {
			return err
		}
		version = 4
	}
	if version == 4 {
		if err := s.migratePlayerHarmony(ctx); err != nil {
			return err
		}
		version = 5
	}
	if version == 5 {
		if err := s.migrateAdminAndAssetAudit(ctx); err != nil {
			return err
		}
		version = 6
	}
	if version == 6 {
		if err := s.migratePlayerWebPasswordGate(ctx); err != nil {
			return err
		}
		version = 7
	}
	if version == 7 {
		if err := s.migrateNotices(ctx); err != nil {
			return err
		}
		version = 8
	}
	if version == 8 {
		if err := s.migrateBattlePass(ctx); err != nil {
			return err
		}
		version = 9
	}
	if version == 9 {
		if err := s.migrateMatchAndReplay(ctx); err != nil {
			return err
		}
		version = 10
	}
	if version == 10 {
		if err := s.migrateGuild(ctx); err != nil {
			return err
		}
		if err := s.migrateSinglePlayerProgress(ctx); err != nil {
			return err
		}
		version = 11
	}
	if version == 11 {
		if err := s.migrateMailExpiry(ctx); err != nil {
			return err
		}
		version = 12
	}
	if version == 12 {
		return nil
	}
	if version != 0 {
		return fmt.Errorf("no migration path from database schema version %d", version)
	}
	stmts := []string{
		`CREATE TABLE IF NOT EXISTS accounts (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, login_key TEXT NOT NULL UNIQUE,
	 platform TEXT NOT NULL DEFAULT 'dev', nick TEXT NOT NULL, device_id TEXT NOT NULL DEFAULT '',
	 token TEXT NOT NULL DEFAULT '', email TEXT NOT NULL DEFAULT '', phone TEXT NOT NULL DEFAULT '', account_no TEXT NOT NULL DEFAULT '',
	 disabled INTEGER NOT NULL DEFAULT 0, created_at INTEGER NOT NULL, last_login_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS players (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, account_id INTEGER NOT NULL UNIQUE REFERENCES accounts(id),
	 nick TEXT NOT NULL, level INTEGER NOT NULL DEFAULT 1, exp INTEGER NOT NULL DEFAULT 0,
	 room_id INTEGER, slot INTEGER NOT NULL DEFAULT 0, node_id INTEGER NOT NULL DEFAULT 0,
	 back_node_id INTEGER NOT NULL DEFAULT 0,
	 front_node_ids_json TEXT NOT NULL DEFAULT '[]',
	 gold INTEGER NOT NULL DEFAULT 0, hp INTEGER NOT NULL DEFAULT 10, special_score INTEGER NOT NULL DEFAULT 0, hospital_rounds INTEGER NOT NULL DEFAULT 0, hero_level INTEGER NOT NULL DEFAULT 0,
	 battle_cards_json TEXT NOT NULL DEFAULT '[]', battle_buffs_json TEXT NOT NULL DEFAULT '[]', lotterys_json TEXT NOT NULL DEFAULT '{}',
	 fashion_plans_json TEXT NOT NULL DEFAULT '{}', use_plan INTEGER NOT NULL DEFAULT 1,
	 online_status INTEGER NOT NULL DEFAULT 0, created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS rooms (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL DEFAULT '', pwd TEXT NOT NULL DEFAULT '',
	 map_id INTEGER NOT NULL DEFAULT 0, max_time INTEGER NOT NULL DEFAULT 0,
	 upgrade_plan INTEGER NOT NULL DEFAULT 0, time_plan INTEGER NOT NULL DEFAULT 0,
	 mode INTEGER NOT NULL DEFAULT 0, lobby_id INTEGER NOT NULL DEFAULT 0,
	 speed_type INTEGER NOT NULL DEFAULT 0, difficulty INTEGER NOT NULL DEFAULT 0,
	 skip_story INTEGER NOT NULL DEFAULT 0, room_label INTEGER NOT NULL DEFAULT 0,
	 master_id INTEGER NOT NULL, state INTEGER NOT NULL DEFAULT 1,
	 created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS room_members (
	 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id), slot INTEGER NOT NULL,
	 ready INTEGER NOT NULL DEFAULT 0, online INTEGER NOT NULL DEFAULT 1,
	 hero_id INTEGER NOT NULL DEFAULT 0, hero_confirmed INTEGER NOT NULL DEFAULT 0,
	 pve_level INTEGER NOT NULL DEFAULT 1, pve_talent_id INTEGER NOT NULL DEFAULT 0, game_data_json TEXT NOT NULL DEFAULT '{}',
	 use_adorn INTEGER NOT NULL DEFAULT 0, skin_pendant INTEGER NOT NULL DEFAULT 0,
	 skin_confirmed INTEGER NOT NULL DEFAULT 0, progress INTEGER NOT NULL DEFAULT 0,
	 PRIMARY KEY(room_id,player_id), UNIQUE(room_id,slot))`,
		`CREATE TABLE IF NOT EXISTS game_sessions (
	 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
	 round INTEGER NOT NULL DEFAULT 1, current_player_id INTEGER NOT NULL DEFAULT 0,
	 pending_move INTEGER NOT NULL DEFAULT 0, turn_phase TEXT NOT NULL DEFAULT 'throw_dice', status TEXT NOT NULL DEFAULT 'running',
	 game_progress INTEGER NOT NULL DEFAULT 0, game_max_progress INTEGER NOT NULL DEFAULT 0, special_score INTEGER NOT NULL DEFAULT 0,
	 started_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_actions (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, room_id INTEGER NOT NULL, player_id INTEGER NOT NULL,
	 cmd_id INTEGER NOT NULL, upsn INTEGER NOT NULL, payload BLOB, created_at INTEGER NOT NULL,
	 UNIQUE(room_id,player_id,upsn))`,
		`CREATE TABLE IF NOT EXISTS game_land_buffs (
	 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
	 unique_id INTEGER NOT NULL, node_id INTEGER NOT NULL, buff_id INTEGER NOT NULL,
	 value INTEGER NOT NULL, buff_json TEXT NOT NULL, created_at INTEGER NOT NULL,
	 PRIMARY KEY(room_id,unique_id))`,
		`CREATE INDEX IF NOT EXISTS idx_game_land_buffs_node ON game_land_buffs(room_id,node_id,created_at,unique_id)`,
		`CREATE TABLE IF NOT EXISTS game_shops (
	 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id), state_json TEXT NOT NULL,
	 created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_divinations (
		 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
		 player_id INTEGER NOT NULL REFERENCES players(id), choices_json TEXT NOT NULL,
		 created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_skill_event_offers (
		 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
		 player_id INTEGER NOT NULL REFERENCES players(id), round INTEGER NOT NULL,
		 action_sn INTEGER NOT NULL, choices_json TEXT NOT NULL, created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_skill_cooldowns (
		 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
		 player_id INTEGER NOT NULL REFERENCES players(id), skill_id INTEGER NOT NULL,
		 ready_round INTEGER NOT NULL, PRIMARY KEY(room_id,player_id,skill_id))`,
		`CREATE TABLE IF NOT EXISTS game_gambles (
	 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
	 state_json TEXT NOT NULL, created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_battles (
	 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
	 state_json TEXT NOT NULL, created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_player_stats (
	 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL, kill_count INTEGER NOT NULL DEFAULT 0,
	 total_die INTEGER NOT NULL DEFAULT 0, total_damage INTEGER NOT NULL DEFAULT 0,
	 total_injured INTEGER NOT NULL DEFAULT 0, treatment_score INTEGER NOT NULL DEFAULT 0,
	 PRIMARY KEY(room_id,player_id))`,
		`CREATE TABLE IF NOT EXISTS game_card_turns (
		 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
		 player_id INTEGER NOT NULL REFERENCES players(id), round INTEGER NOT NULL,
		 done INTEGER NOT NULL DEFAULT 0, use_card_num INTEGER NOT NULL DEFAULT 0,
		 use_card_max_num INTEGER NOT NULL DEFAULT 1, created_at INTEGER NOT NULL,
		 PRIMARY KEY(room_id,player_id,round))`,
		`CREATE TABLE IF NOT EXISTS game_control_moves (
		 room_id INTEGER PRIMARY KEY REFERENCES rooms(id) ON DELETE CASCADE,
		 player_id INTEGER NOT NULL REFERENCES players(id), round INTEGER NOT NULL,
		 card_id INTEGER NOT NULL, max_point INTEGER NOT NULL,
		 action_sn INTEGER NOT NULL, selected_point INTEGER NOT NULL DEFAULT 0,
		 created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS game_bombs (
	 id INTEGER PRIMARY KEY AUTOINCREMENT,
	 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
	 owner_player_id INTEGER NOT NULL REFERENCES players(id),
	 holder_player_id INTEGER NOT NULL REFERENCES players(id),
	 card_id INTEGER NOT NULL, is_open INTEGER NOT NULL DEFAULT 0, created_at INTEGER NOT NULL)`,
		`CREATE INDEX IF NOT EXISTS idx_game_bombs_holder ON game_bombs(room_id,holder_player_id,id)`,
		`CREATE TABLE IF NOT EXISTS turn_start_effects (
	 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
	 round INTEGER NOT NULL, player_id INTEGER NOT NULL, created_at INTEGER NOT NULL,
	 PRIMARY KEY(room_id,round,player_id))`,
		`CREATE TABLE IF NOT EXISTS chat_messages (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, room_id INTEGER NOT NULL DEFAULT 0,
	 from_player INTEGER NOT NULL, to_player INTEGER NOT NULL DEFAULT 0,
	 expression_id INTEGER NOT NULL DEFAULT 0, short_index INTEGER NOT NULL DEFAULT 0,
	 message TEXT NOT NULL DEFAULT '', created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS friendships (
	 player_id INTEGER NOT NULL, friend_id INTEGER NOT NULL, state INTEGER NOT NULL DEFAULT 1,
	 note TEXT NOT NULL DEFAULT '', created_at INTEGER NOT NULL, PRIMARY KEY(player_id,friend_id))`,
		`CREATE TABLE IF NOT EXISTS friend_blocks (
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 blocked_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 created_at INTEGER NOT NULL, PRIMARY KEY(player_id,blocked_id), CHECK(player_id<>blocked_id))`,
		`CREATE TABLE IF NOT EXISTS friend_requests (
	 requester_id INTEGER NOT NULL, target_id INTEGER NOT NULL, state INTEGER NOT NULL DEFAULT 0,
	 created_at INTEGER NOT NULL, PRIMARY KEY(requester_id,target_id))`,
		`CREATE TABLE IF NOT EXISTS match_teams (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, mode INTEGER NOT NULL, map_id INTEGER NOT NULL DEFAULT 0,
	 difficulty INTEGER NOT NULL DEFAULT 0, leader_id INTEGER NOT NULL, state INTEGER NOT NULL DEFAULT 1,
	 created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS match_team_members (
	 team_id INTEGER NOT NULL REFERENCES match_teams(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id), ready INTEGER NOT NULL DEFAULT 0,
	 PRIMARY KEY(team_id,player_id), UNIQUE(player_id))`,
		`CREATE TABLE IF NOT EXISTS inventory (
		 player_id INTEGER NOT NULL, item_id INTEGER NOT NULL, count INTEGER NOT NULL DEFAULT 0,
		 updated_at INTEGER NOT NULL, PRIMARY KEY(player_id,item_id))`,
		`CREATE TABLE IF NOT EXISTS gacha_progress (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 gacha_id INTEGER NOT NULL, pool_id INTEGER NOT NULL, count INTEGER NOT NULL DEFAULT 0,
		 reward_count INTEGER NOT NULL DEFAULT -1, updated_at INTEGER NOT NULL,
		 PRIMARY KEY(player_id,pool_id))`,
		`CREATE TABLE IF NOT EXISTS gacha_records (
		 id INTEGER PRIMARY KEY AUTOINCREMENT,
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 gacha_id INTEGER NOT NULL, pool_id INTEGER NOT NULL, item_id INTEGER NOT NULL,
		 item_count INTEGER NOT NULL, is_convert INTEGER NOT NULL DEFAULT 0, created_at INTEGER NOT NULL)`,
		`CREATE INDEX IF NOT EXISTS idx_gacha_records_player_gacha_id ON gacha_records(player_id,gacha_id,id)`,
		`CREATE TABLE IF NOT EXISTS gacha_draws (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 upsn INTEGER NOT NULL, cmd_id INTEGER NOT NULL, gacha_id INTEGER NOT NULL, pool_id INTEGER NOT NULL,
		 count INTEGER NOT NULL, progress_count INTEGER NOT NULL DEFAULT 0,
		 costs_json TEXT NOT NULL, rewards_json TEXT NOT NULL,
		 bonus_json TEXT NOT NULL DEFAULT '[]', created_at INTEGER NOT NULL,
		 PRIMARY KEY(player_id,upsn))`,
		`CREATE TABLE IF NOT EXISTS player_pve_heroes (
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 hero_id INTEGER NOT NULL, level INTEGER NOT NULL DEFAULT 1, exp INTEGER NOT NULL DEFAULT 0,
	 talents_json TEXT NOT NULL DEFAULT '[]', updated_at INTEGER NOT NULL,
	 PRIMARY KEY(player_id,hero_id))`,
		`CREATE TABLE IF NOT EXISTS mail (
	 player_id INTEGER NOT NULL, mail_id INTEGER NOT NULL, title TEXT NOT NULL DEFAULT '',
	 context TEXT NOT NULL DEFAULT '', is_read INTEGER NOT NULL DEFAULT 0,
	 is_star INTEGER NOT NULL DEFAULT 0, is_rewarded INTEGER NOT NULL DEFAULT 0,
	 rewards_json TEXT NOT NULL DEFAULT '{}', created_at INTEGER NOT NULL,
	 expire_at INTEGER NOT NULL DEFAULT 0, start_time INTEGER NOT NULL DEFAULT 0,
	 PRIMARY KEY(player_id,mail_id))`,
		`CREATE TABLE IF NOT EXISTS task_progress (
		 player_id INTEGER NOT NULL, def_id INTEGER NOT NULL, type INTEGER NOT NULL DEFAULT 0,
		 progress INTEGER NOT NULL DEFAULT 0, rewarded INTEGER NOT NULL DEFAULT 0,
		 updated_at INTEGER NOT NULL, PRIMARY KEY(player_id,def_id,type))`,
		`CREATE TABLE IF NOT EXISTS player_sign_in (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 activity_id INTEGER NOT NULL, sign_in_count INTEGER NOT NULL DEFAULT 0,
		 update_time INTEGER NOT NULL DEFAULT 0, PRIMARY KEY(player_id,activity_id))`,
		`CREATE TABLE IF NOT EXISTS task_counters (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 condition_type INTEGER NOT NULL, period_key TEXT NOT NULL, progress INTEGER NOT NULL DEFAULT 0,
		 updated_at INTEGER NOT NULL, PRIMARY KEY(player_id,condition_type,period_key))`,
		`CREATE TABLE IF NOT EXISTS task_rewards (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 reward_type INTEGER NOT NULL, def_id INTEGER NOT NULL, period_key TEXT NOT NULL, claimed_at INTEGER NOT NULL,
		 PRIMARY KEY(player_id,reward_type,def_id,period_key))`,
		`CREATE TABLE IF NOT EXISTS server_settings (
	 key TEXT PRIMARY KEY, value TEXT NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS admin_audit (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, action TEXT NOT NULL, detail TEXT NOT NULL DEFAULT '{}', created_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS auth_email_codes (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT NOT NULL, code_hash TEXT NOT NULL,
	 sdk_token_hash TEXT NOT NULL DEFAULT '', ip_hash TEXT NOT NULL DEFAULT '', attempts INTEGER NOT NULL DEFAULT 0,
	 expires_at INTEGER NOT NULL, created_at INTEGER NOT NULL, consumed_at INTEGER NOT NULL DEFAULT 0)`,
		`CREATE TABLE IF NOT EXISTS auth_sessions (
	 token_hash TEXT PRIMARY KEY, account_id INTEGER NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
	 expires_at INTEGER NOT NULL, created_at INTEGER NOT NULL, revoked_at INTEGER NOT NULL DEFAULT 0)`,
		`CREATE TABLE IF NOT EXISTS auth_handoffs (
	 code_hash TEXT PRIMARY KEY, account_id INTEGER NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
	 device_id TEXT NOT NULL DEFAULT '', expires_at INTEGER NOT NULL,
	 created_at INTEGER NOT NULL, consumed_at INTEGER NOT NULL DEFAULT 0)`,
		`CREATE TABLE IF NOT EXISTS auth_handoff_limits (
	 ip_hash TEXT PRIMARY KEY, window_started_at INTEGER NOT NULL, attempts INTEGER NOT NULL DEFAULT 0)`,
		`CREATE INDEX IF NOT EXISTS idx_rooms_state ON rooms(state,updated_at)`,
		`CREATE INDEX IF NOT EXISTS idx_players_room ON players(room_id)`,
		`CREATE INDEX IF NOT EXISTS idx_auth_email_codes_email ON auth_email_codes(email,created_at DESC)`,
		`CREATE INDEX IF NOT EXISTS idx_auth_email_codes_ip ON auth_email_codes(ip_hash,created_at DESC)`,
		`CREATE INDEX IF NOT EXISTS idx_auth_sessions_account ON auth_sessions(account_id,expires_at)`,
		`CREATE INDEX IF NOT EXISTS idx_auth_handoffs_expiry ON auth_handoffs(expires_at,consumed_at)`,
	}
	for _, q := range stmts {
		if _, err := s.db.ExecContext(ctx, q); err != nil {
			return err
		}
	}
	if err := s.ensureGachaProgressPoolKey(ctx); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "accounts", "disabled", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "gacha_draws", "bonus_json", "TEXT NOT NULL DEFAULT '[]'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "gacha_draws", "progress_count", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "online_status", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "hero_level", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "special_score", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "hospital_rounds", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "battle_cards_json", "TEXT NOT NULL DEFAULT '[]'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "battle_buffs_json", "TEXT NOT NULL DEFAULT '[]'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "lotterys_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "show_player_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "fashion_plans_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "use_plan", "INTEGER NOT NULL DEFAULT 1"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_sessions", "turn_phase", "TEXT NOT NULL DEFAULT 'throw_dice'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_sessions", "game_progress", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_sessions", "game_max_progress", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_sessions", "special_score", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_card_turns", "use_card_num", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "game_card_turns", "use_card_max_num", "INTEGER NOT NULL DEFAULT 1"); err != nil {
		return err
	}
	if _, err := s.db.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE pending_move>0 AND turn_phase=?`, TurnPhaseMoving, TurnPhaseThrowDice); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "back_node_id", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "front_node_ids_json", "TEXT NOT NULL DEFAULT '[]'"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "friendships", "note", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "chat_messages", "message", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "next_change_name_at", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "accounts", "email", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "accounts", "phone", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "auth_email_codes", "sdk_token_hash", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "accounts", "account_no", "TEXT NOT NULL DEFAULT ''"); err != nil {
		return err
	}
	for _, col := range []struct{ name, definition string }{
		{"hero_id", "INTEGER NOT NULL DEFAULT 0"},
		{"hero_confirmed", "INTEGER NOT NULL DEFAULT 0"},
		{"pve_level", "INTEGER NOT NULL DEFAULT 1"},
		{"pve_talent_id", "INTEGER NOT NULL DEFAULT 0"},
		{"game_data_json", "TEXT NOT NULL DEFAULT '{}'"},
		{"use_adorn", "INTEGER NOT NULL DEFAULT 0"},
		{"skin_pendant", "INTEGER NOT NULL DEFAULT 0"},
		{"skin_confirmed", "INTEGER NOT NULL DEFAULT 0"},
		{"progress", "INTEGER NOT NULL DEFAULT 0"},
	} {
		if err := s.ensureColumn(ctx, "room_members", col.name, col.definition); err != nil {
			return err
		}
	}
	if _, err := s.db.ExecContext(ctx, `CREATE UNIQUE INDEX IF NOT EXISTS idx_accounts_email ON accounts(email) WHERE email <> ''`); err != nil {
		return err
	}
	if _, err := s.db.ExecContext(ctx, `CREATE UNIQUE INDEX IF NOT EXISTS idx_accounts_phone ON accounts(phone) WHERE phone <> ''`); err != nil {
		return err
	}
	if _, err := s.db.ExecContext(ctx, `CREATE UNIQUE INDEX IF NOT EXISTS idx_accounts_account_no ON accounts(account_no) WHERE account_no <> ''`); err != nil {
		return err
	}
	if _, err := s.db.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 1, "baseline", time.Now().Unix()); err != nil {
		return err
	}
	if err := s.migrateRoomInvitesAndRecentPlayers(ctx); err != nil {
		return err
	}
	if err := s.migratePrivateChatHistory(ctx); err != nil {
		return err
	}
	if err := s.migrateLandSummons(ctx); err != nil {
		return err
	}
	if err := s.migratePlayerHarmony(ctx); err != nil {
		return err
	}
	if err := s.migrateAdminAndAssetAudit(ctx); err != nil {
		return err
	}
	if err := s.migratePlayerWebPasswordGate(ctx); err != nil {
		return err
	}
	if err := s.migrateBattlePass(ctx); err != nil {
		return err
	}
	if err := s.migrateMatchAndReplay(ctx); err != nil {
		return err
	}
	if err := s.migrateGuild(ctx); err != nil {
		return err
	}
	if err := s.migrateSinglePlayerProgress(ctx); err != nil {
		return err
	}
	if err := s.migrateMailExpiry(ctx); err != nil {
		return err
	}
	return s.migrateNotices(ctx)
}

// migrateBattlePass creates schema version 9: the player_battle_pass season
// snapshot plus the players.client_data_json column used by
// ClientDataUploadC2S.
func (s *Store) migrateBattlePass(ctx context.Context) error {
	if err := s.ensureColumn(ctx, "players", "client_data_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS player_battle_pass (
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 def_id INTEGER NOT NULL, lv INTEGER NOT NULL DEFAULT 1, exp INTEGER NOT NULL DEFAULT 0,
	 gear INTEGER NOT NULL DEFAULT 0, reward_ids_json TEXT NOT NULL DEFAULT '{}',
	 task_json TEXT NOT NULL DEFAULT '{}', task_reward_json TEXT NOT NULL DEFAULT '{}',
	 updated_at INTEGER NOT NULL, PRIMARY KEY(player_id,def_id))`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`,
		9, "battle_pass", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migrateMatchAndReplay creates schema version 10: the fight_records and
// replay_snapshots tables backing the matchmaking loop and the replay system.
func (s *Store) migrateMatchAndReplay(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS fight_records (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, replay_id TEXT NOT NULL DEFAULT '',
	 room_id INTEGER NOT NULL DEFAULT 0, player_id INTEGER NOT NULL,
	 rank INTEGER NOT NULL DEFAULT 0, hero_id INTEGER NOT NULL DEFAULT 0,
	 map_type INTEGER NOT NULL DEFAULT 0, is_give_up INTEGER NOT NULL DEFAULT 0,
	 is_replay INTEGER NOT NULL DEFAULT 0, time INTEGER NOT NULL, created_at INTEGER NOT NULL)`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `CREATE INDEX IF NOT EXISTS idx_fight_records_player ON fight_records(player_id,is_replay,time)`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS replay_snapshots (
	 replay_id TEXT PRIMARY KEY, room_id INTEGER NOT NULL DEFAULT 0,
	 data BLOB NOT NULL, created_at INTEGER NOT NULL)`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`,
		10, "match_and_replay", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) migratePlayerWebPasswordGate(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS self_web_first_change (account_id INTEGER PRIMARY KEY REFERENCES accounts(id) ON DELETE CASCADE)`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 7, "player_web_first_password_change", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migrateNotices adds the operator-managed announcement table served to the
// game client at /Temp/Notice, and seeds one welcome announcement when the
// table is empty.
func (s *Store) migrateNotices(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS notices (
	 id INTEGER PRIMARY KEY AUTOINCREMENT,
	 enabled INTEGER NOT NULL DEFAULT 1,
	 sort_order INTEGER NOT NULL DEFAULT 0,
	 title_json TEXT NOT NULL DEFAULT '{}',
	 content_json TEXT NOT NULL DEFAULT '{}',
	 created_at INTEGER NOT NULL,
	 updated_at INTEGER NOT NULL)`); err != nil {
		return err
	}
	var count int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM notices`).Scan(&count); err != nil {
		return err
	}
	if count == 0 {
		now := time.Now().Unix()
		title, err := encodeLocalizedText(LocalizedText{
			Cn: "Welcome to the AstralParty Private Server",
			En: "Welcome to AstralParty Private Server",
		})
		if err != nil {
			return err
		}
		content, err := encodeLocalizedText(LocalizedText{
			Cn:  "Welcome to the AstralParty Private Server, independently operated for casual entertainment. Sign in with your phone number — no verification code is needed, and signing in registers your account. Contact the administrator if you need help. Enjoy the game!",
			En:  "Welcome to the AstralParty Private Server, independently operated by the Linxi Island Operations Team for casual entertainment. Sign in with your phone number — no verification code is needed, and signing in registers your account. Contact the administrator if you need help. Enjoy the game!",
			Jp:  "Welcome to the AstralParty Private Server, independently operated for casual entertainment. Sign in with your phone number (no verification code is needed; signing in registers your account). Contact the administrator if you need help. Have fun!",
			Cht: "Welcome to the AstralParty Private Server, independently operated for casual entertainment. Sign in with your phone number (no verification code is needed; signing in registers your account). Contact the administrator if you need help. Have fun!",
			Ko:  "AstralParty 프라이빗 서버에 오신 것을 환영합니다! 본 서버는 임희서 운영팀이 독립적으로 운영하는 여가용 서버입니다. 전화번호로 로그인하세요. 인증 코드는 필요 없으며 로그인 즉시 가입됩니다. 문제가 있으면 관리자에게 문의하세요. 즐거운 게임 되세요!",
		})
		if err != nil {
			return err
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO notices(enabled,sort_order,title_json,content_json,created_at,updated_at) VALUES(1,1,?,?,?,?)`, title, content, now, now); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 8, "operator_notices", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migrateRoomInvitesAndRecentPlayers adds durable room invitations and the
// recent co-player list used by the client's NearFightPlayer RPC.
func (s *Store) migrateRoomInvitesAndRecentPlayers(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	for _, statement := range []string{
		`CREATE TABLE IF NOT EXISTS room_invites (
		 room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
		 inviter_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 invitee_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 created_at INTEGER NOT NULL,
		 PRIMARY KEY(room_id,invitee_id), CHECK(inviter_id<>invitee_id))`,
		`CREATE INDEX IF NOT EXISTS idx_room_invites_invitee ON room_invites(invitee_id,created_at DESC)`,
		`CREATE TABLE IF NOT EXISTS recent_players (
		 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 target_player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		 last_played_at INTEGER NOT NULL,
		 PRIMARY KEY(player_id,target_player_id), CHECK(player_id<>target_player_id))`,
		`CREATE INDEX IF NOT EXISTS idx_recent_players_owner_time ON recent_players(player_id,last_played_at DESC,target_player_id)`,
	} {
		if _, err = tx.ExecContext(ctx, statement); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 2, "room_invites_and_recent_players", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migratePrivateChatHistory adds per-player deletion cursors so clearing a
// conversation on one client does not erase the other player's copy.
func (s *Store) migratePrivateChatHistory(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS chat_history_cursors (
		owner_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		target_player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		deleted_through_id INTEGER NOT NULL DEFAULT 0,
		PRIMARY KEY(owner_id,target_player_id), CHECK(owner_id<>target_player_id))`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 3, "private_chat_history_cursors", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migrateLandSummons persists map-node buffs so pickups survive reconnects and
// process restarts. It is separate from hero buffs because the client keys
// these by board node and receives them through LandBuffsS2C.
func (s *Store) migrateLandSummons(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS game_land_buffs (
		room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
		unique_id INTEGER NOT NULL, node_id INTEGER NOT NULL, buff_id INTEGER NOT NULL,
		value INTEGER NOT NULL, buff_json TEXT NOT NULL, created_at INTEGER NOT NULL,
		PRIMARY KEY(room_id,unique_id))`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `CREATE INDEX IF NOT EXISTS idx_game_land_buffs_node ON game_land_buffs(room_id,node_id,created_at,unique_id)`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 4, "persistent_land_summons", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) migrateAdminAndAssetAudit(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	for _, statement := range []string{
		`ALTER TABLE accounts ADD COLUMN web_password_hash TEXT NOT NULL DEFAULT ''`,
		`ALTER TABLE accounts ADD COLUMN must_change_password INTEGER NOT NULL DEFAULT 0`,
		`CREATE TABLE admin_accounts (
		 id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT NOT NULL UNIQUE, password_hash TEXT NOT NULL,
		 must_change_password INTEGER NOT NULL DEFAULT 0, created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE admin_sessions (
		 token_hash TEXT PRIMARY KEY, admin_id INTEGER NOT NULL REFERENCES admin_accounts(id) ON DELETE CASCADE,
		 expires_at INTEGER NOT NULL, created_at INTEGER NOT NULL, revoked_at INTEGER NOT NULL DEFAULT 0)`,
		`CREATE INDEX idx_admin_sessions_admin ON admin_sessions(admin_id,expires_at)`,
		`CREATE TABLE asset_change_audit (
		 id INTEGER PRIMARY KEY AUTOINCREMENT, player_id INTEGER NOT NULL REFERENCES players(id),
		 actor_id INTEGER NOT NULL DEFAULT 0, actor_kind TEXT NOT NULL, before_json TEXT NOT NULL,
		 after_json TEXT NOT NULL, reverts_change_id INTEGER REFERENCES asset_change_audit(id),
		 reverted_by INTEGER REFERENCES asset_change_audit(id), created_at INTEGER NOT NULL)`,
		`CREATE INDEX idx_asset_change_audit_player ON asset_change_audit(player_id,id DESC)`,
	} {
		if _, err = tx.ExecContext(ctx, statement); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 6, "admin_accounts_and_asset_audit", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migratePlayerHarmony persists the client's anti-harmony (angel mode) switch
// reported through ClientHarmonyC2S so a reconnecting client keeps its choice.
func (s *Store) migratePlayerHarmony(ctx context.Context) error {
	if err := s.ensureColumn(ctx, "players", "is_harmony", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "players", "harmony_type", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`, 5, "player_harmony", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// ensureGachaProgressPoolKey migrates early builds that keyed gacha progress
// only by gacha type. Progress and claim thresholds belong to a pool because
// a type can receive a new pool when its banner rotates.
func (s *Store) ensureGachaProgressPoolKey(ctx context.Context) error {
	rows, err := s.db.QueryContext(ctx, `PRAGMA table_info(gacha_progress)`)
	if err != nil {
		return err
	}
	primary := make(map[string]int)
	for rows.Next() {
		var cid, notNull, primaryKeyOrder int
		var name, kind string
		var defaultValue any
		if err = rows.Scan(&cid, &name, &kind, &notNull, &defaultValue, &primaryKeyOrder); err != nil {
			rows.Close()
			return err
		}
		primary[name] = primaryKeyOrder
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return err
	}
	if err = rows.Close(); err != nil {
		return err
	}
	if primary["player_id"] == 1 && primary["pool_id"] == 2 {
		return nil
	}

	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `ALTER TABLE gacha_progress RENAME TO gacha_progress_legacy`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `CREATE TABLE gacha_progress (
		player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
		gacha_id INTEGER NOT NULL, pool_id INTEGER NOT NULL, count INTEGER NOT NULL DEFAULT 0,
		reward_count INTEGER NOT NULL DEFAULT -1, updated_at INTEGER NOT NULL,
		PRIMARY KEY(player_id,pool_id))`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO gacha_progress(player_id,gacha_id,pool_id,count,reward_count,updated_at)
		SELECT player_id,MAX(gacha_id),pool_id,MAX(count),MAX(reward_count),MAX(updated_at)
		FROM gacha_progress_legacy GROUP BY player_id,pool_id`); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `DROP TABLE gacha_progress_legacy`); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) ensureColumn(ctx context.Context, table, column, definition string) error {
	rows, err := s.db.QueryContext(ctx, "PRAGMA table_info("+table+")")
	if err != nil {
		return err
	}
	defer rows.Close()
	for rows.Next() {
		var cid, notnull, pk int
		var name, typ string
		var def any
		if err = rows.Scan(&cid, &name, &typ, &notnull, &def, &pk); err != nil {
			return err
		}
		if name == column {
			return nil
		}
	}
	if err = rows.Err(); err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, "ALTER TABLE "+table+" ADD COLUMN "+column+" "+definition)
	return err
}

// migrateGuild creates schema version 11: the guild system tables — guild
// profiles, memberships, applications, invitations, guild chat, member-change
// notifications and guild mission claim state.
func (s *Store) migrateGuild(ctx context.Context) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	stmts := []string{
		`CREATE TABLE IF NOT EXISTS guilds (
	 id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL UNIQUE,
	 tag_ids_json TEXT NOT NULL DEFAULT '[]', status INTEGER NOT NULL DEFAULT 1,
	 ex_announcement TEXT NOT NULL DEFAULT '', in_announcement TEXT NOT NULL DEFAULT '',
	 last_external_edit_time INTEGER NOT NULL DEFAULT 0,
	 last_external_editor_id INTEGER NOT NULL DEFAULT 0,
	 external_edit_count INTEGER NOT NULL DEFAULT 0,
	 last_internal_edit_time INTEGER NOT NULL DEFAULT 0,
	 last_internal_editor_id INTEGER NOT NULL DEFAULT 0,
	 internal_edit_count INTEGER NOT NULL DEFAULT 0,
	 master_id INTEGER NOT NULL REFERENCES players(id),
	 created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL)`,
		`CREATE TABLE IF NOT EXISTS guild_members (
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 role INTEGER NOT NULL DEFAULT 3,
	 weekly_activity INTEGER NOT NULL DEFAULT 0,
	 last_weekly_activity INTEGER NOT NULL DEFAULT 0,
	 last_login_time INTEGER NOT NULL DEFAULT 0,
	 weekly_sign INTEGER NOT NULL DEFAULT 0,
	 join_at INTEGER NOT NULL,
	 PRIMARY KEY(guild_id,player_id))`,
		`CREATE INDEX IF NOT EXISTS idx_guild_members_player ON guild_members(player_id)`,
		`CREATE TABLE IF NOT EXISTS guild_applications (
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 apply_time INTEGER NOT NULL,
	 PRIMARY KEY(guild_id,player_id))`,
		`CREATE INDEX IF NOT EXISTS idx_guild_applications_player ON guild_applications(player_id)`,
		`CREATE TABLE IF NOT EXISTS guild_invitations (
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 inviter_id INTEGER NOT NULL,
	 invitation_time INTEGER NOT NULL,
	 PRIMARY KEY(guild_id,player_id))`,
		`CREATE INDEX IF NOT EXISTS idx_guild_invitations_player ON guild_invitations(player_id)`,
		`CREATE TABLE IF NOT EXISTS guild_chat_messages (
	 id INTEGER PRIMARY KEY AUTOINCREMENT,
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 sender_id INTEGER NOT NULL, msg TEXT NOT NULL, time INTEGER NOT NULL)`,
		`CREATE INDEX IF NOT EXISTS idx_guild_chat_guild ON guild_chat_messages(guild_id,id)`,
		`CREATE TABLE IF NOT EXISTS guild_change_messages (
	 id INTEGER PRIMARY KEY AUTOINCREMENT,
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 notification_id INTEGER NOT NULL, params_json TEXT NOT NULL DEFAULT '[]',
	 time INTEGER NOT NULL)`,
		`CREATE INDEX IF NOT EXISTS idx_guild_change_guild ON guild_change_messages(guild_id,id)`,
		`CREATE TABLE IF NOT EXISTS guild_mission_progress (
	 guild_id INTEGER NOT NULL REFERENCES guilds(id) ON DELETE CASCADE,
	 player_id INTEGER NOT NULL REFERENCES players(id) ON DELETE CASCADE,
	 task_id INTEGER NOT NULL, progress INTEGER NOT NULL DEFAULT 0,
	 claimed INTEGER NOT NULL DEFAULT 0, updated_at INTEGER NOT NULL,
	 PRIMARY KEY(guild_id,player_id,task_id))`,
	}
	for _, stmt := range stmts {
		if _, err = tx.ExecContext(ctx, stmt); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`,
		11, "guild_system", time.Now().Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// migrateSinglePlayerProgress adds the player_single_progress table backing
// SingleCampaignC2S / SyncSingleGameDataC2S persistence (schema v11). The
// version stamp is owned by the sibling v11 migration that runs first in the
// chain, so this step only ensures the schema objects exist.
func (s *Store) migrateSinglePlayerProgress(ctx context.Context) error {
	if err := s.ensureColumn(ctx, "players", "single_progress_json", "TEXT NOT NULL DEFAULT '{}'"); err != nil {
		return err
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `CREATE TABLE IF NOT EXISTS player_single_progress (
	 player_id INTEGER PRIMARY KEY REFERENCES players(id) ON DELETE CASCADE,
	 level_pass_json TEXT NOT NULL DEFAULT '{}',
	 stage_level_json TEXT NOT NULL DEFAULT '{}',
	 max_score INTEGER NOT NULL DEFAULT 0,
	 data_json TEXT NOT NULL DEFAULT '{}',
	 updated_at INTEGER NOT NULL)`); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) migrateMailExpiry(ctx context.Context) error {
	if err := s.ensureColumn(ctx, "mail", "expire_at", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	if err := s.ensureColumn(ctx, "mail", "start_time", "INTEGER NOT NULL DEFAULT 0"); err != nil {
		return err
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO schema_migrations(version,name,applied_at) VALUES(?,?,?)`,
		12, "mail_expiry_columns", time.Now().Unix())
	return err
}
