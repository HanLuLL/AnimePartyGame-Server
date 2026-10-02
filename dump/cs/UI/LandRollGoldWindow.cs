using System;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class LandRollGoldWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private LandInfoConfigure landConfig;

	public LandRollGoldWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandRollGoldWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandRollGoldWindow> ShowLand()
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
		if (base.contentPane is UILandRollGoldWindow uILandRollGoldWindow)
		{
			landConfig = StaticConfigure.Land.InfoDict[15];
			uILandRollGoldWindow.list_Gold.itemRenderer = RendererGold;
			uILandRollGoldWindow.list_Gold.numItems = landConfig.Params.Count;
		}
	}

	private void RendererGold(int index, GObject item)
	{
		if (item is UILandRollGold_Button_Gold uILandRollGold_Button_Gold)
		{
			uILandRollGold_Button_Gold.selected = false;
			uILandRollGold_Button_Gold.txt_Gold.SetVar("gold", landConfig.Params[index].ToString()).FlushVars();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void DealLand_RollGold(party.model.Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetRollGoldC2S(_action.Sn);
	}

	public async UniTask RefreshRollGoldDice(int index)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UILandRollGoldWindow win))
		{
			return;
		}
		if (index >= landConfig.Params.Count)
		{
			Debug.LogError($"服务器提供的参数索引为——{index}, 配置参数数量为{landConfig.Params.Count}");
			index = 0;
		}
		int num = landConfig.Params.Count + index;
		for (int i = 0; i < num; i++)
		{
			win.list_Gold.GetChildAt(i % landConfig.Params.Count).onClick.Call();
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(0.05 + 0.05 * (double)i / (double)landConfig.Params.Count)))
			{
				HideImmediately();
				return;
			}
		}
		win.list_Gold.GetChildAt(index).onClick.Call();
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
		HideImmediately();
	}
}
