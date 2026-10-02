using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_106101 : Skill
{
	public Skill_106101()
	{
		skillId = 106101;
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
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "技能演出 - 黄牌警告 释放");
		using IEnumerator<HeroAttrEffect> enumerator = model.EffectDatas.GetEnumerator();
		if (enumerator.MoveNext())
		{
			HeroAttrEffect current = enumerator.Current;
			await perform.PlayPlayerShow(current.PlayerId, skillConfig.PerformTarget, "技能演出 - 黄牌警告 目标玩家");
		}
	}
}
