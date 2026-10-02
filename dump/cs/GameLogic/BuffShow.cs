using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public static class BuffShow
{
	private static HashSet<long> _playerIds = new HashSet<long>();

	public static async UniTask AttrChangeShow(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure buffConfigure = _buff.BuffId.GetBuffConfigure();
		int performId = buffConfigure.PerformStart;
		if (performId == 0)
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, performId, "触发buff");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	public static async UniTask BuffShow_1140101(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure _config = _buff.BuffId.GetBuffConfigure();
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.EffectDatas[i].PlayerId);
			int performId = ((playerDataById.player.Hero.HeroId == 114) ? _config.PerformAwake : _config.PerformDestroy);
			await perform.PlayPlayerShow(playerDataById.player.Id, performId, "114技能触发");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	public static async UniTask BuffShow_1141101(UpdateHeroAttrS2C model, Buff _buff)
	{
		await BuffShow_1140101(model, _buff);
	}

	public static async UniTask BuffShow_1140102(UpdateHeroAttrS2C model, Buff _buff)
	{
		await BuffShow_1140101(model, _buff);
	}

	public static async UniTask BuffShow_10470101(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure _config = _buff.BuffId.GetBuffConfigure();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			HeroHpChangeS2C hp = effectData.Hp;
			if (hp != null && hp.ChangeHp > 0)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(effectData.PlayerId, _config.PerformDestroy, "触发思念buff -- 思念消散回血");
			}
		}
	}

	public static async UniTask BuffShow_10490101(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure _config = _buff.BuffId.GetBuffConfigure();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			HeroHpChangeS2C hp = effectData.Hp;
			if (hp != null && hp.ChangeHp < 0)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(effectData.PlayerId, _config.PerformEnd, "触发火焰buff -- buff触发伤害");
			}
		}
	}

	public static async UniTask BuffShow_1221201(UpdateHeroAttrS2C model, Buff _buff)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.CharacterInst.skill is Skill_122 skill_)
		{
			await skill_.PlayAttackPerform(model, forceShort: true);
		}
	}

	public static async UniTask BuffShow_5007101(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure buffConfigure = _buff.BuffId.GetBuffConfigure();
		_playerIds.Clear();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (!_playerIds.Contains(effectData.PlayerId) && effectData.UseCardNum == null && (effectData.Buff == null || effectData.Buff.Buff.BuffId != 5007101))
			{
				_playerIds.Add(effectData.PlayerId);
			}
		}
		if (_playerIds.Count > 0)
		{
			ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
			int performStart = buffConfigure.PerformStart;
			if (StaticConfigure.Perform.InfoDict.TryGetValue(performStart, out var value))
			{
				foreach (long playerId in _playerIds)
				{
					BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
					if (playerDataById != null && playerDataById.CharacterInst != null)
					{
						actionEffectShow.PlayPlayerShow(playerId, performStart, "新春红包 新春祝福buff触发", ignoreDuration: true).Forget();
					}
				}
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value.TotalTime);
			}
		}
		_playerIds.Clear();
		await UniTask.CompletedTask;
	}

	public static async UniTask BuffShow_6000101(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure _config = _buff.BuffId.GetBuffConfigure();
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Hp != null)
			{
				await perform.PlayPlayerShow(effectData.PlayerId, _config.PerformAwake, "精神混乱 6000101buff触发", ignoreDuration: true);
			}
		}
		await UniTask.CompletedTask;
	}

	public static async UniTask BuffShow_6000802(UpdateHeroAttrS2C model, Buff _buff)
	{
		int performAwake = _buff.BuffId.GetBuffConfigure().PerformAwake;
		if (StaticConfigure.Perform.InfoDict.TryGetValue(performAwake, out var value))
		{
			foreach (HeroAttrEffect effectData in model.EffectDatas)
			{
				if (effectData.Buff != null && effectData.Buff.Buff != null && !string.IsNullOrEmpty(effectData.Buff.Buff.RelationId))
				{
					int num = int.Parse(effectData.Buff.Buff.RelationId);
					SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(num, performAwake, "神秘宝藏 6000802buff触发", ignoreDuration: true).Forget();
				}
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value.TotalTime);
		}
		await UniTask.CompletedTask;
	}

	public static async UniTask BuffShow_5007801(UpdateHeroAttrS2C model, Buff _buff)
	{
		BuffInfoConfigure buffConfigure = _buff.BuffId.GetBuffConfigure();
		int performId = buffConfigure.PerformStart;
		if (performId == 0)
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Hp != null)
			{
				await perform.PlayPlayerShow(effectData.PlayerId, performId, "流星筹码 触发buff");
				if (perform.isCancel)
				{
					return;
				}
			}
		}
	}

	public static async UniTask BuffShow_5007901(UpdateHeroAttrS2C model, Buff _buff)
	{
		await BuffShow_5007801(model, _buff);
	}

	public static async UniTask BuffShow_5008701(UpdateHeroAttrS2C model, Buff _buff)
	{
		int performStart = _buff.BuffId.GetBuffConfigure().PerformStart;
		if (performStart == 0)
		{
			return;
		}
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		bool flag = false;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Hp != null)
			{
				actionEffectShow.PlayPlayerShow(effectData.PlayerId, performStart, "雷电法杖筹码 触发buff").Forget();
				flag = true;
				if (actionEffectShow.isCancel)
				{
					return;
				}
			}
		}
		if (flag && StaticConfigure.Perform.InfoDict.TryGetValue(performStart, out var value))
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value.TotalTime);
		}
	}

	public static async UniTask BuffShow_5009101(UpdateHeroAttrS2C model, Buff _buff)
	{
		int performStart = _buff.BuffId.GetBuffConfigure().PerformStart;
		bool flag = false;
		if (performStart == 0)
		{
			return;
		}
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Hp != null)
			{
				actionEffectShow.PlayPlayerShow(effectData.PlayerId, performStart, "电磁炮筹码 触发buff").Forget();
				flag = true;
				if (actionEffectShow.isCancel)
				{
					return;
				}
			}
		}
		if (flag && StaticConfigure.Perform.InfoDict.TryGetValue(performStart, out var value))
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value.TotalTime);
		}
	}
}
