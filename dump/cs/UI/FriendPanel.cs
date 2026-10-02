using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class FriendPanel : BasePanel<UIFriendPanel>
{
	private float _currentFriendListRefreshTime;

	private float _currentAddListRefreshTime;

	private float _currentApplyListRefreshTime;

	private float _currentBlackListRefreshTime;

	private float _currentSearchRefreshTime;

	public const float REFRESH_COOLDOWN = 5f;

	private readonly string[] _onlineStateItems = new string[2]
	{
		2001.GetLocal(UIStringType.Friend),
		2002.GetLocal(UIStringType.Friend)
	};

	private List<long> friendsList;

	private List<FriendData> searchFriends;

	private List<long> applyFriendList;

	private List<long> blackList;

	public FriendPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIFriendPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		UpdateApplyTab(_status: false);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Friends.list_Friends.SetVirtual();
		base.ui.com_Friends.list_Friends.itemRenderer = RendererFriend;
		base.ui.com_AddFriend.list_LastPlayer.SetVirtual();
		base.ui.com_AddFriend.list_LastPlayer.itemRenderer = RendererLastPlayer;
		base.ui.com_AddFriend.list_SearchPlayer.SetVirtual();
		base.ui.com_AddFriend.list_SearchPlayer.itemRenderer = RendererSearchPlayer;
		base.ui.com_ApplyFriend.list_Friends.SetVirtual();
		base.ui.com_ApplyFriend.list_Friends.itemRenderer = RendererApplyPlayer;
		base.ui.com_BlackList.list_Friends.SetVirtual();
		base.ui.com_BlackList.list_Friends.itemRenderer = RendererBlack;
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.com_Friends.com_State.items = _onlineStateItems;
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo != null)
		{
			int onlineStatus = playerInfo.OnlineStatus;
			base.ui.com_Friends.com_State.selectedIndex = onlineStatus;
			base.ui.com_Friends.com_State.title = _onlineStateItems.GetSafeByIndex(onlineStatus);
		}
		base.ui.com_Friends.Txt_State.text = 2003.GetLocal(UIStringType.Friend);
		base.ui.type.onChanged.Call();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.type.onChanged.Add(SwitchTab);
		base.ui.com_AddFriend.type.onChanged.Add(ChangeAddType);
		base.ui.com_AddFriend.btn_SearchPlayer.onClick.Add(SearchPlayer);
		base.ui.com_ApplyFriend.btn_AllRefuse.onClick.Add(AllRefuse);
		base.ui.com_Friends.com_State.onChanged.Add(ChangeState);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.type.onChanged.Remove(SwitchTab);
		base.ui.com_AddFriend.type.onChanged.Remove(ChangeAddType);
		base.ui.com_AddFriend.btn_SearchPlayer.onClick.Remove(SearchPlayer);
		base.ui.com_ApplyFriend.btn_AllRefuse.onClick.Remove(AllRefuse);
		base.ui.com_Friends.com_State.onChanged.Remove(ChangeState);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshFriends.AddListener(OnRefreshFriendsList);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshLastFriends.AddListener(OnRefreshLastFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshApplyFriends.AddListener(OnRefreshApplyFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshBlackFriends.AddListener(OnRefreshBlackFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.AddListener(UpdateApplyTab);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshFriends.RemoveListener(OnRefreshFriendsList);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshLastFriends.RemoveListener(OnRefreshLastFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshApplyFriends.RemoveListener(OnRefreshApplyFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.signal.refreshBlackFriends.RemoveListener(OnRefreshBlackFriends);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.RemoveListener(UpdateApplyTab);
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void UpdateApplyTab(bool _status)
	{
		base.ui.btn_TabApply.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.Value ? 1 : 0);
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void SwitchTab(EventContext context)
	{
		base.ui.type.onChanged.Retain();
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		if (base.ui.type.selectedIndex == 0)
		{
			base.ui.com_Friends.list_Friends.scrollPane.percY = 0f;
			FriendList().Forget();
		}
		else if (base.ui.type.selectedIndex == 1)
		{
			base.ui.com_AddFriend.list_LastPlayer.scrollPane.percY = 0f;
			AddList();
		}
		else if (base.ui.type.selectedIndex == 2)
		{
			base.ui.com_ApplyFriend.list_Friends.scrollPane.percY = 0f;
			ApplyList().Forget();
		}
		else if (base.ui.type.selectedIndex == 3)
		{
			base.ui.com_BlackList.list_Friends.scrollPane.percY = 0f;
			BlackList().Forget();
		}
		base.ui.type.onChanged.Release();
	}

	private void ChangeAddType()
	{
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		base.ui.com_AddFriend.type.onChanged.Retain();
		if (base.ui.com_AddFriend.type.selectedIndex == 0)
		{
			LastPlayerList().Forget();
		}
		else if (base.ui.com_AddFriend.type.selectedIndex == 1)
		{
			OpenSearch();
		}
		base.ui.com_AddFriend.type.onChanged.Release();
	}

	private void AllRefuse()
	{
		base.ui.com_ApplyFriend.btn_AllRefuse.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyOpC2S(0L, 2, allRefuse: true, delegate
		{
			base.ui.com_ApplyFriend.btn_AllRefuse.onClick.Release();
		});
	}

	private void ChangeState()
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (base.ui.com_Friends.com_State.selectedIndex != playerInfo.OnlineStatus)
		{
			base.ui.com_Friends.com_State.onChanged.Retain();
			int selectedIndex = base.ui.com_Friends.com_State.selectedIndex;
			base.ui.com_Friends.com_State.title = _onlineStateItems[Mathf.Clamp(selectedIndex, 0, _onlineStateItems.Length - 1)];
			SimpleSingletonProvider<GameLogicManager>.inst.friend.SetOnlineStatusC2S(selectedIndex).OnFinished.AddOnce(delegate(RPCAsyncResult result)
			{
				base.ui.com_Friends.com_State.onChanged.Release();
				_ = result.errId;
			});
		}
	}

	private async UniTaskVoid FriendList()
	{
		if (Time.time - _currentFriendListRefreshTime > 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowFriend(_delayStatus: true, 1001);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendListC2S().OnFinished.AddOnce(delegate(RPCAsyncResult result)
			{
				_currentFriendListRefreshTime = Time.time;
				_ = result.errId;
			});
		}
		else
		{
			OnRefreshFriendsList();
		}
	}

	private void OnRefreshFriendsList()
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (base.ui.type.selectedIndex == 0)
		{
			friendsList = SimpleSingletonProvider<GameLogicManager>.inst.friend.FriendList;
			int friendCount = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendCount();
			base.ui.com_Friends.txt_FriendsNum.SetVar("num", $"{friendCount} / {StaticGlobalData.FRIEND_NUMBLIMIT}").FlushVars();
			base.ui.com_Friends.list_Friends.numItems = friendsList.Count;
		}
	}

	private void RendererFriend(int index, GObject item)
	{
		UIFriend_Com_FriendItem _item = item as UIFriend_Com_FriendItem;
		if (_item == null || friendsList == null || friendsList.Count <= index)
		{
			return;
		}
		FriendData _friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(friendsList[index]);
		_item.visible = _friendData != null;
		if (_friendData == null)
		{
			return;
		}
		_item.grayed = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(friendsList[index]);
		RendererLabel((UICom_PlayerLabel)_item.btn_PlayerLabel.com_PlayerLabel, _friendData);
		_item.btn_PlayerLabel.onClick.Set((EventCallback0)delegate
		{
			FriendData curFriendData = GetCurFriendData(_friendData.playerId);
			_item.btn_PlayerLabel.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(_friendData.playerId, curFriendData.Nick, curFriendData.LV, curFriendData.HeadURL, curFriendData.Label, delegate
			{
				_item.btn_PlayerLabel.onClick.Release();
			});
		});
		if (!_friendData.IsOnline)
		{
			_item.btn_PlayerLabel.txt_OfflineTime.text = _friendData.offlineTimeDesc;
			_item.btn_PlayerLabel.txt_OfflineTime.visible = false;
			_item.btn_PlayerLabel.friendStatus.selectedIndex = 1;
			return;
		}
		_item.btn_PlayerLabel.friendStatus.selectedIndex = ((_friendData.roomInfo != null) ? 2 : 0);
		if (_friendData.roomInfo != null)
		{
			_item.btn_PlayerLabel.btn_FriendStatus.onClick.Set((EventCallback0)delegate
			{
				_item.btn_PlayerLabel.onClick.Retain();
				_item.btn_PlayerLabel.btn_FriendStatus.onClick.Retain();
				JoinPanel(_friendData.roomInfo, delegate(int errId)
				{
					if (errId != 0 && _friendData != null)
					{
						SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestGetPlayerSimpleC2S(_friendData.playerId, null);
					}
					_item.btn_PlayerLabel.btn_FriendStatus.onClick.Release();
					_item.btn_PlayerLabel.onClick.Release();
				});
			});
		}
		else
		{
			_item.btn_PlayerLabel.friendStatus.selectedIndex = 0;
		}
	}

	private void RendererLabel(UICom_PlayerLabel com_Label, FriendData friendData)
	{
		CommonUIManager.RendererLabelInfo(com_Label, SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(friendData.playerId, friendData.Nick, showNote: true), friendData.LV);
		(string, bool) label = friendData.Label;
		CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_Label, label.Item1, label.Item2);
		CommonUIManager.RendererHeadShot(com_Label, friendData.HeadURL, isVideo: false);
	}

	private void JoinPanel(FriendRoomInfo roomInfo, Action<int> OnComplete)
	{
		if (roomInfo == null || !SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
		{
			return;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			OnComplete(0);
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestJoinRoomC2S(roomInfo.roomId, "", roomInfo.roomServerId).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 11091)
			{
				SimpleSingletonProvider<UIManager>.inst.input.OpenPwd(delegate(string pwd)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestJoinRoomC2S(roomInfo.roomId, pwd, roomInfo.roomServerId).OnFinished.AddListener(delegate(RPCAsyncResult result_Pwd)
					{
						OnComplete(result_Pwd.errId);
					});
				}, delegate
				{
					OnComplete(0);
				});
			}
			else
			{
				OnComplete(result.errId);
			}
		});
	}

	private void AddList()
	{
		base.ui.com_AddFriend.type.onChanged.Call();
	}

	private async UniTaskVoid LastPlayerList()
	{
		base.ui.com_AddFriend.txt_Times.SetVar("num", $"{StaticGlobalData.FRIEND_APPLICATION_DAILYLIMIT}").FlushVars();
		if (Time.time - _currentAddListRefreshTime > 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowFriend(_delayStatus: true, 1001);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestNearFightPlayerC2S().OnFinished.AddOnce(delegate(RPCAsyncResult result)
			{
				_currentAddListRefreshTime = Time.time;
				_ = result.errId;
			});
		}
		else
		{
			OnRefreshLastFriends();
		}
	}

	private void OnRefreshLastFriends()
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (base.ui.type.selectedIndex == 1 && base.ui.com_AddFriend.type.selectedIndex == 0)
		{
			List<long> lastPlayFriends = SimpleSingletonProvider<GameLogicManager>.inst.friend.LastPlayFriends;
			base.ui.com_AddFriend.list_LastPlayer.numItems = lastPlayFriends.Count;
		}
	}

	private void OpenSearch()
	{
		base.ui.com_AddFriend.txtField_Search.text = "";
		base.ui.com_AddFriend.showResult.selectedIndex = 0;
		base.ui.com_AddFriend.list_SearchPlayer.numItems = 0;
		base.ui.com_AddFriend.btn_SearchPlayer.onClick.Release();
	}

	private async void SearchPlayer()
	{
		base.ui.com_AddFriend.list_SearchPlayer.numItems = 0;
		base.ui.com_AddFriend.showResult.selectedIndex = 0;
		if (string.IsNullOrEmpty(base.ui.com_AddFriend.txtField_Search.text))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Friend));
			return;
		}
		long UID = long.Parse(base.ui.com_AddFriend.txtField_Search.text);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(UID))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1010.GetLocal(UIStringType.Friend));
		}
		else if (Time.time - _currentSearchRefreshTime > 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowFriend(_delayStatus: true, 1001);
			}
			base.ui.com_AddFriend.btn_SearchPlayer.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestSearchPlayerC2S(UID, delegate(RPCAsyncResult result)
			{
				_currentSearchRefreshTime = Time.time;
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
				base.ui.com_AddFriend.btn_SearchPlayer.onClick.Release();
				if (result.errId == 0)
				{
					searchFriends = SimpleSingletonProvider<GameLogicManager>.inst.friend.searchFriends;
					base.ui.com_AddFriend.showResult.selectedIndex = ((searchFriends.Count == 0) ? 1 : 0);
					base.ui.com_AddFriend.list_SearchPlayer.numItems = searchFriends.Count;
				}
			});
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1002.GetLocal(UIStringType.Friend));
		}
	}

	private void RendererLastPlayer(int index, GObject item)
	{
		List<long> lastPlayFriends = SimpleSingletonProvider<GameLogicManager>.inst.friend.LastPlayFriends;
		UIFriend_Com_ApplyItem _item = item as UIFriend_Com_ApplyItem;
		if (_item == null || lastPlayFriends == null || lastPlayFriends.Count <= index)
		{
			return;
		}
		FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(lastPlayFriends[index]);
		_item.visible = friendData != null;
		if (friendData == null)
		{
			return;
		}
		DateTime time = (friendData._Time * 1000).StampMillisecondsToDateTime();
		_item.txt_ApplyTime.text = time.ToUIDateTime_YMDHM();
		RendererLabel((UICom_PlayerLabel)_item.com_PlayerLabel, friendData);
		_item.onClick.Set((EventCallback0)delegate
		{
			FriendData curFriendData = GetCurFriendData(friendData.playerId);
			_item.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(friendData.playerId, curFriendData.Nick, curFriendData.LV, curFriendData.HeadURL, curFriendData.Label, delegate
			{
				_item.onClick.Release();
			});
		});
	}

	private void RendererSearchPlayer(int index, GObject item)
	{
		UIFriend_Com_ApplyItem _item = item as UIFriend_Com_ApplyItem;
		if (_item == null || searchFriends == null || searchFriends.Count <= index)
		{
			return;
		}
		_item.txt_ApplyTime.text = "";
		FriendData searchFriendData = searchFriends[index];
		RendererLabel((UICom_PlayerLabel)_item.com_PlayerLabel, searchFriendData);
		_item.onClick.Set((EventCallback0)delegate
		{
			FriendData curFriendData = GetCurFriendData(searchFriendData.playerId);
			_item.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(searchFriendData.playerId, curFriendData.Nick, curFriendData.LV, curFriendData.HeadURL, curFriendData.Label, delegate
			{
				_item.onClick.Release();
			});
		});
	}

	public void TryRefreshApplyList()
	{
		if (base.ui.type.selectedIndex == 2)
		{
			ApplyList().Forget();
		}
	}

	private async UniTaskVoid ApplyList()
	{
		base.ui.com_ApplyFriend.txt_ShowTime.SetVar("day", $"{StaticGlobalData.FRIEND_APPLICATION_AVAILABLE_DAYS}").FlushVars();
		if (Time.time - _currentApplyListRefreshTime > 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowFriend(_delayStatus: true, 1001);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyListC2S().OnFinished.AddOnce(delegate
			{
				_currentApplyListRefreshTime = Time.time;
			});
		}
		else
		{
			OnRefreshApplyFriends();
		}
	}

	private void OnRefreshApplyFriends()
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (base.ui.type.selectedIndex == 2)
		{
			applyFriendList = SimpleSingletonProvider<GameLogicManager>.inst.friend.ShowApplyList;
			base.ui.com_ApplyFriend.list_Friends.numItems = applyFriendList.Count;
			base.ui.com_ApplyFriend.btn_AllRefuse.visible = applyFriendList.Count > 0;
		}
	}

	private void RendererApplyPlayer(int index, GObject item)
	{
		UIFriend_Com_ApplyItem _item = item as UIFriend_Com_ApplyItem;
		if (_item == null || applyFriendList == null || applyFriendList.Count <= index)
		{
			return;
		}
		FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetApplyInfo(applyFriendList[index]);
		_item.visible = friendData != null;
		if (friendData == null)
		{
			return;
		}
		DateTime time = (friendData._Time * 1000).StampMillisecondsToDateTime();
		_item.txt_ApplyTime.text = time.ToUIDateTime_YMDHM();
		RendererLabel((UICom_PlayerLabel)_item.com_PlayerLabel, friendData);
		_item.onClick.Set((EventCallback0)delegate
		{
			FriendData curFriendData = GetCurFriendData(friendData.playerId);
			_item.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(friendData.playerId, curFriendData.Nick, curFriendData.LV, curFriendData.HeadURL, curFriendData.Label, delegate
			{
				_item.onClick.Release();
			});
		});
	}

	private async UniTaskVoid BlackList()
	{
		if (Time.time - _currentBlackListRefreshTime > 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowFriend(_delayStatus: true, 1001);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendBlacksListC2S().OnFinished.AddOnce(delegate
			{
				_currentBlackListRefreshTime = Time.time;
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
			});
		}
		else
		{
			OnRefreshBlackFriends();
		}
	}

	private void OnRefreshBlackFriends()
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (base.ui.type.selectedIndex == 3)
		{
			blackList = SimpleSingletonProvider<GameLogicManager>.inst.friend.ShowBlacksList;
			int blackCount = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetBlackCount();
			base.ui.com_BlackList.txt_blackNum.SetVar("num", $"{blackCount}/{StaticGlobalData.FRIEND_BLACKLIST_NUMBLIMIT}").FlushVars();
			base.ui.com_BlackList.list_Friends.numItems = blackList.Count;
		}
	}

	private void RendererBlack(int index, GObject item)
	{
		UIFriend_Com_ApplyItem _item = item as UIFriend_Com_ApplyItem;
		if (_item == null || blackList == null || blackList.Count <= index)
		{
			return;
		}
		FriendData _friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(blackList[index]);
		_item.visible = _friendData != null;
		if (_friendData == null)
		{
			return;
		}
		RendererLabel((UICom_PlayerLabel)_item.com_PlayerLabel, _friendData);
		_item.onClick.Set((EventCallback0)delegate
		{
			FriendData curFriendData = GetCurFriendData(_friendData.playerId);
			_item.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(_friendData.playerId, curFriendData.Nick, curFriendData.LV, curFriendData.HeadURL, curFriendData.Label, delegate
			{
				_item.onClick.Release();
			});
		});
	}

	private FriendData GetCurFriendData(long playerId)
	{
		if (base.ui.type.selectedIndex == 0 || base.ui.type.selectedIndex == 3)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(playerId);
		}
		if (base.ui.type.selectedIndex == 1)
		{
			if (base.ui.com_AddFriend.type.selectedIndex != 0)
			{
				return searchFriends.Find((FriendData x) => x.playerId == playerId);
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.friend.LastPlayFriends.Contains(playerId))
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(playerId);
			}
		}
		if (base.ui.type.selectedIndex == 2)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetApplyData(playerId);
		}
		return null;
	}
}
