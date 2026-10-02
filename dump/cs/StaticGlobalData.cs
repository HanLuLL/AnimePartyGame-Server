using System.Collections.Generic;

public class StaticGlobalData
{
	private static readonly Dictionary<string, string> _cachedStringsDict = new Dictionary<string, string>();

	private static readonly Dictionary<string, int> _cachedIntDict = new Dictionary<string, int>();

	private static readonly Dictionary<string, bool> _cachedBoolDict = new Dictionary<string, bool>();

	public static string INITIAL_ITEM => FindValue_String("INITIAL_ITEM");

	public static string INITIAL_FASHION => FindValue_String("INITIAL_FASHION");

	public static int HEART_BEAT_INTERVAL => FindValue_Int("HEART_BEAT_INTERVAL");

	public static int CLIENT_REQUEST_SERVERSTATE_CD => FindValue_Int("CLIENT_REQUEST_SERVERSTATE_CD");

	public static int GAME_INITIAL_GOLD => FindValue_Int("GAME_INITIAL_GOLD");

	public static int GAME_INITIAL_EFFECTCARD_COUNT => FindValue_Int("GAME_INITIAL_EFFECTCARD_COUNT");

	public static int GAME_INITIAL_COMBATCARD_COUNT => FindValue_Int("GAME_INITIAL_COMBATCARD_COUNT");

	public static int GAME_CARDINHAND_LIMIT => FindValue_Int("GAME_CARDINHAND_LIMIT");

	public static int GAME_BATTLE_COST_LIMIT => FindValue_Int("GAME_BATTLE_COST_LIMIT");

	public static int GAME_LAND_BLOOD_FIXEDBLOOD => FindValue_Int("GAME_LAND_BLOOD_FIXEDBLOOD");

	public static int GAME_LAND_SHOP_FREECARDNUMB => FindValue_Int("GAME_LAND_SHOP_FREECARDNUMB");

	public static int GAME_LAND_LOTTERY_GOLD_BASE => FindValue_Int("GAME_LAND_LOTTERY_GOLD_BASE");

	public static int GAME_LAND_LOTTERY_GOLD_ADD => FindValue_Int("GAME_LAND_LOTTERY_GOLD_ADD");

	public static int GAME_LAND_LOTTERY_NUMB_LIMIT => FindValue_Int("GAME_LAND_LOTTERY_NUMB_LIMIT");

	public static int GAME_RECONNECT_TIME_LIMIT => FindValue_Int("GAME_RECONNECT_TIME_LIMIT");

	public static int GAME_ALL_RECONNECT_TIME_LIMIT => FindValue_Int("GAME_ALL_RECONNECT_TIME_LIMIT");

	public static int GAME_HOST_TIME_OFFLINE => FindValue_Int("GAME_HOST_TIME_OFFLINE");

	public static int GAME_FIGHT_DAMAGE_THRESHOLD => FindValue_Int("GAME_FIGHT_DAMAGE_THRESHOLD");

	public static int GAME_FIGHT_BUST_Gold_THRESHOLD => FindValue_Int("GAME_FIGHT_BUST_Gold_THRESHOLD");

	public static int GAME_JUDGE_DICE_LIMIT => FindValue_Int("GAME_JUDGE_DICE_LIMIT");

	public static int GAME_CAMERA_SWITCH_TIME => FindValue_Int("GAME_CAMERA_SWITCH_TIME");

	public static int GAME_MUTATOR_SHOW_TIME => FindValue_Int("GAME_MUTATOR_SHOW_TIME");

	public static int GAME_HERO_SCALE_LIMIT_MAX => FindValue_Int("GAME_HERO_SCALE_LIMIT_MAX");

	public static int GAME_CHARACTER_EXPRESSION_CD => FindValue_Int("GAME_CHARACTER_EXPRESSION_CD");

	public static int GAME_CHARACTER_EXPRESSION_DURATION => FindValue_Int("GAME_CHARACTER_EXPRESSION_DURATION");

	public static int GAME_OPERATION_TIME_LIMIT_MAX => FindValue_Int("GAME_OPERATION_TIME_LIMIT_MAX");

	public static int GAME_CHANGE_PLAYORDER_ROUND => FindValue_Int("GAME_CHANGE_PLAYORDER_ROUND");

	public static int GAME_RESULT_TIME_LIMIT_MAX => FindValue_Int("GAME_RESULT_TIME_LIMIT_MAX");

	public static int GAME_NEED_PLAYER_COUNT => FindValue_Int("GAME_NEED_PLAYER_COUNT");

	public static int SELECT_ROLE_TIMELIMIT => FindValue_Int("SELECT_ROLE_TIMELIMIT");

	public static int INGAME_RESOURCE_LOADING_TIMELIMIT => FindValue_Int("INGAME_RESOURCE_LOADING_TIMELIMIT");

	public static int ROOM_WAIT_TIMELIMIT => FindValue_Int("ROOM_WAIT_TIMELIMIT");

	public static int ROOM_ACTIVE_TIME => FindValue_Int("ROOM_ACTIVE_TIME");

	public static int ROOM_CONNECT_TIMELIMIT => FindValue_Int("ROOM_CONNECT_TIMELIMIT");

	public static int ROOM_QUIT_TIMELIMIT => FindValue_Int("ROOM_QUIT_TIMELIMIT");

	public static int ROOM_KICK_TIMELIMIT => FindValue_Int("ROOM_KICK_TIMELIMIT");

	public static int ROOM_AUDIENCE_NUMBLIMIT => FindValue_Int("ROOM_AUDIENCE_NUMBLIMIT");

	public static int ROOM_SURRENDER_PUNISHTIME => FindValue_Int("ROOM_SURRENDER_PUNISHTIME");

	public static int SELECT_ROLESKIN_TIMELIMIT => FindValue_Int("SELECT_ROLESKIN_TIMELIMIT");

	public static int ROOM_PVELOCK_DIFFICULTY => FindValue_Int("ROOM_PVELOCK_DIFFICULTY");

	public static int ROOM_PVELOCK_LEVEL => FindValue_Int("ROOM_PVELOCK_LEVEL");

	public static int ROOM_PLAY_TIMELIMIT => FindValue_Int("ROOM_PLAY_TIMELIMIT");

	public static int FRIEND_NUMBLIMIT => FindValue_Int("FRIEND_NUMBLIMIT");

	public static int FRIEND_APPLICATION_DAILYLIMIT => FindValue_Int("FRIEND_APPLICATION_DAILYLIMIT");

	public static int FRIEND_APPLICATION_AVAILABLE_DAYS => FindValue_Int("FRIEND_APPLICATION_AVAILABLE_DAYS");

	public static int FRIEND_INVITEPARTY_AVAILABLE_SECONDS => FindValue_Int("FRIEND_INVITEPARTY_AVAILABLE_SECONDS");

	public static int FRIEND_BLACKLIST_NUMBLIMIT => FindValue_Int("FRIEND_BLACKLIST_NUMBLIMIT");

	public static int FRIEND_INVITEPARTY_BUTTON_CD => FindValue_Int("FRIEND_INVITEPARTY_BUTTON_CD");

	public static int FRIEND_RECENTPARTY_NUMBLIMIT => FindValue_Int("FRIEND_RECENTPARTY_NUMBLIMIT");

	public static int FRIEND_CHAT_RECORD_NUMBLIMIT => FindValue_Int("FRIEND_CHAT_RECORD_NUMBLIMIT");

	public static int FRIEND_CHAT_BYTES_LIMIT => FindValue_Int("FRIEND_CHAT_BYTES_LIMIT");

	public static int FRIEND_CHAT_SAVED_DAYS => FindValue_Int("FRIEND_CHAT_SAVED_DAYS");

	public static int FRIEND_SHOW_MINI_LIMIT => FindValue_Int("FRIEND_SHOW_MINI_LIMIT ");

	public static int AUDIO_BGM_FIGHT => FindValue_Int("AUDIO_BGM_FIGHT");

	public static int AUDIO_BANK_UI => FindValue_Int("AUDIO_BANK_UI");

	public static int AUDIO_BANK_BGM_LOGIN => FindValue_Int("AUDIO_BANK_BGM_LOGIN");

	public static int AUDIO_BANK_BGM_HOME => FindValue_Int("AUDIO_BANK_BGM_HOME");

	public static int AUDIO_BANK_BGM_BATTLE => FindValue_Int("AUDIO_BANK_BGM_BATTLE");

	public static int AUDIO_BANK_BGM_ROLE_COMMON => FindValue_Int("AUDIO_BANK_BGM_ROLE_COMMON");

	public static int AUDIO_BANK_Npc_01 => FindValue_Int("AUDIO_BANK_Npc_01");

	public static int AUDIO_BANK_HERO_HOME => FindValue_Int("AUDIO_BANK_HERO_HOME");

	public static int AUDIO_BANK_HERO_GUIDE => FindValue_Int("AUDIO_BANK_HERO_GUIDE");

	public static int MAIL_NUM_LIMIT => FindValue_Int("MAIL_NUM_LIMIT");

	public static int MAIL_NUM_WARNING => FindValue_Int("MAIL_NUM_WARNING");

	public static int MAIL_EXPIRATION => FindValue_Int("MAIL_EXPIRATION");

	public static int SPECIAL_ITEM_MONTHLY_CARD => FindValue_Int("SPECIAL_ITEM_MONTHLY_CARD");

	public static int SPECIAL_ITEM_SUPER_GIFT => FindValue_Int("SPECIAL_ITEM_SUPER_GIFT");

	public static int SPECIAL_ITEM_SUPER_BADGE => FindValue_Int("SPECIAL_ITEM_SUPER_BADGE");

	public static int SPECIAL_ITEM_BATTLEPASS_NORMAL => FindValue_Int("SPECIAL_ITEM_BATTLEPASS_NORMAL");

	public static int SPECIAL_ITEM_BATTLEPASS_PREMIUM => FindValue_Int("SPECIAL_ITEM_BATTLEPASS_PREMIUM");

	public static int SPECIAL_ITEM_STARDISC_FREE => FindValue_Int("SPECIAL_ITEM_STARDISC_FREE");

	public static int SPECIAL_ITEM_STARDISC_PAY => FindValue_Int("SPECIAL_ITEM_STARDISC_PAY");

	public static int SPECIAL_ITEM_PLAYER_EXP => FindValue_Int("SPECIAL_ITEM_PLAYER_EXP");

	public static int SPECIAL_ITEM_BATTLEPASS_MAP => FindValue_Int("SPECIAL_ITEM_BATTLEPASS_MAP");

	public static int SPECIAL_ITEM_BATTLEPASS_LEVEL => FindValue_Int("SPECIAL_ITEM_BATTLEPASS_LEVEL");

	public static int PVETOKEN_WEEK_LIMIT => FindValue_Int("PVETOKEN_WEEK_LIMIT");

	public static int HOME_PANEL_VIDEO => FindValue_Int("HOME_PANEL_VIDEO");

	public static string CN_SERVER_OPENING_TIME => FindValue_String("CN_SERVER_OPENING_TIME");

	public static string RECHARGE_REBATE_RECIVE_TIME => FindValue_String("RECHARGE_REBATE_RECIVE_TIME");

	public static int REBATE_EXP_DATE => FindValue_Int("REBATE_EXP_DATE");

	public static string GLOBAL_CELEBRATE_END_TIME => FindValue_String("GLOBAL_CELEBRATE_END_TIME");

	public static bool IS_CN_SEVER => FindValue_Bool("IS_CN_SEVER");

	public static int COMEBACK_TRIGGER_TIME => FindValue_Int("COMEBACK_TRIGGER_TIME");

	public static int COMEBACK_DURING_TIME => FindValue_Int("COMEBACK_DURING_TIME");

	public static int MAX_CREDIT_SCORE => FindValue_Int("MAX_CREDIT_SCORE");

	public static int WEEKLY_RESET_THRESHOLD_SCORE => FindValue_Int("WEEKLY_RESET_THRESHOLD_SCORE");

	public static int MAX_STAR_EXPRESSION_COUNT => FindValue_Int("MAX_STAR_EXPRESSION_COUNT");

	public static int MAX_BATTLE_RECORDS => FindValue_Int("MAX_BATTLE_RECORDS");

	public static int GUILD_MEMBER_LIMIT => FindValue_Int("GUILD_MEMBER_LIMIT");

	public static int GUILD_VICEGUILDMASTER_LIMIT => FindValue_Int("GUILD_VICEGUILDMASTER_LIMIT");

	public static int GUILD_NAME_LIMIT => FindValue_Int("GUILD_NAME_LIMIT");

	public static int GUILD_TAG_LIMIT => FindValue_Int("GUILD_TAG_LIMIT");

	public static int GUILD_APPLICATIONS_TERM => FindValue_Int("GUILD_APPLICATIONS_TERM");

	public static int GUILD_JOIN_CD => FindValue_Int("GUILD_JOIN_CD");

	public static int GUILD_APPLICATIONS_DAILYLIMIT => FindValue_Int("GUILD_APPLICATIONS_DAILYLIMIT");

	public static int GUILD_SETTINGS_DAILYLIMIT => FindValue_Int("GUILD_SETTINGS_DAILYLIMIT");

	public static int GUILD_EXTERNALANNOUNCEMENT_LIMIT => FindValue_Int("GUILD_EXTERNALANNOUNCEMENT_LIMIT");

	public static int GUILD_INTERNALANNOUNCEMENT_DAILYLIMIT => FindValue_Int("GUILD_INTERNALANNOUNCEMENT_DAILYLIMIT");

	public static int GUILD_INTERNALANNOUNCEMENT_LIMIT => FindValue_Int("GUILD_INTERNALANNOUNCEMENT_LIMIT");

	public static int GUILD_INVITETOGUILD_TERM => FindValue_Int("GUILD_INVITETOGUILD_TERM");

	public static int GUILD_IMPEACH_MASTEROFFLINEDAYS => FindValue_Int("GUILD_IMPEACH_MASTEROFFLINEDAYS");

	public static int GUILD_ATUODISBAND_OFFLINE_DAYS => FindValue_Int("GUILD_ATUODISBAND_OFFLINE_DAYS");

	public static int GUILD_MEMBERMESSAGE_RETENTIONDAYS => FindValue_Int("GUILD_MEMBERMESSAGE_RETENTIONDAYS");

	public static int GUILD_MEMBERMESSAGE_RETENTIONCOUNT => FindValue_Int("GUILD_MEMBERMESSAGE_RETENTIONCOUNT");

	public static int GUILD_CHAT_RETENTIONDAYS => FindValue_Int("GUILD_CHAT_RETENTIONDAYS");

	public static int GUILD_CHAT_RETENTIONCOUNT => FindValue_Int("GUILD_CHAT_RETENTIONCOUNT");

	public static int GUILD_SEARCH_CD => FindValue_Int("GUILD_SEARCH_CD");

	public static int GUILD_SEARCH_RESULT_ITEMSPERPAGE => FindValue_Int("GUILD_SEARCH_RESULT_ITEMSPERPAGE");

	public static int GUILD_CHATMESSAGE_CD => FindValue_Int("GUILD_CHATMESSAGE_CD");

	public static void InitData()
	{
		_cachedStringsDict.Clear();
		_cachedIntDict.Clear();
		_cachedBoolDict.Clear();
		foreach (GlobalDataConfigure data in StaticConfigure.Global.Datas)
		{
			switch (data.Type)
			{
			case "String":
				_cachedStringsDict.Add(data.Key, data.Value);
				break;
			case "Int":
				_cachedIntDict.Add(data.Key, int.Parse(data.Value));
				break;
			case "Bool":
				_cachedBoolDict.Add(data.Key, bool.Parse(data.Value));
				break;
			}
		}
	}

	private static int FindValue_Int(string key)
	{
		if (_cachedIntDict.TryGetValue(key, out var value))
		{
			return value;
		}
		return 0;
	}

	private static string FindValue_String(string key)
	{
		if (_cachedStringsDict.TryGetValue(key, out var value))
		{
			return value;
		}
		return "";
	}

	private static bool FindValue_Bool(string key)
	{
		if (_cachedBoolDict.TryGetValue(key, out var value))
		{
			return value;
		}
		return false;
	}
}
