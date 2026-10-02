using System;
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

public class UIGuild_Notice_Com : GComponent
{
	private bool _initialized;

	private bool _isOpen;

	private bool _isBusy;

	private bool _internalDirty;

	private bool _internalConflict;

	private bool _sensitiveWordsReady;

	private int _openVersion;

	private int _busyVersion;

	private string _internalBaseline = string.Empty;

	private long _internalBaselineEditTime;

	private GButton _busyButton;

	public System.Action OnRequestClose;

	public GLabel bottom;

	public GTextInput input_notice;

	public UIGuild_Common_Button btn_notice_sure;

	public GTextField txt_editerName;

	public GTextField txt_editerTime;

	public const string URL = "ui://w5bj58pzhtd12a";

	public void Init()
	{
		if (!_initialized)
		{
			if (input_notice != null)
			{
				input_notice.maxLength = StaticGlobalData.GUILD_INTERNALANNOUNCEMENT_LIMIT;
			}
			if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
			{
				uICom_PopUpWindow_Bottom.btnsState.selectedIndex = 0;
			}
			_initialized = true;
		}
	}

	public void Open(int openVersion)
	{
		_openVersion = openVersion;
		_isOpen = true;
		EndBusy();
		_sensitiveWordsReady = false;
		RefreshInternalAnnouncement(fromSync: false);
		EnsureSensitiveWordsLoaded(openVersion).Forget();
	}

	public void AddEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickClose);
		}
		btn_notice_sure?.onClick.Add(OnClickSaveInternalAnnouncement);
		input_notice?.onChanged.Add(OnInternalDraftChanged);
	}

	public void RemoveEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickClose);
		}
		btn_notice_sure?.onClick.Remove(OnClickSaveInternalAnnouncement);
		input_notice?.onChanged.Remove(OnInternalDraftChanged);
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
		_internalBaseline = string.Empty;
		_internalBaselineEditTime = 0L;
		_internalDirty = false;
		_internalConflict = false;
		_sensitiveWordsReady = false;
		OnRequestClose = null;
		if (input_notice != null)
		{
			input_notice.text = string.Empty;
		}
	}

	private void RefreshInternalAnnouncement(bool fromSync)
	{
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.CurrentGuild;
		if (guild == null)
		{
			if (!fromSync && input_notice != null)
			{
				input_notice.text = string.Empty;
			}
			RefreshEditorMeta(0L, 0L);
			return;
		}
		string text = guild.InAnnouncement ?? string.Empty;
		bool flag = _internalBaseline != text || _internalBaselineEditTime != guild.LastInternalEditTime;
		if (fromSync && _internalDirty)
		{
			bool flag2 = (input_notice?.text ?? string.Empty) != text;
			_internalConflict = flag2 && (_internalConflict || flag);
			_internalDirty = flag2;
		}
		else
		{
			if (input_notice != null)
			{
				input_notice.text = text;
			}
			_internalDirty = false;
			_internalConflict = false;
		}
		_internalBaseline = text;
		_internalBaselineEditTime = guild.LastInternalEditTime;
		RefreshEditorMeta(guild.LastInternalEditorId, guild.LastInternalEditTime);
	}

	private void OnInternalDraftChanged()
	{
		if (_isOpen)
		{
			_internalDirty = (input_notice?.text ?? string.Empty) != _internalBaseline;
		}
	}

	private void OnCurrentGuildChanged()
	{
		if (_isOpen)
		{
			RefreshInternalAnnouncement(fromSync: true);
		}
	}

	private async void OnClickSaveInternalAnnouncement()
	{
		if (_isBusy)
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		Guild guild2 = guild?.CurrentGuild;
		if (guild2 == null || !guild.CanEditInternalAnnouncement)
		{
			ShowTip(3123);
			return;
		}
		string draft = (input_notice?.text ?? string.Empty).Trim();
		if (draft.Length > StaticGlobalData.GUILD_INTERNALANNOUNCEMENT_LIMIT)
		{
			return;
		}
		if (guild2.InternalEditCount >= StaticGlobalData.GUILD_INTERNALANNOUNCEMENT_DAILYLIMIT)
		{
			ShowTip(3104);
			return;
		}
		if (draft == (guild2.InAnnouncement ?? string.Empty) && !_internalConflict)
		{
			ShowTip(3108);
			return;
		}
		if (!_sensitiveWordsReady)
		{
			ShowTip(3109);
			return;
		}
		int version = _openVersion;
		BeginBusy(btn_notice_sure, version);
		bool flag = !string.IsNullOrEmpty(draft);
		if (flag)
		{
			flag = !(await SensitiveWords.Valid(draft));
		}
		if (flag)
		{
			if (IsCurrentOpen(version))
			{
				ShowTip(3103);
			}
			EndBusy(btn_notice_sure, version);
		}
		else
		{
			if (!IsCurrentOpen(version))
			{
				return;
			}
			if (_internalConflict)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(3153), delegate
				{
					SendInternalAnnouncement(draft, version);
				}, delegate
				{
					EndBusy(btn_notice_sure, version);
				});
			}
			else
			{
				SendInternalAnnouncement(draft, version);
			}
		}
	}

	private void SendInternalAnnouncement(string draft, int version)
	{
		if (!IsCurrentOpen(version))
		{
			EndBusy(btn_notice_sure, version);
			return;
		}
		RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild?.UpdateInternalAnnouncement(draft);
		if (rPCAsyncResult == null)
		{
			EndBusy(btn_notice_sure, version);
			return;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (IsCurrentOpen(version))
			{
				EndBusy(btn_notice_sure, version);
				if (rpcResult.errId == 0)
				{
					ShowTip(3105);
					OnRequestClose?.Invoke();
				}
			}
		});
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
			Debug.LogError($"[GuildNotice] 敏感词资源加载失败：{arg}");
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

	public static UIGuild_Notice_Com CreateInstance()
	{
		return (UIGuild_Notice_Com)UIPackage.CreateObject("Guild", "Guild_Notice_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		input_notice = (GTextInput)GetChildAt(3);
		btn_notice_sure = (UIGuild_Common_Button)GetChildAt(4);
		txt_editerName = (GTextField)GetChildAt(6);
		txt_editerTime = (GTextField)GetChildAt(7);
	}
}
