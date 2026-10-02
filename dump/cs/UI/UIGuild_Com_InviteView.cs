using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuild_Com_InviteView : GComponent
{
	public const int SEARCH_TYPE = 0;

	public const int FRIEND_TYPE = 1;

	public const int RECENT_TYPE = 2;

	private readonly List<long> _candidateIds = new List<long>();

	private bool _initialized;

	private bool _isShowing;

	private bool _isSearching;

	private bool _isInviting;

	private int _sessionVersion;

	private int _searchVersion;

	private long _selectedPlayerId;

	private long _searchPlayerId;

	private long _searchResultId;

	public System.Action OnRequestClose;

	public Controller type;

	public GGraph mohu;

	public GButton btn_Search;

	public GButton btn_Friend;

	public GButton btn_LastTeam;

	public GTextField txt_NotFriends;

	public GTextField txt_NotLastFriends;

	public GList list_PlayerLabel;

	public GTextField txt_TypeExplain;

	public GButton btn_Invite;

	public GTextInput uid_input;

	public UIGuild_Common_Button btn_search;

	public UIGuild_Button_PlayerItem search_player;

	public const string URL = "ui://w5bj58pzhtd12i";

	public void Init()
	{
		if (!_initialized)
		{
			if (list_PlayerLabel != null)
			{
				list_PlayerLabel.itemRenderer = RenderCandidate;
			}
			_initialized = true;
		}
	}

	public void OnShow()
	{
		_sessionVersion++;
		_searchVersion++;
		_isShowing = true;
		_isSearching = false;
		_isInviting = false;
		_selectedPlayerId = 0L;
		_searchPlayerId = 0L;
		_searchResultId = 0L;
		if (search_player != null)
		{
			search_player.visible = false;
		}
		if (uid_input != null)
		{
			uid_input.text = string.Empty;
		}
		if (type != null)
		{
			type.selectedIndex = 0;
		}
		RefreshCurrentType();
	}

	public void OnHide()
	{
		_sessionVersion++;
		_searchVersion++;
		_isShowing = false;
		EndSearchBusy();
		EndInviteBusy();
		_candidateIds.Clear();
		_selectedPlayerId = 0L;
		_searchPlayerId = 0L;
		_searchResultId = 0L;
		if (search_player != null)
		{
			search_player.visible = false;
		}
	}

	public void AddEvent()
	{
		btn_Search?.onClick.Add(OnClickSearchType);
		btn_Friend?.onClick.Add(OnClickFriendType);
		btn_LastTeam?.onClick.Add(OnClickRecentType);
		mohu?.onClick.Add(OnClickBackground);
		type?.onChanged.Add(OnTypeChanged);
		btn_search?.onClick.Add(OnClickSearchPlayer);
		btn_Invite?.onClick.Add(OnClickInvite);
	}

	public void RemoveEvent()
	{
		btn_Search?.onClick.Remove(OnClickSearchType);
		btn_Friend?.onClick.Remove(OnClickFriendType);
		btn_LastTeam?.onClick.Remove(OnClickRecentType);
		mohu?.onClick.Remove(OnClickBackground);
		type?.onChanged.Remove(OnTypeChanged);
		btn_search?.onClick.Remove(OnClickSearchPlayer);
		btn_Invite?.onClick.Remove(OnClickInvite);
	}

	public void AddListener()
	{
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		friend?.signal.refreshFriends.AddListener(OnFriendsChanged);
		friend?.signal.refreshLastFriends.AddListener(OnRecentPlayersChanged);
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildMembersChanged.AddListener(OnGuildStateChanged);
		guild?.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
	}

	public void RemoveListener()
	{
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		friend?.signal.refreshFriends.RemoveListener(OnFriendsChanged);
		friend?.signal.refreshLastFriends.RemoveListener(OnRecentPlayersChanged);
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.guildMembersChanged.RemoveListener(OnGuildStateChanged);
		guild?.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
	}

	public void ClearData()
	{
		OnHide();
		if (uid_input != null)
		{
			uid_input.text = string.Empty;
		}
		if (search_player != null)
		{
			search_player.visible = false;
		}
		OnRequestClose = null;
	}

	private void OnClickBackground()
	{
		if (_isShowing)
		{
			OnRequestClose?.Invoke();
		}
	}

	private void OnClickSearchType()
	{
		SetType(0);
	}

	private void OnClickFriendType()
	{
		SetType(1);
	}

	private void OnClickRecentType()
	{
		SetType(2);
	}

	private void SetType(int typeIndex)
	{
		if (type != null && type.selectedIndex != typeIndex)
		{
			type.selectedIndex = typeIndex;
		}
	}

	private void OnTypeChanged()
	{
		Controller controller = type;
		if (controller == null || controller.selectedIndex != 0)
		{
			EndSearchBusy();
			_searchVersion++;
			_searchPlayerId = 0L;
		}
		_selectedPlayerId = 0L;
		_searchResultId = 0L;
		if (search_player != null)
		{
			search_player.visible = false;
		}
		RefreshCurrentType();
	}

	private void RefreshCurrentType(bool requestSource = true)
	{
		if (!_isShowing || type == null)
		{
			return;
		}
		switch (type.selectedIndex)
		{
		case 0:
			_candidateIds.Clear();
			if (list_PlayerLabel != null)
			{
				list_PlayerLabel.numItems = 0;
			}
			break;
		case 1:
			if (requestSource)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.friend?.RequestFriendListC2S();
			}
			RebuildCandidates(SimpleSingletonProvider<GameLogicManager>.inst.friend?.ShowFriendList, onlineOnly: true);
			break;
		case 2:
			if (requestSource)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.friend?.RequestNearFightPlayerC2S();
			}
			RebuildCandidates(SimpleSingletonProvider<GameLogicManager>.inst.friend?.LastPlayFriends, onlineOnly: true);
			break;
		}
		RefreshInviteButton();
	}

	private void RebuildCandidates(IList<long> source, bool onlineOnly)
	{
		_candidateIds.Clear();
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (source != null && friend != null)
		{
			for (int i = 0; i < source.Count; i++)
			{
				long num = source[i];
				friend.TryGetFriendData(num, out var friendData);
				if (CanUseCandidate(num, friendData, onlineOnly))
				{
					_candidateIds.Add(num);
				}
			}
		}
		if (!_candidateIds.Contains(_selectedPlayerId))
		{
			_selectedPlayerId = 0L;
		}
		if (list_PlayerLabel != null)
		{
			list_PlayerLabel.numItems = _candidateIds.Count;
		}
		if (txt_NotFriends != null)
		{
			GTextField gTextField = txt_NotFriends;
			Controller controller = type;
			gTextField.visible = controller != null && controller.selectedIndex == 1 && _candidateIds.Count == 0;
		}
		if (txt_NotLastFriends != null)
		{
			GTextField gTextField2 = txt_NotLastFriends;
			Controller controller2 = type;
			gTextField2.visible = controller2 != null && controller2.selectedIndex == 2 && _candidateIds.Count == 0;
		}
		RefreshInviteButton();
	}

	private void RenderCandidate(int index, GObject item)
	{
		if (item is UIGuild_Button_PlayerItem uIGuild_Button_PlayerItem && index >= 0 && index < _candidateIds.Count)
		{
			long num = _candidateIds[index];
			FriendData friendData = null;
			SimpleSingletonProvider<GameLogicManager>.inst.friend?.TryGetFriendData(num, out friendData);
			uIGuild_Button_PlayerItem.Bind(friendData, num == _selectedPlayerId, OnCandidateSelected);
		}
	}

	private void OnCandidateSelected(long playerId)
	{
		_selectedPlayerId = ((_selectedPlayerId == playerId) ? 0 : playerId);
		Controller controller = type;
		if (controller != null && controller.selectedIndex == 0)
		{
			BindSearchResult();
		}
		else if (list_PlayerLabel != null)
		{
			list_PlayerLabel.numItems = _candidateIds.Count;
		}
		RefreshInviteButton();
	}

	private void OnClickSearchPlayer()
	{
		if (_isSearching || !long.TryParse(uid_input?.text?.Trim(), out var playerId) || playerId <= 0)
		{
			ShowTip(3161);
			return;
		}
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (friend != null)
		{
			int version = _sessionVersion;
			int searchVersion = ++_searchVersion;
			_searchPlayerId = playerId;
			_searchResultId = 0L;
			_selectedPlayerId = 0L;
			if (search_player != null)
			{
				search_player.visible = false;
			}
			_isSearching = true;
			btn_search?.onClick.Retain();
			friend.RequestSearchPlayerC2S(playerId, delegate(RPCAsyncResult result)
			{
				OnSearchFinished(result, playerId, version, searchVersion);
			});
		}
	}

	private void OnSearchFinished(RPCAsyncResult result, long playerId, int version, int searchVersion)
	{
		if (!_isShowing || version != _sessionVersion || searchVersion != _searchVersion)
		{
			return;
		}
		Controller controller = type;
		if (controller == null || controller.selectedIndex != 0 || _searchPlayerId != playerId)
		{
			return;
		}
		EndSearchBusy();
		if (result.errId == 0)
		{
			FriendData friendData = null;
			SimpleSingletonProvider<GameLogicManager>.inst.friend?.TryGetFriendData(playerId, out friendData);
			if (!CanUseCandidate(playerId, friendData, onlineOnly: false))
			{
				ShowTip(3162);
				return;
			}
			_searchResultId = playerId;
			BindSearchResult();
		}
	}

	private void BindSearchResult()
	{
		if (search_player != null)
		{
			FriendData friendData = null;
			FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
			if (_searchResultId != 0L)
			{
				friend?.TryGetFriendData(_searchResultId, out friendData);
			}
			search_player.visible = friendData != null;
			if (friendData != null)
			{
				search_player.Bind(friendData, _selectedPlayerId == _searchResultId, OnCandidateSelected);
			}
		}
	}

	private void OnClickInvite()
	{
		if (_isInviting || _selectedPlayerId == 0L)
		{
			return;
		}
		FriendData friendData = null;
		SimpleSingletonProvider<GameLogicManager>.inst.friend?.TryGetFriendData(_selectedPlayerId, out friendData);
		if (!CanUseCandidate(_selectedPlayerId, friendData, onlineOnly: false))
		{
			_selectedPlayerId = 0L;
			RefreshInviteButton();
			ShowTip(3162);
			return;
		}
		RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.guild?.SendGuildInvitation(_selectedPlayerId);
		if (rPCAsyncResult == null)
		{
			ShowTip(3162);
			return;
		}
		int version = _sessionVersion;
		_isInviting = true;
		btn_Invite?.onClick.Retain();
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (_isShowing && version == _sessionVersion)
			{
				EndInviteBusy();
				if (rpcResult.errId == 0)
				{
					ShowTip(3160);
					_selectedPlayerId = 0L;
					RefreshCurrentType(requestSource: false);
				}
				else
				{
					RefreshInviteButton();
				}
			}
		});
	}

	private static bool CanUseCandidate(long playerId, FriendData data, bool onlineOnly)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (guild == null || friend == null || !guild.CanInvite || data == null || playerId == 0L)
		{
			return false;
		}
		if (playerId == guild.SelfPlayerId || guild.GetGuildMember(playerId) != null)
		{
			return false;
		}
		if (friend.IsBlack(playerId) || guild.IsGuildInvitationActive(playerId))
		{
			return false;
		}
		if (onlineOnly)
		{
			return data.IsOnline;
		}
		return true;
	}

	private void RefreshInviteButton()
	{
		if (btn_Invite != null)
		{
			bool flag = !_isInviting && _selectedPlayerId != 0L && (SimpleSingletonProvider<GameLogicManager>.inst.guild?.CanInvite ?? false);
			btn_Invite.touchable = flag;
			btn_Invite.grayed = !flag;
		}
	}

	private void OnFriendsChanged()
	{
		if (_isShowing)
		{
			Controller controller = type;
			if (controller != null && controller.selectedIndex == 1)
			{
				RebuildCandidates(SimpleSingletonProvider<GameLogicManager>.inst.friend?.ShowFriendList, onlineOnly: true);
			}
		}
	}

	private void OnRecentPlayersChanged()
	{
		if (_isShowing)
		{
			Controller controller = type;
			if (controller != null && controller.selectedIndex == 2)
			{
				RebuildCandidates(SimpleSingletonProvider<GameLogicManager>.inst.friend?.LastPlayFriends, onlineOnly: true);
			}
		}
	}

	private void OnGuildStateChanged()
	{
		if (_isShowing)
		{
			RefreshCurrentType(requestSource: false);
		}
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo playerGuild)
	{
		if (_isShowing)
		{
			if (playerGuild != null && playerGuild.GuildId == 0L)
			{
				OnHide();
			}
			else
			{
				RefreshCurrentType(requestSource: false);
			}
		}
	}

	private void EndSearchBusy()
	{
		if (_isSearching)
		{
			_isSearching = false;
			btn_search?.onClick.Release();
		}
	}

	private void EndInviteBusy()
	{
		if (_isInviting)
		{
			_isInviting = false;
			btn_Invite?.onClick.Release();
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

	public static UIGuild_Com_InviteView CreateInstance()
	{
		return (UIGuild_Com_InviteView)UIPackage.CreateObject("Guild", "Guild_Com_InviteView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GGraph)GetChildAt(0);
		btn_Search = (GButton)GetChildAt(2);
		btn_Friend = (GButton)GetChildAt(3);
		btn_LastTeam = (GButton)GetChildAt(4);
		txt_NotFriends = (GTextField)GetChildAt(6);
		txt_NotLastFriends = (GTextField)GetChildAt(7);
		list_PlayerLabel = (GList)GetChildAt(8);
		txt_TypeExplain = (GTextField)GetChildAt(9);
		btn_Invite = (GButton)GetChildAt(10);
		uid_input = (GTextInput)GetChildAt(13);
		btn_search = (UIGuild_Common_Button)GetChildAt(14);
		search_player = (UIGuild_Button_PlayerItem)GetChildAt(15);
	}
}
