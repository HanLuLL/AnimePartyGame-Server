using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_12201 : Skill_122
{
	public Skill_12201()
	{
		skillId = 12201;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		int cardCount = curPlayerData.cardContainer.CardCount;
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return cardCount >= skillConfig.Params[1];
		}
		return false;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "122主动技能 自己表现");
		if (perform.isCancel)
		{
			return;
		}
		Dictionary<long, List<HeroAttrEffect>> dictionary = new Dictionary<long, List<HeroAttrEffect>>();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData != null && effectData.PlayerId != model.PlayerId)
			{
				if (!dictionary.ContainsKey(effectData.PlayerId))
				{
					dictionary[effectData.PlayerId] = new List<HeroAttrEffect>();
				}
				dictionary[effectData.PlayerId].Add(effectData);
			}
		}
		List<HeroAttrEffect> _list = new List<HeroAttrEffect>();
		foreach (List<HeroAttrEffect> value in dictionary.Values)
		{
			_list.AddRange(value);
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"无法通过属性变更数据model.PlayerId：{model.PlayerId}， 获取对应玩家的数据");
			return;
		}
		int shortHitId = skillConfig.PerformTargets[0];
		int finalHitId = skillConfig.PerformTargets[1];
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
		for (int i = 0; i < _list.Count; i++)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(_list[i].PlayerId);
			int performId = ((_list.Count != i + 1) ? shortHitId : finalHitId);
			await perform.PlayPlayerShow(_list[i].PlayerId, performId, "122主动技能 目标表现", ignoreDuration: false, _list[i]);
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
