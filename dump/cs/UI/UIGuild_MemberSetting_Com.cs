using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class UIGuild_MemberSetting_Com : GComponent
{
	private readonly List<GuildMemberOperation> _memberOperations = new List<GuildMemberOperation>();

	private bool _initialized;

	private bool _isOpen;

	private bool _isBusy;

	private long _selectedPlayerId;

	private int _openVersion;

	private int _busyVersion;

	private GButton _busyButton;

	private GuildMemberOperation _pendingMemberOperation;

	private GuildTitleType _pendingTargetTitle;

	private string _pendingTargetName = string.Empty;

	public System.Action OnRequestClose;

	public GLabel bottom;

	public GComponent member_label;

	public GTextField txt_memberJob;

	public GTextField txt_memberTime;

	public GTextField txt_memberActive;

	public UIGuild_Common_Button btn_memberDetail;

	public UIGuild_Common_Button btn_exitGuild;

	public UIGuild_Common_Button btn_expelGuild;

	public GList operation_list;

	public const string URL = "ui://w5bj58pzhtd12b";

	public void Init()
	{
		if (!_initialized)
		{
			if (operation_list != null)
			{
				operation_list.itemRenderer = RenderMemberOperation;
			}
			if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
			{
				uICom_PopUpWindow_Bottom.btnsState.selectedIndex = 0;
			}
			_initialized = true;
		}
	}

	public void Open(long playerId, int openVersion)
	{
		_selectedPlayerId = playerId;
		_openVersion = openVersion;
		_isOpen = true;
		EndBusy();
		ResetPendingOperation();
		RefreshMemberSettings();
	}

	public void AddEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickClose);
		}
		btn_memberDetail?.onClick.Add(OnClickMemberDetail);
		btn_exitGuild?.onClick.Add(OnClickExitGuild);
		btn_expelGuild?.onClick.Add(OnClickKickMember);
	}

	public void RemoveEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickClose);
		}
		btn_memberDetail?.onClick.Remove(OnClickMemberDetail);
		btn_exitGuild?.onClick.Remove(OnClickExitGuild);
		btn_expelGuild?.onClick.Remove(OnClickKickMember);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.guildMembersChanged.AddListener(OnGuildMembersChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.guildMembersChanged.RemoveListener(OnGuildMembersChanged);
	}

	public void ClearData()
	{
		_openVersion++;
		_isOpen = false;
		_selectedPlayerId = 0L;
		_memberOperations.Clear();
		ResetPendingOperation();
		EndBusy();
		OnRequestClose = null;
	}

	private void RefreshMemberSettings()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		GuildMember guildMember = guild?.GetGuildMember(_selectedPlayerId);
		FriendShowPlayerInfo info = guild?.GetGuildMemberInfo(_selectedPlayerId);
		if (guildMember == null)
		{
			SetFixedButtonsVisible(detail: false, exit: false, kick: false);
			_memberOperations.Clear();
			if (operation_list != null)
			{
				operation_list.numItems = 0;
			}
			if (_isOpen && !_isBusy)
			{
				ShowTip(3143);
				OnRequestClose?.Invoke();
			}
			return;
		}
		RefreshPlayerLabel(guildMember, info);
		if (txt_memberJob != null)
		{
			txt_memberJob.SetVar("job", GuildText.GetTitle((GuildTitleType)guildMember.Role)).FlushVars();
		}
		RefreshMemberTime(guildMember, info);
		if (txt_memberActive != null)
		{
			txt_memberActive.SetVar("num", guildMember.WeeklyActivity.ToString()).FlushVars();
		}
		bool exit = _selectedPlayerId == guild.SelfPlayerId;
		SetFixedButtonsVisible(detail: true, exit, guild.CanKickMember(_selectedPlayerId));
		RebuildMemberOperations(guild, guildMember);
	}

	private void RefreshPlayerLabel(GuildMember member, FriendShowPlayerInfo info)
	{
		if (member_label is UICom_PlayerLabel com_Label)
		{
			string playerName = (string.IsNullOrEmpty(info?.Name) ? (member.PlayerName ?? string.Empty) : info.Name);
			CommonUIManager.RendererLabelInfo(com_Label, playerName, info?.Lv ?? 0);
			CommonUIManager.RendererHeadShot(com_Label, GetHeadIconUrl(info?.HeadIcon ?? 0), isVideo: false);
			(string, bool) playerLabel = GetPlayerLabel(info?.Background ?? 0);
			CommonUIManager.RendererLabel(UIType.Panel, 55, com_Label, playerLabel.Item1, playerLabel.Item2);
		}
	}

	private void RefreshMemberTime(GuildMember member, FriendShowPlayerInfo info)
	{
		if (txt_memberTime != null)
		{
			long num = ((!(info?.IsOnline ?? member.Online)) ? (info?.OfflineTime ?? 0) : (info?.Time ?? 0));
			long num2 = ((num > 0) ? num : member.LastLoginTime);
			string obj;
			if (num2 <= 0)
			{
				obj = string.Empty;
			}
			else
			{
				DateTime originUtcDT = TimeUtils.OriginUtcDT;
				obj = originUtcDT.AddSeconds(num2).ToString("yyyy/MM/dd");
			}
			string value = obj;
			txt_memberTime.SetVar("time", value).FlushVars();
		}
	}

	private void SetFixedButtonsVisible(bool detail, bool exit, bool kick)
	{
		SetButtonVisible(btn_memberDetail, detail);
		SetButtonVisible(btn_exitGuild, exit);
		SetButtonVisible(btn_expelGuild, kick);
	}

	private void RebuildMemberOperations(GuildLogic guild, GuildMember selectedMember)
	{
		_memberOperations.Clear();
		if (_selectedPlayerId != guild.SelfPlayerId)
		{
			if (guild.CanTransferMaster(_selectedPlayerId))
			{
				_memberOperations.Add(GuildMemberOperation.TRANSFER);
			}
			if (guild.CanPromoteMember(_selectedPlayerId, out var targetTitle))
			{
				_memberOperations.Add(GuildMemberOperation.PROMOTE);
			}
			if (guild.CanDemoteMember(_selectedPlayerId, out targetTitle))
			{
				_memberOperations.Add(GuildMemberOperation.DEMOTE);
			}
			if (selectedMember.Role == 1 && guild.CanImpeachMaster())
			{
				_memberOperations.Add(GuildMemberOperation.IMPEACH);
			}
		}
		if (operation_list != null)
		{
			operation_list.numItems = _memberOperations.Count;
		}
	}

	private void RenderMemberOperation(int index, GObject item)
	{
		if (item is UIGuild_Common_Button uIGuild_Common_Button && index >= 0 && index < _memberOperations.Count)
		{
			GuildMemberOperation guildMemberOperation = _memberOperations[index];
			uIGuild_Common_Button.data = guildMemberOperation;
			uIGuild_Common_Button.title = GuildText.Get(GetOperationTitleId(guildMemberOperation));
			uIGuild_Common_Button.onClick.Set(OnClickMemberOperation);
		}
	}

	private void OnClickMemberOperation(EventContext context)
	{
		if (!_isBusy && context.sender is UIGuild_Common_Button { data: var obj } uIGuild_Common_Button && obj is GuildMemberOperation operation)
		{
			ExecuteMemberOperation(operation, uIGuild_Common_Button).Forget();
		}
	}

	private async UniTask ExecuteMemberOperation(GuildMemberOperation operation, GButton sourceButton)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild?.GetGuildMember(_selectedPlayerId) == null)
		{
			ShowTip(3122);
			return;
		}
		string guildMemberName = guild.GetGuildMemberName(_selectedPlayerId);
		int num;
		object[] args;
		switch (operation)
		{
		case GuildMemberOperation.TRANSFER:
			if (guild.CanTransferMaster(_selectedPlayerId))
			{
				num = 3110;
				args = new object[1] { guildMemberName };
				break;
			}
			goto default;
		case GuildMemberOperation.PROMOTE:
		{
			if (guild.CanPromoteMember(_selectedPlayerId, out var targetTitle2))
			{
				num = 3112;
				args = new object[2]
				{
					guildMemberName,
					GuildText.GetTitle(targetTitle2)
				};
				break;
			}
			goto default;
		}
		case GuildMemberOperation.DEMOTE:
		{
			if (guild.CanDemoteMember(_selectedPlayerId, out var targetTitle))
			{
				num = 3114;
				args = new object[2]
				{
					guildMemberName,
					GuildText.GetTitle(targetTitle)
				};
				break;
			}
			goto default;
		}
		case GuildMemberOperation.IMPEACH:
			if (guild.CanImpeachMaster())
			{
				num = 3120;
				args = Array.Empty<object>();
				break;
			}
			goto default;
		default:
			ShowTip(3122);
			RefreshMemberSettings();
			return;
		}
		_pendingMemberOperation = operation;
		_pendingTargetName = guildMemberName;
		_pendingTargetTitle = GuildTitleType.None;
		if (operation == GuildMemberOperation.PROMOTE)
		{
			guild.CanPromoteMember(_selectedPlayerId, out _pendingTargetTitle);
		}
		else if (operation == GuildMemberOperation.DEMOTE)
		{
			guild.CanDemoteMember(_selectedPlayerId, out _pendingTargetTitle);
		}
		int version = _openVersion;
		long playerId = _selectedPlayerId;
		BeginBusy(sourceButton, version);
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(num, args), delegate
		{
			SendMemberOperation(operation, playerId, version, sourceButton);
		}, delegate
		{
			EndBusy(sourceButton, version);
		});
	}

	private void SendMemberOperation(GuildMemberOperation operation, long playerId, int version, GButton sourceButton)
	{
		if (!_isOpen || version != _openVersion || _selectedPlayerId != playerId || _pendingMemberOperation != operation)
		{
			EndBusy(sourceButton, version);
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		RPCAsyncResult rPCAsyncResult = null;
		switch (operation)
		{
		case GuildMemberOperation.TRANSFER:
			rPCAsyncResult = guild?.TransferMaster(_selectedPlayerId);
			break;
		case GuildMemberOperation.PROMOTE:
		{
			if (guild != null && guild.CanPromoteMember(_selectedPlayerId, out var targetTitle))
			{
				rPCAsyncResult = guild.ChangeMemberTitle(_selectedPlayerId, targetTitle);
			}
			else if (guild != null && guild.IsPromotionBlockedByViceMasterLimit(_selectedPlayerId))
			{
				ShowTip(3119);
			}
			break;
		}
		case GuildMemberOperation.DEMOTE:
		{
			if (guild != null && guild.CanDemoteMember(_selectedPlayerId, out var targetTitle2))
			{
				rPCAsyncResult = guild.ChangeMemberTitle(_selectedPlayerId, targetTitle2);
			}
			break;
		}
		case GuildMemberOperation.IMPEACH:
			rPCAsyncResult = guild?.ImpeachMaster();
			break;
		}
		if (rPCAsyncResult == null)
		{
			EndBusy(sourceButton, version);
			RefreshMemberSettings();
			return;
		}
		string targetName = _pendingTargetName;
		GuildTitleType targetTitle3 = _pendingTargetTitle;
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (_isOpen && version == _openVersion && _selectedPlayerId == playerId)
			{
				EndBusy(sourceButton, version);
				if (rpcResult.errId == 0)
				{
					ShowMemberOperationSucceeded(operation, targetName, targetTitle3);
					OnRequestClose?.Invoke();
				}
			}
		});
	}

	private void OnClickMemberDetail()
	{
		if (_isBusy)
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		GuildMember guildMember = guild?.GetGuildMember(_selectedPlayerId);
		FriendShowPlayerInfo friendShowPlayerInfo = guild?.GetGuildMemberInfo(_selectedPlayerId);
		if (guildMember == null || friendShowPlayerInfo == null)
		{
			ShowTip(3142);
			return;
		}
		string headIconUrl = GetHeadIconUrl(friendShowPlayerInfo.HeadIcon);
		(string, bool) playerLabel = GetPlayerLabel(friendShowPlayerInfo.Background);
		int version = _openVersion;
		long playerId = _selectedPlayerId;
		BeginBusy(btn_memberDetail, version);
		SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(playerId, string.IsNullOrEmpty(friendShowPlayerInfo.Name) ? guildMember.PlayerName : friendShowPlayerInfo.Name, friendShowPlayerInfo.Lv, headIconUrl, playerLabel, delegate
		{
			if (_isOpen && version == _openVersion && _selectedPlayerId == playerId)
			{
				EndBusy(btn_memberDetail, version);
			}
		});
	}

	private void OnClickExitGuild()
	{
		if (!_isBusy && _selectedPlayerId == SimpleSingletonProvider<GameLogicManager>.inst.guild?.SelfPlayerId)
		{
			int version = _openVersion;
			BeginBusy(btn_exitGuild, version);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(3130), delegate
			{
				SendExitGuild(version);
			}, delegate
			{
				EndBusy(btn_exitGuild, version);
			}).Forget();
		}
	}

	private void SendExitGuild(int version)
	{
		if (!_isOpen || version != _openVersion || _selectedPlayerId != SimpleSingletonProvider<GameLogicManager>.inst.guild?.SelfPlayerId)
		{
			EndBusy(btn_exitGuild, version);
			return;
		}
		RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild?.ExitCurrentGuild();
		if (rPCAsyncResult == null)
		{
			EndBusy(btn_exitGuild, version);
			return;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate
		{
			EndBusy(btn_exitGuild, version);
		});
	}

	private void OnClickKickMember()
	{
		if (_isBusy)
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || !guild.CanKickMember(_selectedPlayerId))
		{
			ShowTip(3122);
			RefreshMemberSettings();
			return;
		}
		_pendingTargetName = guild.GetGuildMemberName(_selectedPlayerId);
		int version = _openVersion;
		long playerId = _selectedPlayerId;
		BeginBusy(btn_expelGuild, version);
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(3116, _pendingTargetName), delegate
		{
			SendKickMember(playerId, version);
		}, delegate
		{
			EndBusy(btn_expelGuild, version);
		}).Forget();
	}

	private void SendKickMember(long playerId, int version)
	{
		if (!_isOpen || version != _openVersion || _selectedPlayerId != playerId)
		{
			EndBusy(btn_expelGuild, version);
			return;
		}
		string targetName = _pendingTargetName;
		RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild?.KickMember(_selectedPlayerId);
		if (rPCAsyncResult == null)
		{
			EndBusy(btn_expelGuild, version);
			return;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (_isOpen && version == _openVersion && _selectedPlayerId == playerId)
			{
				EndBusy(btn_expelGuild, version);
				if (rpcResult.errId == 0)
				{
					ShowTip(3117, targetName);
					OnRequestClose?.Invoke();
				}
			}
		});
	}

	private void OnGuildMembersChanged()
	{
		if (_isOpen)
		{
			RefreshMemberSettings();
		}
	}

	private static void ShowMemberOperationSucceeded(GuildMemberOperation operation, string targetName, GuildTitleType targetTitle)
	{
		switch (operation)
		{
		case GuildMemberOperation.TRANSFER:
			ShowTip(3111, targetName);
			break;
		case GuildMemberOperation.PROMOTE:
			ShowTip(3113, targetName, GuildText.GetTitle(targetTitle));
			break;
		case GuildMemberOperation.DEMOTE:
			ShowTip(3115, targetName, GuildText.GetTitle(targetTitle));
			break;
		case GuildMemberOperation.IMPEACH:
			ShowTip(3121);
			break;
		}
	}

	private void ResetPendingOperation()
	{
		_pendingMemberOperation = GuildMemberOperation.NONE;
		_pendingTargetTitle = GuildTitleType.None;
		_pendingTargetName = string.Empty;
	}

	private static void SetButtonVisible(GButton button, bool visible)
	{
		if (button != null)
		{
			button.visible = visible;
			button.touchable = visible;
		}
	}

	private static string GetHeadIconUrl(int headIconId)
	{
		if (headIconId == 0)
		{
			return string.Empty;
		}
		ItemInfoConfigure itemInfoConfigure = headIconId.GetItemInfoConfigure();
		if (itemInfoConfigure != null)
		{
			return itemInfoConfigure.SubMeterID.GetFashionAccountHeadShot();
		}
		return string.Empty;
	}

	private static (string, bool) GetPlayerLabel(int backgroundId)
	{
		if (backgroundId == 0)
		{
			return (string.Empty, false);
		}
		return (backgroundId.GetItemInfoConfigure()?.SubMeterID.GetFashionAccountBackgroundConfigure())?.GetPlayerLabel() ?? (string.Empty, false);
	}

	private static int GetOperationTitleId(GuildMemberOperation operation)
	{
		return operation switch
		{
			GuildMemberOperation.TRANSFER => 3127, 
			GuildMemberOperation.PROMOTE => 3128, 
			GuildMemberOperation.DEMOTE => 3129, 
			GuildMemberOperation.IMPEACH => 3154, 
			_ => 3999, 
		};
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

	public static UIGuild_MemberSetting_Com CreateInstance()
	{
		return (UIGuild_MemberSetting_Com)UIPackage.CreateObject("Guild", "Guild_MemberSetting_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		member_label = (GComponent)GetChildAt(3);
		txt_memberJob = (GTextField)GetChildAt(4);
		txt_memberTime = (GTextField)GetChildAt(5);
		txt_memberActive = (GTextField)GetChildAt(6);
		btn_memberDetail = (UIGuild_Common_Button)GetChildAt(7);
		btn_exitGuild = (UIGuild_Common_Button)GetChildAt(8);
		btn_expelGuild = (UIGuild_Common_Button)GetChildAt(9);
		operation_list = (GList)GetChildAt(10);
	}
}
