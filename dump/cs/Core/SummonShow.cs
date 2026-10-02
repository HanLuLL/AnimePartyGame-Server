using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace Core;

public static class SummonShow
{
	public static async UniTask CommonSummonShow(int summonId, UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		SummonInfoConfigure _configure = summonId.GetSummonDataConfigure();
		if (_configure.MapcardID != 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).RefreshMapEventData(_configure.MapcardID);
		}
		List<long> _SubPlayers = new List<long>(4);
		List<long> _AddPlayers = new List<long>(4);
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			HeroGoldChangeS2C gold = model.EffectDatas[i].Gold;
			if (gold == null || gold.ChangeGold >= 0)
			{
				HeroHpChangeS2C hp = model.EffectDatas[i].Hp;
				if (hp == null || hp.ChangeHp >= 0)
				{
					goto IL_01c4;
				}
			}
			if (!_SubPlayers.Contains(model.EffectDatas[i].PlayerId))
			{
				_SubPlayers.Add(model.EffectDatas[i].PlayerId);
			}
			goto IL_01c4;
			IL_01c4:
			HeroGoldChangeS2C gold2 = model.EffectDatas[i].Gold;
			if (gold2 == null || gold2.ChangeGold <= 0)
			{
				HeroHpChangeS2C hp2 = model.EffectDatas[i].Hp;
				if (hp2 == null || hp2.ChangeHp <= 0)
				{
					continue;
				}
			}
			if (!_AddPlayers.Contains(model.EffectDatas[i].PlayerId))
			{
				_AddPlayers.Add(model.EffectDatas[i].PlayerId);
			}
		}
		landBuffData.HideSummon();
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int j = 0; j < _SubPlayers.Count; j++)
		{
			await perform.PlayPlayerShow(_SubPlayers[j], _configure.PerformDebuff, "触发召唤物");
			if (perform.isCancel)
			{
				return;
			}
		}
		for (int j = 0; j < _AddPlayers.Count; j++)
		{
			await perform.PlayPlayerShow(_AddPlayers[j], _configure.PerformBuff, "召唤物收益");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	public static async UniTask SummonTriggerShow_2006(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.HideBomb(model.PlayerId);
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.ActiveBomb(model.PlayerId);
	}

	public static async UniTask SummonTriggerShow_2007(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		SummonInfoConfigure _configure = 2007.GetSummonDataConfigure();
		if (_configure.MapcardID != 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).RefreshMapEventData(_configure.MapcardID);
		}
		landBuffData.HideSummon();
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, _configure.PerformDebuff, "事件传送开始");
		if (perform.isCancel)
		{
			return;
		}
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.EffectDatas[i].Place != null && playerData.CharacterInst != null)
			{
				playerData.CharacterInst.SendCharacter(model.EffectDatas[i].Place.Place.NodeId, model.EffectDatas[i].Place.Place.FrontNodeIds);
				break;
			}
		}
		await perform.PlayPlayerShow(model.PlayerId, _configure.PerformBuff, "事件传送结束");
		if (!perform.isCancel)
		{
			BattlePlayerData currentPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer();
			if (currentPlayer.CharacterInst != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(currentPlayer.CharacterInst, willMove: false);
			}
			LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[13];
			await perform.PlayPlayerShow(model.PlayerId, landInfoConfigure.Perform1, $"地图格{model.Cause.Id}，住院格");
		}
	}

	public static async UniTask SummonTriggerShow_2500(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && playerDataById.CharacterInst != null && playerDataById.player.characterConfig.Id == 1033)
		{
			int num = 103311.GetSkillConfigure()?.PerformSelf ?? 0;
			if (num == 0)
			{
				return;
			}
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, num, "科学怪人复活嘎呜");
		}
		await UniTask.CompletedTask;
	}

	public static async UniTask SummonTriggerShow_2501(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && playerDataById.CharacterInst != null && playerDataById.player.characterConfig.Id == 1068)
		{
			int num = 106811.GetSkillConfigure()?.PerformSelf ?? 0;
			if (num == 0)
			{
				return;
			}
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, num, "看守复活");
		}
		await UniTask.CompletedTask;
	}

	public static async UniTask SummonTriggerShow_11111(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && playerDataById.CharacterInst != null)
		{
			int performDebuff = landBuffData.Summon.trapConfig.PerformDebuff;
			Transform effectTransform = landBuffData.Summon.GetEffectTransform();
			SimpleSingletonProvider<EffectManager>.inst.PlayById(11102, effectTransform.position, effectTransform.rotation).Forget();
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, performDebuff, "猫影子攻击");
		}
	}

	public static async UniTask SummonTriggerShow_2600(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && playerDataById.CharacterInst != null)
		{
			int performBuff = landBuffData.Summon.trapConfig.PerformBuff;
			landBuffData.HideSummon();
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, performBuff, "拾起凤凰蛋");
		}
	}

	public static async UniTask SummonTriggerShow_11211(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && playerDataById.CharacterInst != null)
		{
			playerDataById.CharacterInst.characterAnimator.Move(walk: false);
			int performId = landBuffData.Summon?.trapConfig?.PerformBuff ?? 11204;
			landBuffData.HideSummon();
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, performId, "拾起迷你史莱姆");
		}
	}

	public static async UniTask SummonTriggerShow_12911(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		await SummonTriggerShow_1291X(model, landBuffData, isPve: false);
	}

	public static async UniTask SummonTriggerShow_12912(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		await SummonTriggerShow_1291X(model, landBuffData, isPve: true);
	}

	public static async UniTask SummonTriggerShow_1291X(UpdateHeroAttrS2C model, LandBuffData landBuffData, bool isPve)
	{
		if (landBuffData.Summon == null)
		{
			return;
		}
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		int performId = landBuffData.Summon.trapConfig.CommonPerforms[1];
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (isPve)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(effectData.PlayerId);
				if (playerDataById == null || playerDataById.CharacterInst == null)
				{
					continue;
				}
				performId = ((playerDataById.Property.ExtraMinusMovePoint.Value > 0) ? landBuffData.Summon.trapConfig.CommonPerforms[2] : landBuffData.Summon.trapConfig.CommonPerforms[1]);
			}
			actionEffectShow.PlayPlayerShow(effectData.PlayerId, performId, "被触手召唤物攻击 演出").Forget();
		}
		if (landBuffData.Summon.GetSummonShowIgnoreDuration())
		{
			landBuffData.Summon.summonInstance.summonShow.PlaySummonShow(landBuffData.Summon, landBuffData.Summon.trapConfig.CommonPerforms[0], "触手召唤物攻击 演出", ignoreDuration: true).Forget();
		}
		else
		{
			await landBuffData.Summon.summonInstance.summonShow.PlaySummonShow(landBuffData.Summon, landBuffData.Summon.trapConfig.CommonPerforms[0], "触手召唤物攻击 演出");
		}
	}

	public static async UniTask SummonTriggerShow_12921(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		if (landBuffData.Summon == null)
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		SummonInfoConfigure summonConfig = landBuffData.Summon.trapConfig;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Buff != null)
			{
				int num = Math.Clamp(summonConfig.BuffIds.IndexOf(effectData.Buff.Buff.BuffId), 0, summonConfig.DebuffPerforms.Count - 1);
				UniTask task = perform.PlayPlayerShow(effectData.PlayerId, summonConfig.DebuffPerforms[num], "赛克斯区域召唤物阻挡 演出");
				if (num == 0)
				{
					await task;
				}
				else
				{
					task.Forget();
				}
			}
		}
		await UniTask.CompletedTask;
	}

	public static async UniTask SummonTriggerShow_12922(UpdateHeroAttrS2C model, LandBuffData landBuffData)
	{
		await SummonTriggerShow_12921(model, landBuffData);
	}
}
