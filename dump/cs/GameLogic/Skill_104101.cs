using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_104101 : Skill
{
	public Skill_104101()
	{
		skillId = 104101;
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
		List<long> defbuffTargets = new List<long>();
		List<long> AddTargets = new List<long>();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.PlayerId != model.PlayerId && !defbuffTargets.Contains(effectData.PlayerId) && !AddTargets.Contains(effectData.PlayerId))
			{
				if (effectData.Def != null)
				{
					defbuffTargets.Add(effectData.PlayerId);
				}
				else
				{
					AddTargets.Add(effectData.PlayerId);
				}
			}
		}
		for (int i = 0; i < defbuffTargets.Count; i++)
		{
			await perform.PlayPlayerShow(defbuffTargets[i], skillConfig.PerformTargets[0], $"主动技{skillConfig.Id} 释放者演出");
		}
		for (int i = 0; i < AddTargets.Count; i++)
		{
			await perform.PlayPlayerShow(AddTargets[i], skillConfig.PerformTargets[1], $"主动技{skillConfig.Id} 释放者演出");
		}
	}
}
