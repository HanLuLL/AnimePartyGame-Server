using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core;

public static class LandEventShow
{
	public static async UniTask LandEventShow_30001(UpdateHeroAttrS2C model)
	{
		List<long> _playerList = new List<long>(4);
		List<int> _goldList = new List<int>(4);
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			HeroGoldChangeS2C gold = model.EffectDatas[i].Gold;
			if (gold != null && gold.ChangeGold < 0 && !_playerList.Contains(model.EffectDatas[i].PlayerId))
			{
				_playerList.Add(model.EffectDatas[i].PlayerId);
				_goldList.Add(model.EffectDatas[i].Gold.ChangeGold);
			}
		}
		int subPlayerNum = _playerList.Count;
		for (int j = 0; j < model.EffectDatas.Count; j++)
		{
			if (!_playerList.Contains(model.EffectDatas[j].PlayerId))
			{
				_playerList.Add(model.EffectDatas[j].PlayerId);
				_goldList.Add(model.EffectDatas[j].Gold.ChangeGold);
			}
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
		await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).ShowEvent_30001(_playerList, _goldList, subPlayerNum);
	}

	public static async UniTask LandEventShow_30002(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30003(UpdateHeroAttrS2C model)
	{
		await ShowAttrChange_Single(StaticConfigure.Event.InfoDict[30003], model);
	}

	public static async UniTask LandEventShow_30004(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30005(UpdateHeroAttrS2C model)
	{
		await ChangePlace(StaticConfigure.Event.InfoDict[30005], model);
	}

	public static async UniTask LandEventShow_30006(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30006];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30007(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30008(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30008];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30009(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30009];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30010(UpdateHeroAttrS2C model)
	{
		await ShowAttrChange_Single(StaticConfigure.Event.InfoDict[30010], model);
	}

	public static async UniTask LandEventShow_30011(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30010];
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		int performId = ((SimpleSingletonProvider<GameLogicManager>.inst.land.land_Point >= eventInfoConfigure.Params[0]) ? eventInfoConfigure.Perform2 : eventInfoConfigure.Perform1);
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			HeroGoldChangeS2C gold = model.EffectDatas[i].Gold;
			if (gold != null && gold.ChangeGold == 0)
			{
				HeroHpChangeS2C hp = model.EffectDatas[i].Hp;
				if (hp != null && hp.ChangeHp == 0)
				{
					continue;
				}
			}
			await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, performId, $"事件id—{30010}表演");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	public static async UniTask LandEventShow_30012(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30013(UpdateHeroAttrS2C model)
	{
		await ChangePlace(StaticConfigure.Event.InfoDict[30013], model);
	}

	public static async UniTask LandEventShow_30014(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30014];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30015(UpdateHeroAttrS2C model)
	{
		await ChangePlace(StaticConfigure.Event.InfoDict[30015], model);
	}

	public static async UniTask LandEventShow_30016(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30017(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30018(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30019(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30019];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30020(UpdateHeroAttrS2C model)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	public static async UniTask LandEventShow_30021(UpdateHeroAttrS2C model)
	{
		await SwitchSelfCamera();
		EventInfoConfigure _config = StaticConfigure.Event.InfoDict[30021];
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(effectData.PlayerId);
			if (playerDataById != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.OnCardChanged(playerDataById, new RepeatedField<CardInfo>());
			}
		}
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500)))
		{
			await ShowAttrChange_ALL(_config.Id, _config.Perform2, model);
		}
	}

	public static async UniTask LandEventShow_30201(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30202(UpdateHeroAttrS2C model)
	{
		EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.InfoDict[30202];
		await ShowAttrChange_ALL(eventInfoConfigure.Id, eventInfoConfigure.Perform1, model);
	}

	public static async UniTask LandEventShow_30203(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30204(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public static async UniTask LandEventShow_30206(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		EventInfoConfigure _config = StaticConfigure.Event.InfoDict[30206];
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			int performId = ((model.EffectDatas[i].Hp.ChangeHp > 0) ? _config.Perform1 : _config.Perform2);
			await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, performId, "30206 取长补短演出");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	private static async UniTask ChangePlace(EventInfoConfigure _config, UpdateHeroAttrS2C model)
	{
		await SwitchSelfCamera();
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		UniTask[] showTask = new UniTask[4];
		for (int i = 0; i < playerIds.Count; i++)
		{
			showTask[i] = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerIds[i], _config.Perform1, "事件传送开始");
		}
		if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(showTask))
		{
			return;
		}
		for (int j = 0; j < model.EffectDatas.Count; j++)
		{
			if (model.EffectDatas[j].Place != null)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.EffectDatas[j].PlayerId);
				if (playerDataById != null && playerDataById.CharacterInst != null)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerDataById.CharacterInst, willMove: true);
					playerDataById.CharacterInst.SendCharacter(model.EffectDatas[j].Place.Place.NodeId, model.EffectDatas[j].Place.Place.FrontNodeIds);
				}
				else
				{
					Debug.LogError($"根据玩家ID: {model.EffectDatas[j].PlayerId} 查询到底数据存在问题，{playerDataById == null} {playerDataById?.CharacterInst == null}");
				}
			}
		}
		for (int k = 0; k < playerIds.Count; k++)
		{
			showTask[k] = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerIds[k], _config.Perform2, "事件传送结束");
		}
		await SwitchSelfCamera();
		await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(showTask);
		BattlePlayerData currentPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer();
		if (currentPlayer != null && currentPlayer.CharacterInst != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(currentPlayer.CharacterInst, willMove: false);
		}
		else
		{
			Debug.LogError($"{currentPlayer == null} {currentPlayer?.CharacterInst == null}");
		}
	}

	private static async UniTask SwitchSelfCamera()
	{
		BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = 0f;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null && selfPlayerData.CharacterInst != null)
		{
			await selfPlayerData.CharacterInst.SwitchCamera();
		}
		BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)StaticGlobalData.GAME_CAMERA_SWITCH_TIME / 1000f;
	}

	private static async UniTask ShowAttrChange_ALL(int eventId, int performId, UpdateHeroAttrS2C model)
	{
		await SwitchSelfCamera();
		UniTask[] array = new UniTask[4];
		List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < changeAttrPlayerIds.Count; i++)
		{
			array[i] = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(changeAttrPlayerIds[i], performId, $"事件id—{eventId}表演");
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(array);
	}

	private static async UniTask ShowAttrChange_Single(EventInfoConfigure _config, UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			HeroGoldChangeS2C gold = model.EffectDatas[i].Gold;
			if (gold != null && gold.ChangeGold == 0)
			{
				HeroHpChangeS2C hp = model.EffectDatas[i].Hp;
				if (hp != null && hp.ChangeHp == 0)
				{
					continue;
				}
			}
			await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, _config.Perform1, $"事件id—{_config.Id}表演");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
