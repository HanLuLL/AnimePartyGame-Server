using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_104201 : Skill
{
	public Skill_104201()
	{
		skillId = 104201;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
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
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, $"主动技{skillConfig.Id} 释放者演出");
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		List<long> mainTargets = new List<long>();
		List<long> otherTargets = new List<long>();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.PlayerId == model.PlayerId)
			{
				continue;
			}
			float a = effectData.Hp?.ChangeHp ?? 0;
			if (!mainTargets.Contains(effectData.PlayerId) && !otherTargets.Contains(effectData.PlayerId))
			{
				if (Mathf.Approximately(a, safeByIndex))
				{
					mainTargets.Add(effectData.PlayerId);
				}
				else
				{
					otherTargets.Add(effectData.PlayerId);
				}
			}
		}
		for (int i = 0; i < mainTargets.Count; i++)
		{
			await perform.PlayPlayerShow(mainTargets[i], skillConfig.PerformTargets[0], $"主动技{skillConfig.Id} 释放者演出");
		}
		for (int i = 0; i < otherTargets.Count; i++)
		{
			await perform.PlayPlayerShow(otherTargets[i], skillConfig.PerformTargets[1], $"主动技{skillConfig.Id} 释放者演出");
		}
	}
}
