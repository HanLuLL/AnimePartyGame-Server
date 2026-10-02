using System;
using System.Collections.Generic;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class InviteWindow : BaseWindow
{
	private float _currentFriendInviteListRequestTime;

	private float _currentLastInviteListRequestTime;

	private int _selectTabIndex;

	private readonly List<long> _inviteFriendIds = new List<long>();

	private List<FriendData> _showFriendsData = new List<FriendData>(64);

	public InviteWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIInviteWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.mohu.onClick.Add(HideWin);
			uIInviteWindow.btn_Invite.onClick.Add(InviteFriends);
			uIInviteWindow.tab.onChanged.Add(OnChangeFriendType);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.mohu.onClick.Remove(HideWin);
			uIInviteWindow.btn_Invite.onClick.Remove(InviteFriends);
			uIInviteWindow.tab.onChanged.Remove(OnChangeFriendType);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uIInviteWindow.type.selectedIndex = 0;
		}
	}

	private void OnChangeFriendType()
	{
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.tab.onChanged.Retain();
			_inviteFriendIds.Clear();
			PullFriendList(uIInviteWindow.tab.selectedIndex);
			uIInviteWindow.tab.onChanged.Release();
		}
	}

	private void HideWin()
	{
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.mohu.onClick.Retain();
			uIInviteWindow.type.selectedIndex = 0;
			uIInviteWindow.list_InviteInfo.numItems = 0;
			uIInviteWindow.list_PlayerLabel.numItems = 0;
			Hide();
			uIInviteWindow.mohu.onClick.Release();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			HideWin();
		}
	}

	private void TryShowFriendList(List<long> friends, int tabIndex)
	{
		GComponent gComponent = base.contentPane;
		UIInviteWindow win = gComponent as UIInviteWindow;
		if (win == null)
		{
			return;
		}
		win.type.selectedIndex = 1;
		win.list_PlayerLabel.itemRenderer = delegate(int index, GObject item)
		{
			UIInviteFriend_Button_PlayerItem btn_item = item as UIInviteFriend_Button_PlayerItem;
			if (btn_item != null)
			{
				FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(friends[index]);
				bool flag = friendData.IsOnline && !SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(friendData.playerId);
				btn_item.visible = flag;
				if (flag)
				{
					RendererLabel((UICom_PlayerLabel)btn_item.com_PlayerLabel, friendData);
					if (friendData.IsBusy || friendData.roomInfo != null)
					{
						btn_item.status.selectedIndex = 2;
						btn_item.touchable = false;
					}
					else
					{
						btn_item.status.selectedIndex = (_inviteFriendIds.Contains(friendData.playerId) ? 1 : 0);
						btn_item.touchable = true;
					}
					btn_item.onClick.Set((EventCallback0)delegate
					{
						btn_item.onClick.Retain();
						if (_inviteFriendIds.Contains(friendData.playerId))
						{
							btn_item.status.selectedIndex = 0;
							_inviteFriendIds.Remove(friendData.playerId);
						}
						else
						{
							btn_item.status.selectedIndex = 1;
							_inviteFriendIds.Add(friendData.playerId);
						}
						win.btn_Invite.grayed = _inviteFriendIds.Count == 0;
						btn_item.onClick.Release();
					});
				}
			}
		};
		win.list_PlayerLabel.numItems = friends.Count;
		win.btn_Invite.grayed = _inviteFriendIds.Count == 0;
		long num = friends.Find((long friendId) => SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(friendId).IsOnline && !SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(friendId));
		if (tabIndex == 0)
		{
			win.txt_TypeExplain.text = 1020013.GetLocal(UIStringType.GUI);
			win.txt_NotFriends.visible = num == 0;
			win.txt_NotLastFriends.visible = false;
		}
		else
		{
			win.txt_TypeExplain.text = 1020001.GetLocal(UIStringType.GUI);
			win.txt_NotFriends.visible = false;
			win.txt_NotLastFriends.visible = num == 0;
		}
	}

	private void InviteFriends(EventContext context)
	{
		if (_inviteFriendIds.Count == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1051);
			return;
		}
		GComponent gComponent = base.contentPane;
		UIInviteWindow win = gComponent as UIInviteWindow;
		if (win == null)
		{
			return;
		}
		win.btn_Invite.onClick.Retain();
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendInviteC2S(_inviteFriendIds, SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Pwd).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_Invite.onClick.Release();
				HideWin();
			});
		}
		else if (SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.InTeam)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestMatchTeamInviteC2S(_inviteFriendIds).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_Invite.onClick.Release();
				HideWin();
			});
		}
	}

	private void RendererLabel(UICom_PlayerLabel com_Label, FriendData friendData)
	{
		string displayNick = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(friendData.playerId, friendData.Nick, showNote: true);
		CommonUIManager.RendererLabelInfo(com_Label, displayNick, friendData.LV);
		(string, bool) label = friendData.Label;
		CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, com_Label, label.Item1, label.Item2);
		CommonUIManager.RendererHeadShot(com_Label, friendData.HeadURL, isVideo: false);
	}

	public async UniTask OpenFriendList()
	{
		_currentFriendInviteListRequestTime = -StaticGlobalData.FRIEND_INVITEPARTY_BUTTON_CD;
		_currentLastInviteListRequestTime = -StaticGlobalData.FRIEND_INVITEPARTY_BUTTON_CD;
		await TryShow();
		_selectTabIndex = 0;
		_inviteFriendIds.Clear();
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.btn_Friend.title = 1020011.GetLocal(UIStringType.GUI);
			uIInviteWindow.btn_LastTeam.title = 1020012.GetLocal(UIStringType.GUI);
			uIInviteWindow.txt_NotFriends.text = 1020002.GetLocal(UIStringType.GUI);
			uIInviteWindow.txt_NotLastFriends.text = 1020003.GetLocal(UIStringType.GUI);
			uIInviteWindow.tab.selectedIndex = _selectTabIndex;
			uIInviteWindow.tab.onChanged.Call();
		}
	}

	private void PullFriendList(int tabIndex)
	{
		if (!(base.contentPane is UIInviteWindow uIInviteWindow))
		{
			return;
		}
		if (tabIndex != _selectTabIndex)
		{
			uIInviteWindow.list_PlayerLabel.numItems = 0;
		}
		if (tabIndex == 0)
		{
			if (Time.time - _currentFriendInviteListRequestTime > 5f)
			{
				_currentFriendInviteListRequestTime = Time.time;
				if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShow().Forget();
				}
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendListC2S().OnFinished.AddOnce(delegate
				{
					SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
					TryShowFriendList(SimpleSingletonProvider<GameLogicManager>.inst.friend.ShowFriendList, tabIndex);
				});
			}
			else
			{
				TryShowFriendList(SimpleSingletonProvider<GameLogicManager>.inst.friend.ShowFriendList, tabIndex);
			}
			return;
		}
		if (tabIndex == 1)
		{
			if (Time.time - _currentLastInviteListRequestTime > 5f)
			{
				_currentLastInviteListRequestTime = Time.time;
				if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShow().Forget();
				}
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestNearFightPlayerC2S().OnFinished.AddOnce(delegate
				{
					SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
					TryShowFriendList(SimpleSingletonProvider<GameLogicManager>.inst.friend.LastPlayFriends, tabIndex);
				});
				return;
			}
			TryShowFriendList(SimpleSingletonProvider<GameLogicManager>.inst.friend.LastPlayFriends, tabIndex);
		}
		_selectTabIndex = tabIndex;
	}

	private List<FriendData> GetShowFriendsData()
	{
		_showFriendsData.Clear();
		foreach (long showFriend in SimpleSingletonProvider<GameLogicManager>.inst.friend.ShowFriendList)
		{
			FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(showFriend);
			if (friendData != null && friendData.IsOnline)
			{
				_showFriendsData.Add(friendData);
			}
		}
		return _showFriendsData;
	}

	public async UniTask TryShowInvite(AstralInviteType type)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			return;
		}
		await TryShow();
		if (!(base.contentPane is UIInviteWindow uIInviteWindow))
		{
			return;
		}
		switch (type)
		{
		case AstralInviteType.RoomInvite:
			uIInviteWindow.btn_ClearInfo.onClick.Set(ClearRoomInvite);
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendInviteListC2S().OnFinished.AddOnce(delegate
			{
				TryShowInviteList();
			});
			break;
		case AstralInviteType.TeamInvite:
			uIInviteWindow.btn_ClearInfo.onClick.Set(ClearMatchTeamInvite);
			TryShowMatchTeamInvite();
			break;
		}
		uIInviteWindow.txt_inviteText.text = 1020022.GetLocal(UIStringType.GUI);
		uIInviteWindow.type.selectedIndex = 2;
	}

	private void TryShowMatchTeamInvite()
	{
		if (!(base.contentPane is UIInviteWindow uIInviteWindow))
		{
			return;
		}
		List<MatchTeamInviteInfo> _inviteData = SimpleSingletonProvider<GameLogicManager>.inst.match.GetMatchTeamInvite();
		uIInviteWindow.list_InviteInfo.itemRenderer = delegate(int index, GObject item)
		{
			UIInviteFriend_Com_InviteInfo com_Invite = item as UIInviteFriend_Com_InviteInfo;
			if (com_Invite != null)
			{
				MatchTeamInviteInfo inviteInfo = _inviteData[index];
				RendererLabel((UICom_PlayerLabel)com_Invite.com_PlayerLabel, inviteInfo);
				bool flag = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(inviteInfo.PlayerId);
				bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(inviteInfo.PlayerId);
				if (com_Invite.com_FriendStatus is UIRoomPlayer_Com_FriendStatus uIRoomPlayer_Com_FriendStatus)
				{
					uIRoomPlayer_Com_FriendStatus.type.selectedIndex = (flag2 ? 1 : (flag ? 2 : 0));
				}
				DateTime time = inviteInfo.Time.StampMillisecondsToDateTime();
				com_Invite.txt_Time.text = time.ToUIDateTime_YMDHM();
				com_Invite.btn_Join.onClick.Set((EventCallback0)delegate
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
					{
						com_Invite.btn_Join.onClick.Retain();
						SimpleSingletonProvider<GameLogicManager>.inst.match.RequestJoinMatchTeamC2S(inviteInfo.PlayerId, inviteInfo.TeamId).OnFinished.AddOnce(delegate(RPCAsyncResult result)
						{
							com_Invite.btn_Join.onClick.Release();
							if (result.errId != 0)
							{
								TryShowMatchTeamInvite();
							}
							else
							{
								HideWin();
							}
						});
					}
				});
			}
		};
		uIInviteWindow.list_InviteInfo.numItems = _inviteData.Count;
	}

	private void RendererLabel(UICom_PlayerLabel com_Label, MatchTeamInviteInfo teamLeader)
	{
		string displayNick = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(teamLeader.PlayerId, teamLeader.Nick, showNote: true);
		CommonUIManager.RendererLabelInfo(com_Label, displayNick, teamLeader.LV);
		(string, bool) label = teamLeader.Label;
		CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, com_Label, label.Item1, label.Item2);
		CommonUIManager.RendererHeadShot(com_Label, teamLeader.HeadURL, isVideo: false);
	}

	private void ClearMatchTeamInvite()
	{
		if (base.contentPane is UIInviteWindow uIInviteWindow)
		{
			uIInviteWindow.btn_ClearInfo.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.match.ClearMatchTeamInvite();
			uIInviteWindow.list_InviteInfo.numItems = 0;
			uIInviteWindow.btn_ClearInfo.onClick.Release();
		}
	}

	private void TryShowInviteList()
	{
		if (!(base.contentPane is UIInviteWindow uIInviteWindow))
		{
			return;
		}
		List<long> _inviteData = SimpleSingletonProvider<GameLogicManager>.inst.friend.InviteFriends;
		uIInviteWindow.list_InviteInfo.itemRenderer = delegate(int index, GObject item)
		{
			UIInviteFriend_Com_InviteInfo com_Invite = item as UIInviteFriend_Com_InviteInfo;
			if (com_Invite != null)
			{
				FriendData inviteInfo = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(_inviteData[index]);
				RendererLabel((UICom_PlayerLabel)com_Invite.com_PlayerLabel, inviteInfo);
				DateTime time = (inviteInfo._Time * 1000).StampMillisecondsToDateTime();
				com_Invite.txt_Time.text = time.ToUIDateTime_YMDHM();
				bool flag = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(inviteInfo.playerId);
				bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(inviteInfo.playerId);
				if (com_Invite.com_FriendStatus is UIRoomPlayer_Com_FriendStatus uIRoomPlayer_Com_FriendStatus)
				{
					uIRoomPlayer_Com_FriendStatus.type.selectedIndex = (flag2 ? 1 : (flag ? 2 : 0));
				}
				com_Invite.btn_Join.onClick.Set((EventCallback0)delegate
				{
					FriendRoomInfo roomInfo = inviteInfo.roomInfo;
					if (roomInfo != null && SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense() && SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
					{
						com_Invite.btn_Join.onClick.Retain();
						SimpleSingletonProvider<GameLogicManager>.inst.room.RequestJoinRoomC2S(roomInfo.roomId, roomInfo.invitePwd, roomInfo.roomServerId).OnFinished.AddOnce(delegate(RPCAsyncResult result)
						{
							com_Invite.btn_Join.onClick.Release();
							if (result.errId != 0)
							{
								SimpleSingletonProvider<GameLogicManager>.inst.friend.InviteFriends.Remove(inviteInfo.playerId);
								TryShowInviteList();
							}
							else
							{
								HideWin();
							}
						});
					}
				});
			}
		};
		uIInviteWindow.list_InviteInfo.numItems = _inviteData.Count;
	}

	private void ClearRoomInvite()
	{
		GComponent gComponent = base.contentPane;
		UIInviteWindow win = gComponent as UIInviteWindow;
		if (win != null)
		{
			win.btn_ClearInfo.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendInviteCleanC2S().OnFinished.AddOnce(delegate
			{
				win.btn_ClearInfo.onClick.Release();
				win.list_InviteInfo.numItems = 0;
			});
		}
	}
}
