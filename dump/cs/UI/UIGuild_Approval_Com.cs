using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuild_Approval_Com : GComponent
{
	private readonly Queue<long> _profileRequestQueue = new Queue<long>();

	private readonly HashSet<long> _queuedProfileIds = new HashSet<long>();

	private bool _initialized;

	private bool _isOpen;

	private bool _isProfileRequesting;

	private bool _isRejectAllBusy;

	private int _rejectAllBusyVersion;

	private int _openVersion;

	private long _profileRequestPlayerId;

	public System.Action OnRequestClose;

	public GLabel bottom;

	public GList approval_list;

	public UIGuild_Common_Button btn_refuseall;

	public const string URL = "ui://w5bj58pzhtd12j";

	public void Init()
	{
		if (!_initialized)
		{
			if (approval_list != null)
			{
				approval_list.itemRenderer = RenderApplication;
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
		EndRejectAllBusy();
		ResetProfileRequests();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || !guild.CanApproveApplications)
		{
			OnRequestClose?.Invoke();
			return;
		}
		guild.RefreshValidGuildApplications();
		RefreshApplications();
	}

	public void AddEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickClose);
		}
		btn_refuseall?.onClick.Add(OnClickRejectAll);
	}

	public void RemoveEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickClose);
		}
		btn_refuseall?.onClick.Remove(OnClickRejectAll);
	}

	public void AddListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildApplicationsChanged.AddListener(OnGuildApplicationsChanged);
		guild?.signal.guildMembersChanged.AddListener(OnGuildMembersChanged);
	}

	public void RemoveListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildApplicationsChanged.RemoveListener(OnGuildApplicationsChanged);
		guild?.signal.guildMembersChanged.RemoveListener(OnGuildMembersChanged);
	}

	public void ClearData()
	{
		_openVersion++;
		_isOpen = false;
		ResetProfileRequests();
		EndRejectAllBusy();
		OnRequestClose = null;
	}

	private void RefreshApplications()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		IReadOnlyList<GuildApplication> readOnlyList = guild?.ValidGuildApplications;
		int num = readOnlyList?.Count ?? 0;
		if (approval_list != null)
		{
			approval_list.numItems = num;
		}
		if (btn_refuseall != null)
		{
			bool flag = !_isRejectAllBusy && guild != null && guild.CanApproveApplications && num > 0;
			btn_refuseall.visible = num > 0;
			btn_refuseall.touchable = flag;
			btn_refuseall.grayed = !flag;
		}
		QueueMissingProfiles(readOnlyList);
	}

	private void RenderApplication(int index, GObject item)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		IReadOnlyList<GuildApplication> readOnlyList = guild?.ValidGuildApplications;
		if (item is UIGuild_ApprovalItem_Com uIGuild_ApprovalItem_Com && readOnlyList != null && index >= 0 && index < readOnlyList.Count)
		{
			GuildApplication guildApplication = readOnlyList[index];
			FriendData friendData = null;
			SimpleSingletonProvider<GameLogicManager>.inst.friend?.TryGetFriendData(guildApplication.PlayerId, out friendData);
			uIGuild_ApprovalItem_Com.Bind(guildApplication, friendData, guild.IsGuildApplicationRequesting(guildApplication.PlayerId), OnViewApplication, OnRejectApplication, OnAcceptApplication);
		}
	}

	private void QueueMissingProfiles(IReadOnlyList<GuildApplication> applications)
	{
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (friend == null || applications == null)
		{
			return;
		}
		for (int i = 0; i < applications.Count; i++)
		{
			long playerId = applications[i].PlayerId;
			if (playerId != 0L && !friend.TryGetFriendData(playerId, out var _) && _queuedProfileIds.Add(playerId))
			{
				_profileRequestQueue.Enqueue(playerId);
			}
		}
		RequestNextProfile();
	}

	private void RequestNextProfile()
	{
		if (!_isOpen || _isProfileRequesting || _profileRequestQueue.Count == 0)
		{
			return;
		}
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (friend == null)
		{
			return;
		}
		long playerId = _profileRequestQueue.Dequeue();
		if (friend.TryGetFriendData(playerId, out var _))
		{
			_queuedProfileIds.Remove(playerId);
			RequestNextProfile();
			return;
		}
		int version = _openVersion;
		_isProfileRequesting = true;
		_profileRequestPlayerId = playerId;
		friend.RequestSearchPlayerC2S(playerId, delegate(RPCAsyncResult result)
		{
			OnProfileRequestFinished(result, playerId, version);
		});
	}

	private void OnProfileRequestFinished(RPCAsyncResult result, long playerId, int version)
	{
		if (_isOpen && version == _openVersion && _profileRequestPlayerId == playerId)
		{
			_isProfileRequesting = false;
			_profileRequestPlayerId = 0L;
			_queuedProfileIds.Remove(playerId);
			if (result.errId == 0 && approval_list != null)
			{
				approval_list.numItems = SimpleSingletonProvider<GameLogicManager>.inst.guild?.ValidGuildApplications.Count ?? 0;
			}
			RequestNextProfile();
		}
	}

	private void ResetProfileRequests()
	{
		_profileRequestQueue.Clear();
		_queuedProfileIds.Clear();
		_isProfileRequesting = false;
		_profileRequestPlayerId = 0L;
	}

	private void OnViewApplication(long playerId)
	{
		if (_isOpen && HasValidApplication(playerId))
		{
			FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
			if (friend != null && friend.TryGetFriendData(playerId, out var friendData))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(playerId, friendData.Nick, friendData.LV, friendData.HeadURL, friendData.Label, null);
			}
		}
	}

	private void OnAcceptApplication(long playerId)
	{
		if (_isOpen && HasValidApplication(playerId))
		{
			RPCAsyncResult result = SimpleSingletonProvider<GameLogicManager>.inst.guild?.AcceptGuildApplication(playerId);
			TrackApplicationRequest(result, 3163);
		}
	}

	private void OnRejectApplication(long playerId)
	{
		if (_isOpen && HasValidApplication(playerId))
		{
			RPCAsyncResult result = SimpleSingletonProvider<GameLogicManager>.inst.guild?.RejectGuildApplication(playerId);
			TrackApplicationRequest(result, 3164);
		}
	}

	private void TrackApplicationRequest(RPCAsyncResult result, int successMessageId)
	{
		if (result == null)
		{
			return;
		}
		int version = _openVersion;
		if (approval_list != null)
		{
			approval_list.numItems = SimpleSingletonProvider<GameLogicManager>.inst.guild?.ValidGuildApplications.Count ?? 0;
		}
		result.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (_isOpen && version == _openVersion)
			{
				if (rpcResult.errId == 0)
				{
					ShowTip(successMessageId);
				}
				RefreshApplications();
			}
		});
	}

	private static bool HasValidApplication(long playerId)
	{
		IReadOnlyList<GuildApplication> readOnlyList = SimpleSingletonProvider<GameLogicManager>.inst.guild?.ValidGuildApplications;
		if (readOnlyList == null)
		{
			return false;
		}
		for (int i = 0; i < readOnlyList.Count; i++)
		{
			if (readOnlyList[i].PlayerId == playerId)
			{
				return true;
			}
		}
		return false;
	}

	private void OnClickRejectAll()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (!_isRejectAllBusy && guild != null && guild.ValidGuildApplications.Count != 0)
		{
			int version = _openVersion;
			BeginRejectAllBusy(version);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(GuildText.Get(3165), delegate
			{
				SendRejectAll(version);
			}, delegate
			{
				EndRejectAllBusy(version);
			}).Forget();
		}
	}

	private void SendRejectAll(int version)
	{
		if (_isOpen && version == _openVersion)
		{
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			if (guild != null && guild.CanApproveApplications)
			{
				RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild.RejectAllGuildApplications();
				if (rPCAsyncResult == null)
				{
					EndRejectAllBusy(version);
					return;
				}
				int requestVersion = _openVersion;
				RefreshApplications();
				rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
				{
					if (_isOpen && requestVersion == _openVersion)
					{
						EndRejectAllBusy(requestVersion);
						if (rpcResult.errId == 0)
						{
							ShowTip(3166);
						}
						RefreshApplications();
					}
				});
				return;
			}
		}
		EndRejectAllBusy(version);
	}

	private void BeginRejectAllBusy(int version)
	{
		EndRejectAllBusy();
		_isRejectAllBusy = true;
		_rejectAllBusyVersion = version;
		btn_refuseall?.onClick.Retain();
	}

	private void EndRejectAllBusy(int? version = null)
	{
		if (_isRejectAllBusy && (!version.HasValue || version.Value == _rejectAllBusyVersion))
		{
			_isRejectAllBusy = false;
			_rejectAllBusyVersion = 0;
			btn_refuseall?.onClick.Release();
		}
	}

	private void OnGuildApplicationsChanged()
	{
		if (_isOpen)
		{
			RefreshApplications();
		}
	}

	private void OnGuildMembersChanged()
	{
		if (_isOpen)
		{
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			if (guild == null || !guild.CanApproveApplications)
			{
				OnRequestClose?.Invoke();
			}
			else
			{
				RefreshApplications();
			}
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

	public static UIGuild_Approval_Com CreateInstance()
	{
		return (UIGuild_Approval_Com)UIPackage.CreateObject("Guild", "Guild_Approval_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		approval_list = (GList)GetChildAt(3);
		btn_refuseall = (UIGuild_Common_Button)GetChildAt(4);
	}
}
