using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIGuild_Setting_Com : GComponent
{
	private readonly List<GuildTagConfigure> _tagConfigs = new List<GuildTagConfigure>();

	private readonly HashSet<int> _selectedTagIds = new HashSet<int>();

	private bool _initialized;

	private bool _isOpen;

	private bool _isBusy;

	private bool _settingsDirty;

	private bool _sensitiveWordsReady;

	private int _openVersion;

	private int _busyVersion;

	private string _settingsBaseline = string.Empty;

	private GButton _busyButton;

	public System.Action OnRequestClose;

	public GLabel bottom;

	public GList tag_list;

	public GTextInput recruit_notice;

	public UIGuild_Common_Button btn_dissolve;

	public UIGuild_Common_Button btn_settingsure;

	public GTextField txt_editerName;

	public GTextField txt_editerTime;

	public const string URL = "ui://w5bj58pzhtd129";

	public void Init()
	{
		if (!_initialized)
		{
			if (tag_list != null)
			{
				tag_list.itemRenderer = RenderTag;
			}
			if (recruit_notice != null)
			{
				recruit_notice.maxLength = StaticGlobalData.GUILD_EXTERNALANNOUNCEMENT_LIMIT;
			}
			if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
			{
				uICom_PopUpWindow_Bottom.btnsState.selectedIndex = 0;
			}
			CacheTags();
			_initialized = true;
		}
	}

	public void Open(int openVersion)
	{
		_openVersion = openVersion;
		_isOpen = true;
		EndBusy();
		_sensitiveWordsReady = false;
		RefreshSettings();
		EnsureSensitiveWordsLoaded(openVersion).Forget();
	}

	public void AddEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickClose);
		}
		btn_settingsure?.onClick.Add(OnClickSaveSettings);
		btn_dissolve?.onClick.Add(OnClickDisband);
		recruit_notice?.onChanged.Add(OnSettingsDraftChanged);
	}

	public void RemoveEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickClose);
		}
		btn_settingsure?.onClick.Remove(OnClickSaveSettings);
		btn_dissolve?.onClick.Remove(OnClickDisband);
		recruit_notice?.onChanged.Remove(OnSettingsDraftChanged);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.currentGuildChanged.AddListener(OnCurrentGuildChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.currentGuildChanged.RemoveListener(OnCurrentGuildChanged);
	}

	public void ClearData()
	{
		_openVersion++;
		_isOpen = false;
		EndBusy();
		_selectedTagIds.Clear();
		_settingsBaseline = string.Empty;
		_settingsDirty = false;
		_sensitiveWordsReady = false;
		OnRequestClose = null;
		if (recruit_notice != null)
		{
			recruit_notice.text = string.Empty;
		}
	}

	private void CacheTags()
	{
		_tagConfigs.Clear();
		GuildConfigure guild = StaticConfigure.Guild;
		if (guild == null)
		{
			return;
		}
		foreach (GuildTagConfigure tag in guild.Tags)
		{
			if (tag != null && tag.GuildTagType != GuildTagType.None)
			{
				_tagConfigs.Add(tag);
			}
		}
		_tagConfigs.Sort((GuildTagConfigure left, GuildTagConfigure right) => ((int)left.GuildTagType).CompareTo((int)right.GuildTagType));
	}

	private void RefreshSettings()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		Guild guild2 = guild?.CurrentGuild;
		_selectedTagIds.Clear();
		if (guild2 == null)
		{
			if (recruit_notice != null)
			{
				recruit_notice.text = string.Empty;
			}
			if (tag_list != null)
			{
				tag_list.numItems = 0;
			}
			RefreshEditorMeta(0L, 0L);
			SetDisbandVisible(visible: false);
			return;
		}
		foreach (int tagId in guild2.TagIds)
		{
			_selectedTagIds.Add(tagId);
		}
		if (recruit_notice != null)
		{
			recruit_notice.text = guild2.ExAnnouncement ?? string.Empty;
		}
		_settingsBaseline = CreateSettingsBaseline(_selectedTagIds, recruit_notice?.text);
		_settingsDirty = false;
		if (tag_list != null)
		{
			tag_list.numItems = _tagConfigs.Count;
		}
		RefreshEditorMeta(guild2.LastExternalEditorId, guild2.LastExternalEditTime);
		SetDisbandVisible(guild.CanDisbandGuild);
	}

	private void RenderTag(int index, GObject item)
	{
		if (item is GButton gButton && index >= 0 && index < _tagConfigs.Count)
		{
			int guildTagType = (int)_tagConfigs[index].GuildTagType;
			gButton.data = guildTagType;
			gButton.title = _tagConfigs[index].NameID.GetLocal(UIStringType.Guild);
			gButton.selected = _selectedTagIds.Contains(guildTagType);
			gButton.onClick.Set(OnClickTag);
		}
	}

	private void OnClickTag(EventContext context)
	{
		if (_isBusy || !(context.sender is GButton { data: var obj }) || !(obj is int item))
		{
			return;
		}
		if (!_selectedTagIds.Remove(item))
		{
			if (_selectedTagIds.Count >= StaticGlobalData.GUILD_TAG_LIMIT)
			{
				ShowTip(3106, StaticGlobalData.GUILD_TAG_LIMIT);
				return;
			}
			_selectedTagIds.Add(item);
		}
		_settingsDirty = CreateSettingsBaseline(_selectedTagIds, recruit_notice?.text) != _settingsBaseline;
		if (tag_list != null)
		{
			tag_list.numItems = _tagConfigs.Count;
		}
	}

	private void OnSettingsDraftChanged()
	{
		if (_isOpen)
		{
			_settingsDirty = CreateSettingsBaseline(_selectedTagIds, recruit_notice?.text) != _settingsBaseline;
		}
	}

	private async void OnClickSaveSettings()
	{
		if (_isBusy)
		{
			return;
		}
		GuildLogic guildLogic = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		Guild guild = guildLogic?.CurrentGuild;
		if (guild == null || !guildLogic.CanEditSettings)
		{
			ShowTip(3123);
			return;
		}
		List<int> tagIds = new List<int>(_selectedTagIds);
		tagIds.Sort();
		string announcement = (recruit_notice?.text ?? string.Empty).Trim();
		if (tagIds.Count > StaticGlobalData.GUILD_TAG_LIMIT || announcement.Length > StaticGlobalData.GUILD_EXTERNALANNOUNCEMENT_LIMIT)
		{
			return;
		}
		if (CreateSettingsBaseline(tagIds, announcement) == _settingsBaseline)
		{
			ShowTip(3107);
			return;
		}
		if (guild.ExternalEditCount >= StaticGlobalData.GUILD_SETTINGS_DAILYLIMIT)
		{
			ShowTip(3101);
			return;
		}
		if (!_sensitiveWordsReady)
		{
			ShowTip(3109);
			return;
		}
		int version = _openVersion;
		BeginBusy(btn_settingsure, version);
		bool flag = !string.IsNullOrEmpty(announcement);
		if (flag)
		{
			flag = !(await SensitiveWords.Valid(announcement));
		}
		if (flag)
		{
			if (IsCurrentOpen(version))
			{
				ShowTip(3100);
			}
			EndBusy(btn_settingsure, version);
		}
		else
		{
			if (!IsCurrentOpen(version))
			{
				return;
			}
			RPCAsyncResult rPCAsyncResult = guildLogic.UpdateSettings(tagIds, announcement);
			if (rPCAsyncResult == null)
			{
				EndBusy(btn_settingsure, version);
				return;
			}
			rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
			{
				if (IsCurrentOpen(version))
				{
					EndBusy(btn_settingsure, version);
					if (rpcResult.errId == 0)
					{
						ShowTip(3102);
						OnRequestClose?.Invoke();
					}
				}
			});
		}
	}

	private void OnClickDisband()
	{
		if (_isBusy)
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanDisbandGuild)
		{
			int version = _openVersion;
			BeginBusy(btn_dissolve, version);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(3132), delegate
			{
				SendDisbandGuild(version);
			}, delegate
			{
				EndBusy(btn_dissolve, version);
			}).Forget();
		}
	}

	private void SendDisbandGuild(int version)
	{
		if (IsCurrentOpen(version))
		{
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			if (guild != null && guild.CanDisbandGuild)
			{
				RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild.DisbandCurrentGuild();
				if (rPCAsyncResult == null)
				{
					EndBusy(btn_dissolve, version);
					return;
				}
				rPCAsyncResult.OnFinished.AddOnce(delegate
				{
					EndBusy(btn_dissolve, version);
				});
				return;
			}
		}
		EndBusy(btn_dissolve, version);
	}

	private void OnCurrentGuildChanged()
	{
		if (_isOpen && !_settingsDirty)
		{
			RefreshSettings();
		}
	}

	private async UniTask EnsureSensitiveWordsLoaded(int version)
	{
		try
		{
			await StaticConfigure.LoadSensitiveWords();
			if (IsCurrentOpen(version))
			{
				_sensitiveWordsReady = true;
			}
		}
		catch (Exception arg)
		{
			Debug.LogError($"[GuildSettings] 敏感词资源加载失败：{arg}");
		}
	}

	private void RefreshEditorMeta(long editorId, long timestamp)
	{
		string value = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetGuildMemberName(editorId) ?? string.Empty;
		txt_editerName?.SetVar("name", value).FlushVars();
		if (txt_editerTime != null)
		{
			GTextField gTextField = txt_editerTime;
			string obj;
			if (timestamp <= 0)
			{
				obj = string.Empty;
			}
			else
			{
				DateTime originUtcDT = TimeUtils.OriginUtcDT;
				obj = originUtcDT.AddSeconds(timestamp).ToString("yyyy/MM/dd HH:mm");
			}
			gTextField.text = obj;
		}
	}

	private void SetDisbandVisible(bool visible)
	{
		if (btn_dissolve != null)
		{
			btn_dissolve.visible = visible;
			btn_dissolve.touchable = visible;
		}
	}

	private static string CreateSettingsBaseline(IEnumerable<int> tagIds, string announcement)
	{
		List<int> list = new List<int>(tagIds);
		list.Sort();
		return string.Join(",", list) + "|" + (announcement ?? string.Empty);
	}

	private void BeginBusy(GButton button, int version)
	{
		EndBusy();
		_isBusy = true;
		_busyVersion = version;
		_busyButton = button;
		_busyButton?.onClick.Retain();
	}

	private void EndBusy(GButton button = null, int? version = null)
	{
		if (_busyButton == null)
		{
			_isBusy = false;
			_busyVersion = 0;
		}
		else if ((button == null || button == _busyButton) && (!version.HasValue || version.Value == _busyVersion))
		{
			_busyButton.onClick.Release();
			_busyButton = null;
			_busyVersion = 0;
			_isBusy = false;
		}
	}

	private bool IsCurrentOpen(int version)
	{
		if (_isOpen)
		{
			return version == _openVersion;
		}
		return false;
	}

	private static void ShowTip(int id, params object[] args)
	{
		string text = GuildText.Get(id, args);
		if (!string.IsNullOrEmpty(text))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(text);
		}
	}

	private void OnClickClose()
	{
		OnRequestClose?.Invoke();
	}

	public static UIGuild_Setting_Com CreateInstance()
	{
		return (UIGuild_Setting_Com)UIPackage.CreateObject("Guild", "Guild_Setting_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		tag_list = (GList)GetChildAt(3);
		recruit_notice = (GTextInput)GetChildAt(8);
		btn_dissolve = (UIGuild_Common_Button)GetChildAt(9);
		btn_settingsure = (UIGuild_Common_Button)GetChildAt(10);
		txt_editerName = (GTextField)GetChildAt(12);
		txt_editerTime = (GTextField)GetChildAt(13);
	}
}
