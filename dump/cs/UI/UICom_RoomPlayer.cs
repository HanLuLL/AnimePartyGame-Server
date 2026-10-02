using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UICom_RoomPlayer : GComponent
{
	private List<RoomPlayer> Players;

	private UIRoomPlayer_Com_MasterMenu masterMenu;

	private UIRoomPlayer_Com_ShortChat shortChat;

	private UIRoomPlayer_Com_RewardUpDesc rewardUpDesc;

	private long _captain;

	private MapModeType _mapMode;

	private Dictionary<int, GameModeNPCPlayerConfigure> _npcPlayerConfigures = new Dictionary<int, GameModeNPCPlayerConfigure>();

	private const float PCScale = 0.65f;

	private float _lastSendTime;

	private RepeatedField<int> _termIds = new RepeatedField<int>();

	public GList list_playerWait;

	public GButton btn_terms;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://m6sn3r22zi0cbx";

	public void InitComponents()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			masterMenu = UIRoomPlayer_Com_MasterMenu.CreateInstance();
		}
		shortChat = UIRoomPlayer_Com_ShortChat.CreateInstance();
		if (PlatformTarget.IsMobileTarget)
		{
			shortChat.scale = Vector2.one;
		}
		else
		{
			shortChat.scale = Vector2.one * 0.65f;
		}
		shortChat.list_Chat.itemRenderer = RendererShotInfo;
		rewardUpDesc = UIRoomPlayer_Com_RewardUpDesc.CreateInstance();
		list_playerWait.itemRenderer = RendererPlayerWaitItem;
	}

	public void Close()
	{
		CommonUIManager.StopAllVideo();
		GRoot.inst.HidePopup(masterMenu);
		GRoot.inst.HidePopup(shortChat);
		GRoot.inst.HidePopup(rewardUpDesc);
	}

	public void DisposeCom()
	{
		GRoot.inst.HidePopup(masterMenu);
		masterMenu?.Dispose();
		masterMenu = null;
		GRoot.inst.HidePopup(shortChat);
		shortChat?.Dispose();
		shortChat = null;
		GRoot.inst.HidePopup(rewardUpDesc);
		rewardUpDesc?.Dispose();
		rewardUpDesc = null;
	}

	public void AddEvent()
	{
		if (masterMenu != null)
		{
			masterMenu.btn_SetMaster.onClick.Add(SetNewMaster);
			masterMenu.btn_KickPlayer.onClick.Add(KickPlayer);
			masterMenu.btn_AccountInfo.onClick.Add(ShowAccountInfo);
		}
		shortChat?.list_Chat.onClickItem.Add(RequestSendShortInfo);
		btn_terms.onClick.Add(OpenTremsWindow);
	}

	public void RemoveEvent()
	{
		if (masterMenu != null)
		{
			masterMenu.btn_SetMaster.onClick.Remove(SetNewMaster);
			masterMenu.btn_KickPlayer.onClick.Remove(KickPlayer);
			masterMenu.btn_AccountInfo.onClick.Remove(ShowAccountInfo);
		}
		shortChat?.list_Chat.onClickItem.Remove(RequestSendShortInfo);
		btn_terms.onClick.Remove(OpenTremsWindow);
	}

	public void RefreshPlayer(List<RoomPlayer> playersData, long captain, MapModeType mapMode)
	{
		GRoot.inst.HidePopup(masterMenu);
		Players = playersData;
		_captain = captain;
		_mapMode = mapMode;
		SetBotPlaceholder();
		RefreshMapMode();
		list_playerWait.numItems = 4;
	}

	private RoomPlayer GetPlayerData(long playerId)
	{
		return Players?.Find((RoomPlayer x) => x.Id == playerId);
	}

	private void RendererPlayerWaitItem(int index, GObject item)
	{
		UIRoomPlayer_Com_PlayerLabel _item = item as UIRoomPlayer_Com_PlayerLabel;
		if (_item == null)
		{
			return;
		}
		if (_npcPlayerConfigures.ContainsKey(index))
		{
			RefreshPlaceholderInfo(_item, index);
			return;
		}
		if (index < Players.Count)
		{
			_item.RefreshInfo(Players[index]);
			_item.isMaster.selectedIndex = ((Players[index].Id == _captain) ? 1 : 0);
			_item.btn_Chat.onClick.Set(ShowShortChat);
			if (PlatformTarget.IsMobileTarget)
			{
				_item.btn_Chat.scale = Vector2.one;
			}
			else
			{
				_item.btn_Chat.scale = Vector2.one * 0.65f;
			}
			_item.com_Label.onClick.Set((EventCallback0)delegate
			{
				_item.onClick.Retain();
				ShowMasterMenu(_item, _item.playerInfo.Id);
				_item.onClick.Release();
			});
			_item.btn_RewardUp.onClick.Set((EventCallback0)delegate
			{
				_item.btn_RewardUp.onClick.Retain();
				if (_item.btn_RewardUp is UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp)
				{
					if (uIRoomPlayer_Button_RewardUp.state.selectedIndex == 0)
					{
						rewardUpDesc.txt_Title1.text = 1095.GetLocal(UIStringType.Message);
						rewardUpDesc.txt_Desc1.text = 1096.GetLocal(UIStringType.Message);
						if (rewardUpDesc.up1 is UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp2)
						{
							uIRoomPlayer_Button_RewardUp2.state.selectedIndex = 0;
						}
						rewardUpDesc.showType.selectedIndex = 0;
					}
					else if (uIRoomPlayer_Button_RewardUp.state.selectedIndex == 1)
					{
						rewardUpDesc.txt_Title1.text = 1135.GetLocal(UIStringType.Message);
						rewardUpDesc.txt_Desc1.text = 1136.GetLocal(UIStringType.Message);
						if (rewardUpDesc.up1 is UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp3)
						{
							uIRoomPlayer_Button_RewardUp3.state.selectedIndex = 1;
						}
						rewardUpDesc.showType.selectedIndex = 0;
					}
					else if (uIRoomPlayer_Button_RewardUp.state.selectedIndex == 2)
					{
						rewardUpDesc.txt_Title1.text = 1095.GetLocal(UIStringType.Message);
						rewardUpDesc.txt_Desc1.text = 1096.GetLocal(UIStringType.Message);
						if (rewardUpDesc.up1 is UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp4)
						{
							uIRoomPlayer_Button_RewardUp4.state.selectedIndex = 0;
						}
						rewardUpDesc.txt_Title2.text = 1135.GetLocal(UIStringType.Message);
						rewardUpDesc.txt_Desc2.text = 1136.GetLocal(UIStringType.Message);
						if (rewardUpDesc.up2 is UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp5)
						{
							uIRoomPlayer_Button_RewardUp5.state.selectedIndex = 1;
						}
						rewardUpDesc.showType.selectedIndex = 1;
					}
				}
				GRoot.inst.ShowPopup(rewardUpDesc, _item.btn_RewardUp, PopupDirection.Down);
				rewardUpDesc.SetXY(rewardUpDesc.x - rewardUpDesc.width, rewardUpDesc.y - rewardUpDesc.height * 0.5f);
				_item.btn_RewardUp.onClick.Release();
			});
		}
		else
		{
			_item.RefreshInfo(null);
			_item.com_Label.onClick.Clear();
		}
		MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
		if (_item.playerInfo == null)
		{
			int maxTeammateCountByMode = matchData.GetMaxTeammateCountByMode(_mapMode);
			bool flag = index < maxTeammateCountByMode;
			if (flag)
			{
				bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_captain);
				_item.txt_Null.visible = !flag2;
				_item.com_InvitePlayer.visible = flag2;
			}
			else
			{
				_item.txt_Null.visible = false;
				_item.com_InvitePlayer.visible = true;
			}
			if (matchData.InTeam && !flag)
			{
				_item.com_InvitePlayer.status.selectedIndex = 1;
			}
			else
			{
				_item.com_InvitePlayer.status.selectedIndex = 0;
				_item.com_InvitePlayer.txt_Invite.text = 1020010.GetLocal(UIStringType.GUI);
			}
		}
		else
		{
			_item.com_InvitePlayer.visible = false;
		}
		_item.com_InvitePlayer.btn_Invite.onClick.Set((EventCallback0)delegate
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_captain) && _item.playerInfo == null)
			{
				_item.com_InvitePlayer.btn_Invite.onClick.Retain();
				if (!SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.InTeam || SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.IsVailOperate())
				{
					SimpleSingletonProvider<UIManager>.inst.invite.OpenFriendList().Forget();
					_item.com_InvitePlayer.btn_Invite.onClick.Release();
				}
			}
		});
	}

	public void RefreshReadyStatus(long playerId)
	{
		GObject gObject = list_playerWait._children.Find((GObject x) => x is UIRoomPlayer_Com_PlayerLabel { playerInfo: not null } uIRoomPlayer_Com_PlayerLabel2 && uIRoomPlayer_Com_PlayerLabel2.playerInfo.Id == playerId);
		if (gObject != null && gObject is UIRoomPlayer_Com_PlayerLabel uIRoomPlayer_Com_PlayerLabel)
		{
			uIRoomPlayer_Com_PlayerLabel.RefreshReady();
		}
	}

	private void ShowMasterMenu(UIRoomPlayer_Com_PlayerLabel label, long playerId)
	{
		if (masterMenu != null)
		{
			RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
			MatchLogic match = SimpleSingletonProvider<GameLogicManager>.inst.match;
			bool flag = false;
			if (room != null && room.IsInRoom)
			{
				flag = !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(room.curRoomInfo.MasterId);
			}
			else
			{
				_ = match?.matchData?.InTeam;
			}
			masterMenu.btn_KickPlayer.visible = flag;
			masterMenu.btn_SetMaster.visible = flag;
			masterMenu.data = playerId;
			GRoot.inst.ShowPopup(masterMenu, label.com_PopPos, PopupDirection.Down);
		}
	}

	private void SetNewMaster(EventContext context)
	{
		if (masterMenu == null)
		{
			return;
		}
		masterMenu.btn_SetMaster.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestAbdicationC2S((long)masterMenu.data).OnFinished.AddOnce(delegate
		{
			if (masterMenu != null)
			{
				GRoot.inst.HidePopup(masterMenu);
				masterMenu.btn_SetMaster.onClick.Release();
			}
		});
	}

	private void KickPlayer(EventContext context)
	{
		if (masterMenu == null)
		{
			return;
		}
		masterMenu.btn_KickPlayer.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestKickPlayer((long)masterMenu.data).OnFinished.AddOnce(delegate
		{
			if (masterMenu != null)
			{
				GRoot.inst.HidePopup(masterMenu);
				masterMenu.btn_KickPlayer.onClick.Release();
			}
		});
	}

	private void ShowAccountInfo()
	{
		if (masterMenu != null)
		{
			long num = (long)masterMenu.data;
			RoomPlayer playerData = GetPlayerData(num);
			(string, bool) labelData = playerData.AccountBackgroundURL();
			string headIcon = playerData.HeadURL();
			num = (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(num) ? 0 : num);
			masterMenu.btn_AccountInfo.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(num, playerData.GetNick(), playerData.Level, headIcon, labelData, delegate
			{
				masterMenu?.btn_AccountInfo.onClick.Release();
			});
		}
	}

	private void ShowShortChat(EventContext context)
	{
		if (context.sender is GButton gButton && shortChat != null)
		{
			gButton.onClick.Retain();
			shortChat.list_Chat.numItems = StaticConfigure.Chat.RoomWaitChats.Count;
			GRoot.inst.ShowPopup(shortChat, gButton);
			Vector2 pt = gButton.LocalToGlobal(Vector2.zero);
			Vector2 vector = GRoot.inst.GlobalToLocal(pt);
			shortChat.SetXY(vector.x - shortChat.width * shortChat.scale.x, vector.y);
			gButton.onClick.Release();
		}
	}

	private void RendererShotInfo(int index, GObject item)
	{
		if (item is GButton gButton)
		{
			gButton.data = index;
			gButton.title = StaticConfigure.Chat.RoomWaitChats[index].ChatInfo;
		}
	}

	private void RequestSendShortInfo(EventContext context)
	{
		float time = Time.time;
		if ((time - _lastSendTime) * 1000f > (float)StaticGlobalData.GAME_CHARACTER_EXPRESSION_CD)
		{
			if (context.data is GButton { data: var obj } && obj is int index)
			{
				shortChat.list_Chat.onClickItem.Retain();
				int chatID = StaticConfigure.Chat.RoomWaitChats[index].ChatID;
				long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.shortChat.Dispatch(playerID, chatID);
				_lastSendTime = time;
				MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
				if (matchData.InTeam)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.match.RequestMatchTeamChatC2S(matchData.TeamId, chatID);
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestRoomShortChatC2S(chatID);
				}
				shortChat.list_Chat.onClickItem.Release();
			}
			GRoot.inst.HidePopup(shortChat);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10017);
		}
	}

	private void SetBotPlaceholder()
	{
		_npcPlayerConfigures.Clear();
		if (_mapMode == MapModeType.MutatorPve)
		{
			GameModeNPCPlayerConfigure gameModeNPCPlayerConfigure = ((int)_mapMode).GetGameModeNPCPlayerConfigure();
			_npcPlayerConfigures.Add(gameModeNPCPlayerConfigure.RoomPosition, gameModeNPCPlayerConfigure);
		}
	}

	public void RefreshPlaceholderInfo(UIRoomPlayer_Com_PlayerLabel item, int index)
	{
		if (_npcPlayerConfigures.TryGetValue(index, out var value))
		{
			item.RefreshPlaceholderInfo(value);
		}
	}

	private void RefreshMapMode()
	{
		if (_mapMode == MapModeType.MutatorPve)
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
			if (matchData != null && matchData.InTeam)
			{
				btn_terms.visible = matchData.MatchMapId != 0;
				return;
			}
			if (curRoomInfo != null)
			{
				btn_terms.visible = curRoomInfo.MapId != 0;
				return;
			}
		}
		btn_terms.visible = false;
	}

	private void OpenTremsWindow()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
		int num = 0;
		int num2 = -1;
		if (matchData != null && matchData.InTeam)
		{
			num = matchData.MatchMapId;
			num2 = matchData.MatchDifficulty;
		}
		else if (curRoomInfo != null)
		{
			num = curRoomInfo.MapId;
			num2 = curRoomInfo.Difficulty;
		}
		if (num == 0 || num2 < 0)
		{
			return;
		}
		_termIds.Clear();
		foreach (int difficultyId in num.GetMapDataConfigure().DifficultyIds)
		{
			foreach (MapGameDifficultyConfigureItem mapGameDifficultyItem in difficultyId.GetMapGameDifficultyItems())
			{
				if (mapGameDifficultyItem.Index != num2)
				{
					continue;
				}
				foreach (MutatorPoolConfigureItem item in mapGameDifficultyItem.MutatorPool.GetMutatorPoolComposeConfigure())
				{
					if (!_termIds.Contains(item.MutatorId))
					{
						_termIds.Add(item.MutatorId);
					}
				}
				break;
			}
		}
		if (_termIds.Count != 0)
		{
			btn_terms.visible = false;
			SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowWinScreenTerms(_termIds, delegate
			{
				btn_terms.visible = true;
			}).Forget();
		}
	}

	public static UICom_RoomPlayer CreateInstance()
	{
		return (UICom_RoomPlayer)UIPackage.CreateObject("Common_External", "Com_RoomPlayer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_playerWait = (GList)GetChildAt(1);
		btn_terms = (GButton)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
