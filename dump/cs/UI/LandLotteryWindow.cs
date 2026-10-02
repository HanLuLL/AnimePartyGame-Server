using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class LandLotteryWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private MapField<int, bool> finishSelectedLottery;

	private int canChooseNum;

	private int lotteryNum;

	private readonly List<int> selectedLotterys = new List<int>();

	private long lotterySn;

	private readonly List<UILandLottery_Button_LotteryNumb> lotteryBtns = new List<UILandLottery_Button_LotteryNumb>();

	private bool isShowResult;

	public LandLotteryWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandLotteryWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandLotteryWindow> ShowLand(int selectIndex)
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow)
		{
			uILandLotteryWindow.stage.selectedIndex = 0;
			uILandLotteryWindow.stage.selectedIndex = selectIndex;
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow)
		{
			uILandLotteryWindow.com_Select.swf.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uILandLotteryWindow.turntableEffect);
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uILandLotteryWindow.loader_VictoryEffect);
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uILandLotteryWindow.loader_GoldEffect);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void DealLand_Lottery(Action _action)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowMultiplePlayerThink(_action.PlayerId, 11005);
			return;
		}
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		LotteryChoiceC2S _lotteryData = ByteBuf.ReadObject<LotteryChoiceC2S>(_action.Data.ToByteArray());
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		if (!base.isShowing)
		{
			await ShowLand(1);
		}
		RefreshLotterySelectWin(_lotteryData, _action.Sn);
	}

	public void RefreshLotterySelectWin(LotteryChoiceC2S _lotteryData, long _Sn)
	{
		lotterySn = _Sn;
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow && OperationTimer.GetOperateTimer(_Sn) == null)
		{
			canChooseNum = _lotteryData.Num;
			lotteryNum = StaticGlobalData.GAME_LAND_LOTTERY_NUMB_LIMIT;
			finishSelectedLottery = playerData.player.Hero.Lotterys;
			uILandLotteryWindow.com_Select.list_SelectNum.itemRenderer = RendererSelectList;
			uILandLotteryWindow.com_Select.list_SelectNum.numItems = lotteryNum;
			uILandLotteryWindow.com_Select.loader_Player.url = playerData.player.standingPainting.ProfilePhoto;
			uILandLotteryWindow.com_Select.txt_hasLottery.text = GetPlayerAllLottery(playerData.player.Id);
			uILandLotteryWindow.com_Select.btn_Confirm.onClick.Set(RequestLotteryPoint);
			OperationTimer.ActionDownTime(_Sn, 5041, OnCompleteSelectLottery, null, null, operateCard: false, showTimerToPlayer: false);
		}
	}

	private void OnCompleteSelectLottery()
	{
		selectedLotterys.Clear();
		for (int i = 0; i < lotteryNum; i++)
		{
			int num = i + 1;
			if (!finishSelectedLottery.TryGetValue(num, out var value) || !value)
			{
				if (selectedLotterys.Count < canChooseNum)
				{
					selectedLotterys.Add(num);
				}
				if (selectedLotterys.Count == canChooseNum)
				{
					break;
				}
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetLotteryChoiceC2S(lotterySn, selectedLotterys);
	}

	private void RequestLotteryPoint()
	{
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow && lotterySn != 0L)
		{
			uILandLotteryWindow.com_Select.btn_Confirm.onClick.Retain();
			if (selectedLotterys.Count != canChooseNum)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10006);
				uILandLotteryWindow.com_Select.btn_Confirm.onClick.Release();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetLotteryChoiceC2S(lotterySn, selectedLotterys).OnFinishedOnly.AddOnce(CloseLotteryWin);
			}
		}
	}

	private void RendererSelectList(int index, GObject item)
	{
		UILandLottery_Button_LotteryNumb btn = item as UILandLottery_Button_LotteryNumb;
		if (btn != null)
		{
			btn.com_Numb.numType.selectedIndex = index;
			btn.touchable = !finishSelectedLottery.TryGetValue(index + 1, out var value) || !value;
			btn.choosed.selectedIndex = ((finishSelectedLottery.TryGetValue(index + 1, out var value2) && value2) ? 2 : 0);
			btn.selected = false;
			lotteryBtns.Add(btn);
			btn.onClick.Set((EventCallback0)delegate
			{
				SelectLottery(btn, index);
			});
		}
	}

	private void SelectLottery(UILandLottery_Button_LotteryNumb btn, int index)
	{
		if (selectedLotterys.Contains(index + 1))
		{
			btn.selected = false;
			btn.choosed.selectedIndex = 0;
			selectedLotterys.Remove(index + 1);
			return;
		}
		if (selectedLotterys.Count >= canChooseNum)
		{
			lotteryBtns[selectedLotterys[0] - 1].selected = false;
			lotteryBtns[selectedLotterys[0] - 1].choosed.selectedIndex = 0;
			selectedLotterys.RemoveAt(0);
		}
		btn.selected = true;
		btn.choosed.selectedIndex = 1;
		selectedLotterys.Add(index + 1);
	}

	public async void TestLotteryResult()
	{
		await ShowLand(2);
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		await RefreshLotteryResultWin(new RepeatedField<long> { selfPlayerData.player.Id }, 1, 20);
	}

	public async UniTask RefreshLotteryResultWin(RepeatedField<long> _playerIds, int val, int AwardGold)
	{
		lotteryNum = StaticGlobalData.GAME_LAND_LOTTERY_NUMB_LIMIT;
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.BattlePlayer);
			isShowResult = true;
			uILandLotteryWindow.cutInResult.Play();
			uILandLotteryWindow.lotteryResult.selectedIndex = 0;
			ResetLotteryResultUI(uILandLotteryWindow);
			RendererPlayerLottery();
			uILandLotteryWindow.txt_LotteryReward.text = AwardGold.ToString();
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500))
			{
				CloseLotteryWin();
				return;
			}
			await ShowLotteryResult(_playerIds, val);
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.BattlePlayer);
		}
	}

	private void ResetLotteryResultUI(UILandLotteryWindow win)
	{
		win.com_lotteryTurntable.rotation = 0f;
		for (int i = 1; i <= 12; i++)
		{
			if (win.com_lotteryTurntable.GetChild("num" + i) is GTextField gTextField)
			{
				gTextField.color = Color.white;
			}
		}
	}

	private void RendererPlayerLottery()
	{
		if (!(base.contentPane is UILandLotteryWindow uILandLotteryWindow))
		{
			return;
		}
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].characterType != CharacterType.Monster)
			{
				if (i == 0)
				{
					uILandLotteryWindow.loader_Player_1.url = playerDatas[i].player.standingPainting.ProfilePhoto;
					uILandLotteryWindow.txt_hasLottery_1.text = GetPlayerAllLottery(playerDatas[i].player.Id);
				}
				if (i == 1)
				{
					uILandLotteryWindow.loader_Player_2.url = playerDatas[i].player.standingPainting.ProfilePhoto;
					uILandLotteryWindow.txt_hasLottery_2.text = GetPlayerAllLottery(playerDatas[i].player.Id);
				}
				if (i == 2)
				{
					uILandLotteryWindow.loader_Player_3.url = playerDatas[i].player.standingPainting.ProfilePhoto;
					uILandLotteryWindow.txt_hasLottery_3.text = GetPlayerAllLottery(playerDatas[i].player.Id);
				}
				if (i == 3)
				{
					uILandLotteryWindow.loader_Player_4.url = playerDatas[i].player.standingPainting.ProfilePhoto;
					uILandLotteryWindow.txt_hasLottery_4.text = GetPlayerAllLottery(playerDatas[i].player.Id);
				}
			}
		}
	}

	private string GetPlayerAllLottery(long playerId)
	{
		MapField<int, bool> lotterys = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).player.Hero.Lotterys;
		string text = "";
		lotteryNum = StaticGlobalData.GAME_LAND_LOTTERY_NUMB_LIMIT;
		for (int i = 0; i < lotteryNum; i++)
		{
			if (lotterys.TryGetValue(i + 1, out var value) && value)
			{
				isShowResult = false;
				text = text + (i + 1) + " ";
			}
		}
		return (text == "") ? "暂无" : text;
	}

	private async UniTask ShowLotteryResult(RepeatedField<long> _playerIds, int point)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UILandLotteryWindow win))
		{
			return;
		}
		win.list_Winners.itemRenderer = delegate(int index, GObject item)
		{
			RendererWinner(_playerIds[index], ((UILandLottery_Com_Cheer)item).loader_Animation);
			((UILandLottery_Com_Cheer)item).loader_Animation.visible = true;
		};
		if (!isShowResult && !(await ShowResultLottery(point)))
		{
			CloseLotteryWin();
			return;
		}
		if (_playerIds.Count > 0)
		{
			Stage.inst.PlayOneShotSound(10);
			EffectInfoConfigure effectDataConfigure = 1010.GetEffectDataConfigure();
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, win.loader_VictoryEffect, 40f);
			EffectInfoConfigure effectDataConfigure2 = 1011.GetEffectDataConfigure();
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure2.EffectName, win.loader_GoldEffect, 40f);
		}
		win.lotteryResult.selectedIndex = ((_playerIds.Count <= 0) ? 1 : 2);
		win.list_Winners.numItems = _playerIds.Count;
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
		for (int num = 0; num < _playerIds.Count; num++)
		{
			if (win.list_Winners.GetChildAt(num) is UILandLottery_Com_Cheer uILandLottery_Com_Cheer)
			{
				SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandLottery_Com_Cheer.loader_Animation);
			}
		}
		CloseLotteryWin();
	}

	private void RendererWinner(long _playerId, GGraph loader_Animation)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		SimpleSingletonProvider<CharacterAssetManager>.inst.PlayAnimation(playerDataById, "Cheer", loader_Animation, Vector2.one * 10f).grayed = false;
	}

	private async UniTask<bool> ShowResultLottery(int point)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UILandLotteryWindow win))
		{
			return false;
		}
		win.com_lotteryTurntable.rotation = 0f;
		for (int i = 1; i <= 12; i++)
		{
			if (win.com_lotteryTurntable.GetChild("num" + i) is GTextField gTextField)
			{
				gTextField.color = Color.white;
			}
		}
		if (StaticConfigure.Effect.InfoDict.TryGetValue(27, out var value))
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, win.turntableEffect, 80f);
		}
		GTweener tweener = win.com_lotteryTurntable.TweenRotate(3600 + (12 - point + 1) * 30, 5f).SetEase(EaseType.QuartOut);
		Stage.inst.PlayOneShotSound(3);
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => !tweener.completed);
		if (win.com_lotteryTurntable.GetChild("num" + point) is GTextField gTextField2)
		{
			gTextField2.color = Color.green;
		}
		return !(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000));
	}

	public void CloseLotteryWin()
	{
		if (base.contentPane is UILandLotteryWindow uILandLotteryWindow)
		{
			Hide();
			uILandLotteryWindow.stage.selectedIndex = 0;
			lotterySn = 0L;
			lotteryBtns.Clear();
			selectedLotterys.Clear();
			canChooseNum = 0;
			lotteryNum = 0;
			uILandLotteryWindow.loader_VictoryEffect.visible = false;
			uILandLotteryWindow.loader_GoldEffect.visible = false;
			uILandLotteryWindow.lotteryResult.selectedIndex = 0;
			uILandLotteryWindow.com_lotteryTurntable.rotation = 0f;
			uILandLotteryWindow.com_Select.btn_Confirm.onClick.Release();
		}
	}
}
