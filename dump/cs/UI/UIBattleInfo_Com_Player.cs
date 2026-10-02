using System.Collections.Generic;
using Core.Camera;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Com_Player : GComponent
{
	private RepeatedField<long> _UsablePlayers;

	private int _SelectActionId;

	private int _targetNum;

	private readonly List<long> _targetPlayerIds = new List<long>();

	public Controller showCancelBtn;

	public UIBattleInfo_Com_PlayerContainer com_Container;

	public GButton btn_CancelSelect;

	public GComponent com_Empty;

	public const string URL = "ui://fxejlqlfsxcpbf";

	public void InitComponent()
	{
		com_Container.com_Player_1.PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataBySlot(0);
		com_Container.com_Player_2.PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataBySlot(1);
		com_Container.com_Player_3.PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataBySlot(2);
		com_Container.com_Player_4.PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataBySlot(3);
		btn_CancelSelect.MakeFullScreen();
		btn_CancelSelect.Center();
	}

	public void Init()
	{
		com_Container.com_Player_1.Init();
		com_Container.com_Player_2.Init();
		com_Container.com_Player_3.Init();
		com_Container.com_Player_4.Init();
	}

	public void AddEvent()
	{
		btn_CancelSelect.onClick.Add(CancelCardSelect);
		com_Container.com_Player_1.onClick.Add(OnClickPlayer);
		com_Container.com_Player_2.onClick.Add(OnClickPlayer);
		com_Container.com_Player_3.onClick.Add(OnClickPlayer);
		com_Container.com_Player_4.onClick.Add(OnClickPlayer);
	}

	public void RemoveEvent()
	{
		btn_CancelSelect.onClick.Remove(CancelCardSelect);
		com_Container.com_Player_1.onClick.Remove(OnClickPlayer);
		com_Container.com_Player_2.onClick.Remove(OnClickPlayer);
		com_Container.com_Player_3.onClick.Remove(OnClickPlayer);
		com_Container.com_Player_4.onClick.Remove(OnClickPlayer);
	}

	public void AddListener()
	{
		com_Container.com_Player_1.AddListener();
		com_Container.com_Player_2.AddListener();
		com_Container.com_Player_3.AddListener();
		com_Container.com_Player_4.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.AddListener(OnEffectCardSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.showSelectPlayer.AddListener(ShowCanSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.closeSelectPlayer.AddListener(CloseSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.AddListener(NotifyAllPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showActionMask.AddListener(OnShowActionMask);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.AddListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.AddListener(LimitOnClickPlayerInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerInfoRefresh.AddListener(Init);
	}

	public void RemoveListener()
	{
		com_Container.com_Player_1.RemoveListener();
		com_Container.com_Player_2.RemoveListener();
		com_Container.com_Player_3.RemoveListener();
		com_Container.com_Player_4.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.RemoveListener(OnEffectCardSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.showSelectPlayer.RemoveListener(ShowCanSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.closeSelectPlayer.RemoveListener(CloseSelectPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.RemoveListener(NotifyAllPlayer);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showActionMask.RemoveListener(OnShowActionMask);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.RemoveListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.RemoveListener(LimitOnClickPlayerInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerInfoRefresh.RemoveListener(Init);
	}

	public void CoverMode(bool inCoverMode)
	{
		com_Container.com_Player_1.UpdateName();
		com_Container.com_Player_2.UpdateName();
		com_Container.com_Player_3.UpdateName();
		com_Container.com_Player_4.UpdateName();
	}

	private void CancelCardSelect()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
		OnEffectCardSelectPlayer(0, null, 0);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
		SimpleSingletonProvider<UIManager>.inst.cardWindow.CancelSelectPlayer();
	}

	private void OnShowActionMask(bool _trigger)
	{
		base.touchableAll = !_trigger;
	}

	private void NotifyAllPlayer(long _playerId)
	{
		com_Container.com_Player_1.PlayAction(_playerId);
		com_Container.com_Player_2.PlayAction(_playerId);
		com_Container.com_Player_3.PlayAction(_playerId);
		com_Container.com_Player_4.PlayAction(_playerId);
	}

	private void ReadyFight(bool state, UIPanelType panelType)
	{
		if (panelType == UIPanelType.None || panelType == UIPanelType.BattlePlayer)
		{
			int num = ((!state) ? 1 : 0);
			SetScale(num, num);
		}
	}

	private void LimitOnClickPlayerInfo(bool state)
	{
		if (state)
		{
			com_Container.com_Player_1.onClick.Retain();
			com_Container.com_Player_2.onClick.Retain();
			com_Container.com_Player_3.onClick.Retain();
			com_Container.com_Player_4.onClick.Retain();
		}
		else
		{
			com_Container.com_Player_1.onClick.Release();
			com_Container.com_Player_2.onClick.Release();
			com_Container.com_Player_3.onClick.Release();
			com_Container.com_Player_4.onClick.Release();
		}
	}

	private void OnClickPlayer(EventContext context)
	{
		if (!(context.sender is UIBattleInfo_Button_PlayerInfo { PlayerData: not null } uIBattleInfo_Button_PlayerInfo))
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher())
		{
			SimpleSingletonProvider<CameraManager>.inst.ControlFreeCamera(uIBattleInfo_Button_PlayerInfo.PlayerData.CharacterInst.transform.position);
			SimpleSingletonProvider<GameLogicManager>.inst.watch.UpdateSubscribePlayer(uIBattleInfo_Button_PlayerInfo.PlayerData.player.Id);
			SimpleSingletonProvider<GameLogicManager>.inst.watch.SwitchFollow(isFollow: false);
			return;
		}
		PlayerActionEnum playerAction = SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction;
		if (playerAction != PlayerActionEnum.CHOOSETARGET_CARD && playerAction != PlayerActionEnum.CHOOSETARGET_SKILL)
		{
			return;
		}
		uIBattleInfo_Button_PlayerInfo.onClick.Retain();
		if (_UsablePlayers == null || !_UsablePlayers.Contains(uIBattleInfo_Button_PlayerInfo.PlayerData.player.Id) || _targetPlayerIds.Contains(uIBattleInfo_Button_PlayerInfo.PlayerData.player.Id))
		{
			return;
		}
		uIBattleInfo_Button_PlayerInfo.selectPlayer.selectedIndex = 1;
		_targetPlayerIds.Add(uIBattleInfo_Button_PlayerInfo.PlayerData.player.Id);
		if (_targetPlayerIds.Count != _targetNum)
		{
			uIBattleInfo_Button_PlayerInfo.onClick.Release();
			return;
		}
		ForcedPlayerBtn();
		showCancelBtn.selectedIndex = 0;
		if (SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction == PlayerActionEnum.CHOOSETARGET_CARD)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
			{
				RepeatedField<long> targetPlayerIds = new RepeatedField<long> { _targetPlayerIds };
				SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceCard_20013Result(targetPlayerIds);
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, _SelectActionId, _targetPlayerIds);
			}
		}
		else if (SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction == PlayerActionEnum.CHOOSETARGET_SKILL)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData?.CharacterInst == null || selfPlayerData.CharacterInst.skill == null)
			{
				Debug.LogError("主动技能无法获取技能实例, 需要检查");
				return;
			}
			selfPlayerData.CharacterInst.skill.RequestReleaseSkillBySelectTarget(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, _targetPlayerIds);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.NONE;
		uIBattleInfo_Button_PlayerInfo.onClick.Release();
	}

	private void OnEffectCardSelectPlayer(int actionId, RepeatedField<long> playerIds, int targetNum)
	{
		showCancelBtn.selectedIndex = ((actionId != 0) ? 1 : 0);
		_targetPlayerIds.Clear();
		_UsablePlayers = playerIds;
		_SelectActionId = actionId;
		_targetNum = targetNum;
		AdjustPlayerParent(com_Container.com_Player_4, playerIds);
		AdjustPlayerParent(com_Container.com_Player_3, playerIds);
		AdjustPlayerParent(com_Container.com_Player_2, playerIds);
		AdjustPlayerParent(com_Container.com_Player_1, playerIds);
	}

	private void AdjustPlayerParent(UIBattleInfo_Button_PlayerInfo comPlayer, RepeatedField<long> playerIds)
	{
		if (comPlayer.PlayerData != null)
		{
			comPlayer.onClick.Release();
			comPlayer.selectPlayer.selectedIndex = 0;
			if (playerIds != null && playerIds.Contains(comPlayer.PlayerData.player.Id))
			{
				com_Empty.AddChild(comPlayer);
				comPlayer.ShowSelectTips();
			}
			else
			{
				com_Container.AddChild(comPlayer);
				comPlayer.CloseSelectTips();
			}
		}
	}

	private void ForcedPlayerBtn()
	{
		com_Container.com_Player_4.selectPlayer.selectedIndex = 0;
		com_Container.AddChild(com_Container.com_Player_4);
		com_Container.com_Player_4.CloseSelectTips();
		com_Container.com_Player_3.selectPlayer.selectedIndex = 0;
		com_Container.AddChild(com_Container.com_Player_3);
		com_Container.com_Player_3.CloseSelectTips();
		com_Container.com_Player_2.selectPlayer.selectedIndex = 0;
		com_Container.AddChild(com_Container.com_Player_2);
		com_Container.com_Player_2.CloseSelectTips();
		com_Container.com_Player_1.selectPlayer.selectedIndex = 0;
		com_Container.AddChild(com_Container.com_Player_1);
		com_Container.com_Player_1.CloseSelectTips();
		SimpleSingletonProvider<UIManager>.inst.cardWindow.CloseSelectPlayer();
	}

	private void ShowCanSelectPlayer(RepeatedField<long> players, bool state)
	{
		com_Container.com_Player_1.selectPlayer.selectedIndex = ((com_Container.com_Player_1.PlayerData != null && players.Contains(com_Container.com_Player_1.PlayerData.player.Id)) ? (state ? 1 : 0) : 0);
		com_Container.com_Player_2.selectPlayer.selectedIndex = ((com_Container.com_Player_2.PlayerData != null && players.Contains(com_Container.com_Player_2.PlayerData.player.Id)) ? (state ? 1 : 0) : 0);
		com_Container.com_Player_3.selectPlayer.selectedIndex = ((com_Container.com_Player_3.PlayerData != null && players.Contains(com_Container.com_Player_3.PlayerData.player.Id)) ? (state ? 1 : 0) : 0);
		com_Container.com_Player_4.selectPlayer.selectedIndex = ((com_Container.com_Player_4.PlayerData != null && players.Contains(com_Container.com_Player_4.PlayerData.player.Id)) ? (state ? 1 : 0) : 0);
	}

	private void CloseSelectPlayer()
	{
		btn_CancelSelect.onClick.Call();
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.NONE;
	}

	public void OnMarkTipShow()
	{
		com_Container.com_Player_1.OnMarkTipShow();
		com_Container.com_Player_2.OnMarkTipShow();
		com_Container.com_Player_3.OnMarkTipShow();
		com_Container.com_Player_4.OnMarkTipShow();
	}

	public void OnMarkTipHide()
	{
		com_Container.com_Player_1.OnMarkTipHide();
		com_Container.com_Player_2.OnMarkTipHide();
		com_Container.com_Player_3.OnMarkTipHide();
		com_Container.com_Player_4.OnMarkTipHide();
	}

	public static UIBattleInfo_Com_Player CreateInstance()
	{
		return (UIBattleInfo_Com_Player)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showCancelBtn = GetControllerAt(0);
		com_Container = (UIBattleInfo_Com_PlayerContainer)GetChildAt(0);
		btn_CancelSelect = (GButton)GetChildAt(1);
		com_Empty = (GComponent)GetChildAt(2);
	}
}
