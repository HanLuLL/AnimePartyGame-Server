using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuild_Com_GuildHomeView : GComponent
{
	private GButton _chatButton;

	private GButton _memberButton;

	private GButton _taskButton;

	private GButton _shopButton;

	private bool _initialized;

	public System.Action OnOpenMemberPage;

	public System.Action OnOpenTaskPage;

	public System.Action OnOpenStorePage;

	public System.Action OnOpenSettings;

	public System.Action OnOpenInternalAnnouncement;

	public GLoader guild_picloader;

	public UIGuild_Com_GuildSummary com_GuildSummary;

	public UIGuild_Com_Announcement com_GuildAnnouncement;

	public GList list_GuildMainNavItems;

	public GButton btn_report;

	public GButton btn_rule;

	public const string URL = "ui://w5bj58pzlwm13";

	public void Init()
	{
		if (!_initialized)
		{
			_chatButton = GetNavigationButton(0);
			_memberButton = GetNavigationButton(1);
			_taskButton = GetNavigationButton(2);
			_shopButton = GetNavigationButton(3);
			_initialized = true;
		}
	}

	public void OnShow()
	{
		RefreshView();
	}

	public void OnHide()
	{
	}

	public void AddEvent()
	{
		_chatButton?.onClick.Add(OnClickChat);
		_memberButton?.onClick.Add(OnClickMember);
		_taskButton?.onClick.Add(OnClickTask);
		_shopButton?.onClick.Add(OnClickShop);
		btn_rule?.onClick.Add(OnClickGuildRule);
		btn_report?.onClick.Add(OnClickReportGuild);
		com_GuildSummary?.btn_GuildSettings?.onClick.Add(OnClickSettings);
		com_GuildAnnouncement?.btn_EditAnnouncement?.onClick.Add(OnClickInternalAnnouncement);
	}

	public void RemoveEvent()
	{
		_chatButton?.onClick.Remove(OnClickChat);
		_memberButton?.onClick.Remove(OnClickMember);
		_taskButton?.onClick.Remove(OnClickTask);
		_shopButton?.onClick.Remove(OnClickShop);
		btn_rule?.onClick.Remove(OnClickGuildRule);
		btn_report?.onClick.Remove(OnClickReportGuild);
		com_GuildSummary?.btn_GuildSettings?.onClick.Remove(OnClickSettings);
		com_GuildAnnouncement?.btn_EditAnnouncement?.onClick.Remove(OnClickInternalAnnouncement);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.currentGuildChanged.AddListener(OnCurrentGuildChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.currentGuildChanged.RemoveListener(OnCurrentGuildChanged);
	}

	public void RefreshView()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		Guild guild2 = guild?.CurrentGuild;
		com_GuildSummary?.txt_GuildName?.SetVar("name", guild2?.Name ?? string.Empty).FlushVars();
		com_GuildSummary?.txt_GuildId?.SetVar("id", guild2?.Id.ToString() ?? string.Empty).FlushVars();
		if (com_GuildSummary?.canManage != null)
		{
			com_GuildSummary.canManage.selectedIndex = ((guild2 != null && guild.CanEditSettings) ? 1 : 0);
		}
		if (com_GuildAnnouncement?.txt_GuildAnnouncement != null)
		{
			com_GuildAnnouncement.txt_GuildAnnouncement.text = ((guild2 == null) ? string.Empty : (string.IsNullOrEmpty(guild2.InAnnouncement) ? GuildText.Get(3150) : guild2.InAnnouncement));
		}
		if (com_GuildAnnouncement?.canEdit != null)
		{
			com_GuildAnnouncement.canEdit.selectedIndex = ((guild2 != null && guild.CanEditInternalAnnouncement) ? 1 : 0);
		}
		RefreshAnnouncementEditorMeta(guild2);
	}

	public void ClearData()
	{
		OnOpenMemberPage = null;
		OnOpenTaskPage = null;
		OnOpenStorePage = null;
		OnOpenSettings = null;
		OnOpenInternalAnnouncement = null;
		com_GuildSummary?.txt_GuildName?.SetVar("name", string.Empty).FlushVars();
		com_GuildSummary?.txt_GuildId?.SetVar("id", string.Empty).FlushVars();
		if (com_GuildAnnouncement?.txt_GuildAnnouncement != null)
		{
			com_GuildAnnouncement.txt_GuildAnnouncement.text = string.Empty;
		}
		if (com_GuildAnnouncement?.txt_AnnouncementUpdatedMeta != null)
		{
			com_GuildAnnouncement.txt_AnnouncementUpdatedMeta.SetVar("time", string.Empty).FlushVars();
		}
	}

	private void RefreshAnnouncementEditorMeta(Guild guild)
	{
		GTextField gTextField = com_GuildAnnouncement?.txt_AnnouncementUpdatedMeta;
		if (gTextField != null)
		{
			if (guild == null || guild.LastInternalEditTime <= 0)
			{
				gTextField.SetVar("time", string.Empty).FlushVars();
				return;
			}
			DateTime originUtcDT = TimeUtils.OriginUtcDT;
			gTextField.SetVar("time", originUtcDT.AddSeconds(guild.LastInternalEditTime).ToString("yyyy/MM/dd")).FlushVars();
		}
	}

	private GButton GetNavigationButton(int index)
	{
		if (list_GuildMainNavItems == null || index < 0 || index >= list_GuildMainNavItems.numChildren)
		{
			return null;
		}
		return list_GuildMainNavItems.GetChildAt(index) as GButton;
	}

	private void OnCurrentGuildChanged()
	{
		RefreshView();
	}

	private void OnClickChat()
	{
	}

	private void OnClickTask()
	{
		OnOpenTaskPage?.Invoke();
	}

	private void OnClickShop()
	{
		OnOpenStorePage?.Invoke();
	}

	private void OnClickGuildRule()
	{
	}

	private void OnClickReportGuild()
	{
	}

	private void OnClickMember()
	{
		OnOpenMemberPage?.Invoke();
	}

	private void OnClickSettings()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanEditSettings)
		{
			OnOpenSettings?.Invoke();
		}
	}

	private void OnClickInternalAnnouncement()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanEditInternalAnnouncement)
		{
			OnOpenInternalAnnouncement?.Invoke();
		}
	}

	public static UIGuild_Com_GuildHomeView CreateInstance()
	{
		return (UIGuild_Com_GuildHomeView)UIPackage.CreateObject("Guild", "Guild_Com_GuildHomeView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		guild_picloader = (GLoader)GetChildAt(1);
		com_GuildSummary = (UIGuild_Com_GuildSummary)GetChildAt(2);
		com_GuildAnnouncement = (UIGuild_Com_Announcement)GetChildAt(3);
		list_GuildMainNavItems = (GList)GetChildAt(4);
		btn_report = (GButton)GetChildAt(5);
		btn_rule = (GButton)GetChildAt(6);
	}
}
