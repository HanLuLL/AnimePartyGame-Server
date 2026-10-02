using System;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class UIGuild_Com_GuildMenberView : GComponent
{
	private readonly List<long> _sortedMemberIds = new List<long>();

	private bool _initialized;

	public Action<long> OnOpenMemberSettings;

	public System.Action OnOpenMemberChanges;

	public System.Action OnOpenInvite;

	public System.Action OnOpenApproval;

	public GTextField memberTitle;

	public GList member_list;

	public UIGuild_Common_Button btn_memberChange;

	public UIGuild_Common_Button btn_invite;

	public UIGuild_Common_Button btn_approval;

	public const string URL = "ui://w5bj58pzg04j10";

	public void Init()
	{
		if (!_initialized)
		{
			member_list.SetVirtual();
			member_list.itemRenderer = RenderMember;
			_initialized = true;
		}
	}

	public void OnShow()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			RebuildMemberIds();
			if (!guild.HasCurrentGuildSnapshot)
			{
				guild.RequestCurrentGuild();
			}
			else if (!guild.HasMemberInfoSnapshot)
			{
				guild.RequestMemberShowInfos();
			}
		}
	}

	public void OnHide()
	{
		_sortedMemberIds.Clear();
	}

	public void AddEvent()
	{
		btn_memberChange?.onClick.Add(OnClickMemberChanges);
		btn_invite?.onClick.Add(OnClickInvite);
		btn_approval?.onClick.Add(OnClickApproval);
	}

	public void RemoveEvent()
	{
		btn_memberChange?.onClick.Remove(OnClickMemberChanges);
		btn_invite?.onClick.Remove(OnClickInvite);
		btn_approval?.onClick.Remove(OnClickApproval);
	}

	public void AddListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildMembersChanged.AddListener(OnGuildDataChanged);
		guild?.signal.guildApplicationsChanged.AddListener(OnGuildApplicationsChanged);
	}

	public void RemoveListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildMembersChanged.RemoveListener(OnGuildDataChanged);
		guild?.signal.guildApplicationsChanged.RemoveListener(OnGuildApplicationsChanged);
	}

	public void ClearData()
	{
		_sortedMemberIds.Clear();
		OnOpenMemberSettings = null;
		OnOpenMemberChanges = null;
		OnOpenInvite = null;
		OnOpenApproval = null;
	}

	private void RebuildMemberIds()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		Guild guild2 = guild?.CurrentGuild;
		_sortedMemberIds.Clear();
		if (guild2 == null)
		{
			memberTitle?.SetVar("num", string.Empty).SetVar("total", string.Empty).FlushVars();
			member_list.numItems = 0;
			return;
		}
		foreach (KeyValuePair<long, GuildMember> member in guild2.Members)
		{
			if (member.Value != null)
			{
				_sortedMemberIds.Add(member.Key);
			}
		}
		_sortedMemberIds.Sort(CompareMembers);
		memberTitle?.SetVar("num", guild2.Members.Count.ToString()).SetVar("total", StaticGlobalData.GUILD_MEMBER_LIMIT.ToString()).FlushVars();
		member_list.numItems = _sortedMemberIds.Count;
		member_list.RefreshVirtualList();
		RefreshPermissionButtons(guild);
	}

	private int CompareMembers(long leftId, long rightId)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		long num = guild?.SelfPlayerId ?? 0;
		bool flag = leftId == num;
		bool flag2 = rightId == num;
		if (flag != flag2)
		{
			if (!flag)
			{
				return 1;
			}
			return -1;
		}
		GuildMember guildMember = guild?.GetGuildMember(leftId);
		GuildMember guildMember2 = guild?.GetGuildMember(rightId);
		FriendShowPlayerInfo friendShowPlayerInfo = guild?.GetGuildMemberInfo(leftId);
		FriendShowPlayerInfo friendShowPlayerInfo2 = guild?.GetGuildMemberInfo(rightId);
		bool flag3 = friendShowPlayerInfo?.IsOnline ?? guildMember?.Online ?? false;
		bool flag4 = friendShowPlayerInfo2?.IsOnline ?? guildMember2?.Online ?? false;
		if (flag3 != flag4)
		{
			if (!flag3)
			{
				return 1;
			}
			return -1;
		}
		long sortTime = GetSortTime(guildMember, friendShowPlayerInfo, flag3);
		int num2 = GetSortTime(guildMember2, friendShowPlayerInfo2, flag4).CompareTo(sortTime);
		if (num2 == 0)
		{
			return leftId.CompareTo(rightId);
		}
		return num2;
	}

	private static long GetSortTime(GuildMember member, FriendShowPlayerInfo info, bool isOnline)
	{
		if (isOnline && info != null && info.Time > 0)
		{
			return info.Time;
		}
		if (!isOnline && info != null && info.OfflineTime > 0)
		{
			return info.OfflineTime;
		}
		return member?.LastLoginTime ?? 0;
	}

	private void RenderMember(int index, GObject item)
	{
		if (item is UIGuild_Member_Item uIGuild_Member_Item)
		{
			if (index < 0 || index >= _sortedMemberIds.Count)
			{
				uIGuild_Member_Item.Render(0L, null, null, hasOperation: false, null);
				return;
			}
			long num = _sortedMemberIds[index];
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			uIGuild_Member_Item.Render(num, guild?.GetGuildMember(num), guild?.GetGuildMemberInfo(num), guild?.HasAnyMemberOperation(num) ?? false, OnOpenMemberSettings);
		}
	}

	private void RefreshPermissionButtons(GuildLogic guild)
	{
		if (btn_memberChange != null)
		{
			btn_memberChange.visible = true;
			btn_memberChange.touchable = true;
		}
		bool flag = guild?.CanInvite ?? false;
		if (btn_invite != null)
		{
			btn_invite.visible = flag;
			btn_invite.touchable = flag;
		}
		bool flag2 = guild?.CanApproveApplications ?? false;
		if (btn_approval != null)
		{
			btn_approval.visible = flag2;
			btn_approval.touchable = flag2;
		}
	}

	private void OnGuildDataChanged()
	{
		RebuildMemberIds();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.HasCurrentGuildSnapshot && !guild.HasMemberInfoSnapshot)
		{
			guild.RequestMemberShowInfos();
		}
	}

	private void OnGuildApplicationsChanged()
	{
		RefreshPermissionButtons(SimpleSingletonProvider<GameLogicManager>.inst.guild);
	}

	private void OnClickMemberChanges()
	{
		OnOpenMemberChanges?.Invoke();
	}

	private void OnClickInvite()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanInvite)
		{
			OnOpenInvite?.Invoke();
		}
	}

	private void OnClickApproval()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanApproveApplications)
		{
			OnOpenApproval?.Invoke();
		}
	}

	public static UIGuild_Com_GuildMenberView CreateInstance()
	{
		return (UIGuild_Com_GuildMenberView)UIPackage.CreateObject("Guild", "Guild_Com_GuildMenberView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		memberTitle = (GTextField)GetChildAt(1);
		member_list = (GList)GetChildAt(3);
		btn_memberChange = (UIGuild_Common_Button)GetChildAt(4);
		btn_invite = (UIGuild_Common_Button)GetChildAt(5);
		btn_approval = (UIGuild_Common_Button)GetChildAt(6);
	}
}
