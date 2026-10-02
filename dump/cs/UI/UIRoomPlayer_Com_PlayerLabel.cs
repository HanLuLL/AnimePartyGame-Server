using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityTimer;
using party.model;

namespace UI;

public class UIRoomPlayer_Com_PlayerLabel : GComponent
{
	public RoomPlayer playerInfo;

	private Timer timer;

	public Controller isMaster;

	public Controller isPlayer;

	public Controller isSelf;

	public Controller isReady;

	public Controller showShortTip;

	public GTextField txt_Null;

	public UICom_InvitePlayer com_InvitePlayer;

	public GComponent com_Label;

	public GGraph com_PopPos;

	public GButton btn_RewardUp;

	public GButton btn_ShortInfo;

	public GButton btn_Chat;

	public UIRoomPlayer_Com_FriendStatus com_FriendStatus;

	public Transition show;

	public Transition UPCut_in;

	public const string URL = "ui://m6sn3r22zi0cbg";

	public void RefreshInfo(RoomPlayer _playerInfo)
	{
		UICom_PlayerLabel uICom_PlayerLabel = (UICom_PlayerLabel)com_Label;
		if (_playerInfo != null)
		{
			if (playerInfo != null && playerInfo.Id != _playerInfo.Id)
			{
				showShortTip.selectedIndex = 0;
			}
			playerInfo = _playerInfo;
			isPlayer.selectedIndex = 1;
			isReady.selectedIndex = (playerInfo.RoomReady ? 1 : 0);
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerInfo.Id))
			{
				bool flag = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(playerInfo.Id);
				bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(playerInfo.Id);
				com_FriendStatus.type.selectedIndex = (flag ? 2 : (flag2 ? 1 : 0));
				isSelf.selectedIndex = 0;
			}
			else
			{
				isSelf.selectedIndex = 1;
			}
			CommonUIManager.RendererLabelInfo(uICom_PlayerLabel, playerInfo.GetNick(showRemark: true), playerInfo.Level);
			(string, bool) tuple = playerInfo.AccountBackgroundURL();
			CommonUIManager.RendererLabel(UIType.None, 0, uICom_PlayerLabel, tuple.Item1, tuple.Item2);
			string headShot = playerInfo.HeadURL();
			CommonUIManager.RendererHeadShot(uICom_PlayerLabel, headShot, isVideo: false);
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo != null && SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				bool flag3 = curRoomInfo.IsPVE();
				bool flag4 = playerInfo.Level <= StaticGlobalData.ROOM_PVELOCK_LEVEL;
				bool flag5 = false;
				ReturnInfo returnInfo = playerInfo?.serverPlayer?.ReturnInfo ?? null;
				if (returnInfo != null)
				{
					flag5 = TimeHelper.ValidityTime(returnInfo.TriggerTime, returnInfo.EndTime);
				}
				btn_RewardUp.visible = flag3 && (flag4 || flag5);
				UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp = btn_RewardUp as UIRoomPlayer_Button_RewardUp;
				if (flag5 && flag4)
				{
					uIRoomPlayer_Button_RewardUp.state.selectedIndex = 2;
				}
				else if (flag5)
				{
					uIRoomPlayer_Button_RewardUp.state.selectedIndex = 1;
				}
				else if (flag4)
				{
					uIRoomPlayer_Button_RewardUp.state.selectedIndex = 0;
				}
			}
			else
			{
				btn_RewardUp.visible = false;
			}
		}
		else
		{
			playerInfo = null;
			showShortTip.selectedIndex = 0;
			isMaster.selectedIndex = 0;
			isPlayer.selectedIndex = 0;
			isReady.selectedIndex = 0;
			isSelf.selectedIndex = 0;
			com_FriendStatus.type.selectedIndex = 0;
			btn_RewardUp.visible = false;
		}
	}

	private void ShowShortInfo(long playerId, int messageId)
	{
		if (playerInfo == null)
		{
			showShortTip.selectedIndex = 0;
		}
		else if (playerId == playerInfo.Id)
		{
			Timer obj = timer;
			if (obj != null)
			{
				obj.Cancel();
			}
			btn_ShortInfo.title = messageId.GetLocal(UIStringType.Chat);
			showShortTip.selectedIndex = 1;
			show.Play();
			timer = Timer.Register(3f, (System.Action)delegate
			{
				showShortTip.selectedIndex = 0;
			}, base.displayObject.gameObject);
		}
	}

	public void RefreshReady()
	{
		if (playerInfo != null)
		{
			isReady.selectedIndex = (playerInfo.RoomReady ? 1 : 0);
		}
		else
		{
			isReady.selectedIndex = 0;
		}
	}

	protected override void CreateDisplayObject()
	{
		base.CreateDisplayObject();
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.shortChat.AddListener(ShowShortInfo);
	}

	public override void Dispose()
	{
		base.Dispose();
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.shortChat.RemoveListener(ShowShortInfo);
	}

	public void RefreshPlaceholderInfo(GameModeNPCPlayerConfigure npcPlayerConfigure)
	{
		RefreshInfo(null);
		if (npcPlayerConfigure != null && npcPlayerConfigure.MonsterId != 0 && npcPlayerConfigure.MonsterId.GetMonsterInfoConfigure() != null)
		{
			isPlayer.selectedIndex = 1;
			CommonUIManager.RendererLabel((UICom_PlayerLabel)com_Label, npcPlayerConfigure);
		}
	}

	public static UIRoomPlayer_Com_PlayerLabel CreateInstance()
	{
		return (UIRoomPlayer_Com_PlayerLabel)UIPackage.CreateObject("Common_External", "RoomPlayer_Com_PlayerLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isMaster = GetControllerAt(0);
		isPlayer = GetControllerAt(1);
		isSelf = GetControllerAt(2);
		isReady = GetControllerAt(3);
		showShortTip = GetControllerAt(4);
		txt_Null = (GTextField)GetChildAt(1);
		com_InvitePlayer = (UICom_InvitePlayer)GetChildAt(2);
		com_Label = (GComponent)GetChildAt(3);
		com_PopPos = (GGraph)GetChildAt(9);
		btn_RewardUp = (GButton)GetChildAt(10);
		btn_ShortInfo = (GButton)GetChildAt(11);
		btn_Chat = (GButton)GetChildAt(12);
		com_FriendStatus = (UIRoomPlayer_Com_FriendStatus)GetChildAt(13);
		show = GetTransitionAt(0);
		UPCut_in = GetTransitionAt(1);
	}
}
