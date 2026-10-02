using System.Collections.Generic;
using System.Linq;
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

public class LandBatteryWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private int batteryTargetNum;

	private readonly RepeatedField<long> batteryTargetIds = new RepeatedField<long>();

	private readonly List<UILandBattery_Button_RoleHeadshot> targetPlayerBtns = new List<UILandBattery_Button_RoleHeadshot>();

	private long batterySn;

	public LandBatteryWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandBatteryWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandBatteryWindow> ShowLand()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandBatteryWindow uILandBatteryWindow)
		{
			uILandBatteryWindow.com_Battery.dialogType.selectedIndex = 0;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.BattlePlayer);
		if (base.contentPane is UILandBatteryWindow uILandBatteryWindow)
		{
			uILandBatteryWindow.com_Battery.dialogType.selectedIndex = 0;
			uILandBatteryWindow.com_Battery.btn_SureTarget.onClick.Remove(RequestChoiceTarget);
			uILandBatteryWindow.com_Battery.btn_Leave.onClick.Remove(RequestLevel);
			uILandBatteryWindow.list_Players.onClickItem.Remove(SelectTargetPlayers);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void DealLand_LandChoiceTarget(Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11006);
			return;
		}
		LandChoiceTargetC2S _choiceTargetData = ByteBuf.ReadObject<LandChoiceTargetC2S>(_action.Data.ToByteArray());
		await ShowLand();
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.BattlePlayer);
		if (_choiceTargetData.LandType == 11)
		{
			RefreshBatteryData(_choiceTargetData.TargetNum, _choiceTargetData.CanTargetIds, _action.Sn);
		}
	}

	private void RefreshBatteryData(int targetNum, MapField<long, bool> _canTargetIds, long _Sn)
	{
		GComponent gComponent = base.contentPane;
		UILandBatteryWindow win = gComponent as UILandBatteryWindow;
		if (win == null)
		{
			return;
		}
		batterySn = _Sn;
		targetPlayerBtns.Clear();
		batteryTargetNum = targetNum;
		GetBatteryTargets(_canTargetIds);
		win.list_Players.itemRenderer = RendererBatteryTargetPlayers;
		win.list_Players.numItems = batteryTargetIds.Count;
		win.list_Players.selectionMode = ((targetNum > 1) ? ListSelectionMode.Multiple : ListSelectionMode.Single);
		win.list_Players.onClickItem.Add(SelectTargetPlayers);
		win.com_Battery.btn_SureTarget.touchable = false;
		win.com_Battery.btn_SureTarget.grayed = true;
		win.com_Battery.btn_SureTarget.onClick.Add(RequestChoiceTarget);
		win.com_Battery.btn_Leave.onClick.Add(RequestLevel);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			OperationTimer.ActionDownTime(batterySn, 5063, delegate
			{
				win.com_Battery.btn_Leave.onClick.Call();
			});
		}
	}

	private void GetBatteryTargets(MapField<long, bool> canTargetIds)
	{
		batteryTargetIds.Clear();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType == CharacterType.Hero && canTargetIds.TryGetValue(playerData.player.Id, out var value) && value)
			{
				batteryTargetIds.Add(playerData.player.Id);
			}
		}
	}

	private async void RendererBatteryTargetPlayers(int index, GObject item)
	{
		if (item is UILandBattery_Button_RoleHeadshot _item)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(batteryTargetIds[index]);
			((UICom_PlayerInfo)_item.com_playerInfo).RefreshData(playerDataById);
			_item.data = batteryTargetIds[index];
			if (StaticConfigure.Effect.InfoDict.TryGetValue(28, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, _item.effect, 17f);
			}
			_item.effect.visible = false;
			_item.selected = false;
		}
	}

	private void SelectTargetPlayers(EventContext context)
	{
		if (base.contentPane is UILandBatteryWindow uILandBatteryWindow && context.data is UILandBattery_Button_RoleHeadshot btn)
		{
			uILandBatteryWindow.list_Players.onClickItem.Retain();
			SaveSelectedPlayer(btn);
			uILandBatteryWindow.com_Battery.btn_SureTarget.touchable = targetPlayerBtns.Count > 0;
			uILandBatteryWindow.com_Battery.btn_SureTarget.grayed = !uILandBatteryWindow.com_Battery.btn_SureTarget.touchable;
			uILandBatteryWindow.list_Players.onClickItem.Release();
		}
	}

	private void SaveSelectedPlayer(UILandBattery_Button_RoleHeadshot btn)
	{
		if (!(base.contentPane is UILandBatteryWindow uILandBatteryWindow))
		{
			return;
		}
		if (targetPlayerBtns.Contains(btn))
		{
			btn.selected = false;
			btn.effect.visible = false;
			targetPlayerBtns.Remove(btn);
			uILandBatteryWindow.com_Battery.dialogType.selectedIndex = 0;
			return;
		}
		if (targetPlayerBtns.Count < batteryTargetNum)
		{
			btn.effect.visible = true;
			targetPlayerBtns.Add(btn);
		}
		else
		{
			UILandBattery_Button_RoleHeadshot uILandBattery_Button_RoleHeadshot = targetPlayerBtns.FirstOrDefault();
			if (uILandBattery_Button_RoleHeadshot != null)
			{
				uILandBattery_Button_RoleHeadshot.selected = false;
				uILandBattery_Button_RoleHeadshot.effect.visible = false;
				targetPlayerBtns.Remove(uILandBattery_Button_RoleHeadshot);
				btn.effect.visible = true;
				targetPlayerBtns.Add(btn);
			}
		}
		uILandBatteryWindow.com_Battery.dialogType.selectedIndex = 1;
	}

	private void RequestChoiceTarget(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		UILandBatteryWindow win = gComponent as UILandBatteryWindow;
		if (win == null || batterySn == 0L)
		{
			return;
		}
		win.com_Battery.btn_SureTarget.onClick.Retain();
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		foreach (UILandBattery_Button_RoleHeadshot targetPlayerBtn in targetPlayerBtns)
		{
			Debug.Log($"#炮台选择# 目标Id{(long)targetPlayerBtn.data}");
			repeatedField.Add((long)targetPlayerBtn.data);
		}
		win.com_Battery.dialogType.selectedIndex = 2;
		SimpleSingletonProvider<GameLogicManager>.inst.land.RequestLandChoiceTargetC2S(batterySn, repeatedField).OnFinishedOnly.AddOnce(delegate
		{
			batterySn = 0L;
			win.com_Battery.btn_SureTarget.onClick.Release();
		});
	}

	private void RequestLevel()
	{
		if (batterySn == 0L)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandBatteryWindow win = gComponent as UILandBatteryWindow;
		if (win != null)
		{
			win.com_Battery.btn_Leave.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestBatteryLeave(batterySn, exit: true).OnFinishedOnly.AddOnce(delegate
			{
				batterySn = 0L;
				win.com_Battery.btn_Leave.onClick.Release();
			});
		}
	}

	public void AttackTargetPlayer(long modelPlayerId, RepeatedField<long> modelTargetIds)
	{
		batteryTargetIds.Clear();
		HideImmediately();
	}
}
