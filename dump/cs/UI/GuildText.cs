using System;
using UnityEngine;

namespace UI;

public static class GuildText
{
	public const int FALLBACK = 3999;

	public const int SETTINGS_SENSITIVE_WORD = 3100;

	public const int SETTINGS_DAILY_LIMIT = 3101;

	public const int SETTINGS_SUCCEEDED = 3102;

	public const int INTERNAL_SENSITIVE_WORD = 3103;

	public const int INTERNAL_DAILY_LIMIT = 3104;

	public const int INTERNAL_SUCCEEDED = 3105;

	public const int TAG_LIMIT = 3106;

	public const int SETTINGS_UNCHANGED = 3107;

	public const int INTERNAL_UNCHANGED = 3108;

	public const int SENSITIVE_WORDS_NOT_READY = 3109;

	public const int TRANSFER_CONFIRM = 3110;

	public const int TRANSFER_SUCCEEDED = 3111;

	public const int PROMOTE_CONFIRM = 3112;

	public const int PROMOTE_SUCCEEDED = 3113;

	public const int DEMOTE_CONFIRM = 3114;

	public const int DEMOTE_SUCCEEDED = 3115;

	public const int KICK_CONFIRM = 3116;

	public const int KICK_SUCCEEDED = 3117;

	public const int NO_TRANSFER_TARGET = 3118;

	public const int VICE_MASTER_LIMIT = 3119;

	public const int IMPEACH_CONFIRM = 3120;

	public const int IMPEACH_SUCCEEDED = 3121;

	public const int MEMBER_STATE_CHANGED = 3122;

	public const int NO_PERMISSION = 3123;

	public const int INVALID_SELF_OPERATION = 3124;

	public const int INVALID_MEMBER_RELATION = 3125;

	public const int IMPEACH_OFFLINE_DAYS_NOT_MET = 3126;

	public const int TRANSFER_OPERATION = 3127;

	public const int PROMOTE_OPERATION = 3128;

	public const int DEMOTE_OPERATION = 3129;

	public const int EXIT_CONFIRM = 3130;

	public const int EXIT_SUCCEEDED = 3131;

	public const int DISBAND_CONFIRM = 3132;

	public const int DISBAND_SUCCEEDED = 3133;

	public const int GUILD_INVALID = 3134;

	public const int GUILD_DETAIL_MISMATCH = 3137;

	public const int MEMBER_CHANGE_SCOPE = 3140;

	public const int MEMBER_CHANGE_EMPTY = 3141;

	public const int MEMBER_INFO_LOADING = 3142;

	public const int MEMBER_SELECTION_INVALID = 3143;

	public const int MEMBER_EMPTY = 3144;

	public const int INTERNAL_ANNOUNCEMENT_EMPTY = 3150;

	public const int GUILD_LOADING = 3151;

	public const int GUILD_LOAD_FAILED = 3152;

	public const int INTERNAL_CONFLICT_CONFIRM = 3153;

	public const int IMPEACH_OPERATION = 3154;

	public const int INVITE_SUCCEEDED = 3160;

	public const int INVITE_UID_INVALID = 3161;

	public const int INVITE_TARGET_INVALID = 3162;

	public const int APPROVAL_ACCEPT_SUCCEEDED = 3163;

	public const int APPROVAL_REJECT_SUCCEEDED = 3164;

	public const int APPROVAL_REJECT_ALL_CONFIRM = 3165;

	public const int APPROVAL_REJECT_ALL_SUCCEEDED = 3166;

	public static string GetTitle(GuildTitleType title)
	{
		return Get((int)title);
	}

	public static string GetMemberChangeTemplate(int notificationId)
	{
		if (notificationId < 101 || notificationId > 109)
		{
			return string.Empty;
		}
		return Get(notificationId);
	}

	public static string Get(int id, params object[] args)
	{
		string local = id.GetLocal(UIStringType.Guild);
		if (string.IsNullOrEmpty(local) && id != 3999)
		{
			local = 3999.GetLocal(UIStringType.Guild);
		}
		if (string.IsNullOrEmpty(local))
		{
			return string.Empty;
		}
		if (args == null || args.Length == 0)
		{
			return local;
		}
		try
		{
			return string.Format(local, args);
		}
		catch (FormatException ex)
		{
			Debug.LogError($"[GuildText] STRGuild id={id} 参数格式错误：{ex.Message}");
			return string.Empty;
		}
	}
}
