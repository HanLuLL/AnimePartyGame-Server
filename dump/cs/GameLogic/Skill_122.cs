using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_122 : Skill
{
	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "122主动技能 自己表现");
		if (!perform.isCancel)
		{
			await PlayAttackPerform(model);
		}
	}

	public async UniTask PlayAttackPerform(UpdateHeroAttrS2C model, bool forceShort = false)
	{
		Dictionary<long, List<HeroAttrEffect>> dictionary = new Dictionary<long, List<HeroAttrEffect>>();
		List<HeroAttrEffect> _attackAttrs = new List<HeroAttrEffect>();
		List<HeroAttrEffect> _otherAttrs = new List<HeroAttrEffect>();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData == null || effectData.PlayerId == model.PlayerId)
			{
				continue;
			}
			if (effectData.Hp == null)
			{
				_otherAttrs.Add(effectData);
				continue;
			}
			if (!dictionary.ContainsKey(effectData.PlayerId))
			{
				dictionary[effectData.PlayerId] = new List<HeroAttrEffect>();
			}
			dictionary[effectData.PlayerId].Add(effectData);
		}
		foreach (List<HeroAttrEffect> value in dictionary.Values)
		{
			_attackAttrs.AddRange(value);
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"无法通过属性变更数据model.PlayerId：{model.PlayerId}， 获取对应玩家的数据");
			return;
		}
		int shortHitId = skillConfig.PerformTargets.GetSafeByIndex(0);
		int finalHitId = skillConfig.PerformTargets.GetSafeByIndex(1);
		SkinStandingPaintingConfigureItem standingPainting = playerDataById.player.standingPainting;
		if (standingPainting != null)
		{
			switch (standingPainting.ItemID)
			{
			case 100122003:
				shortHitId = 12204;
				finalHitId = 12205;
				break;
			case 100122004:
				shortHitId = 12206;
				finalHitId = 12207;
				break;
			}
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < _attackAttrs.Count; i++)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(_attackAttrs[i].PlayerId);
			int performId = ((!forceShort && _attackAttrs.Count == i + 1) ? finalHitId : shortHitId);
			await perform.PlayPlayerShow(_attackAttrs[i].PlayerId, performId, "122主动技能 目标表现", ignoreDuration: false, _attackAttrs[i]);
			if (perform.isCancel)
			{
				return;
			}
		}
		for (int i = 0; i < _otherAttrs.Count; i++)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(_otherAttrs[i].PlayerId);
			BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_otherAttrs[i].PlayerId);
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateBaseAttr(playerDataById2, _otherAttrs[i]);
		}
	}
}
