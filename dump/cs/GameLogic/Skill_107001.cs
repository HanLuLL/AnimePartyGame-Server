using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_107001 : Skill
{
	public Skill_107001()
	{
		skillId = 107001;
		skillConfig = 107001.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (!(curPlayerData?.CharacterInst == null))
		{
			await curPlayerData.CharacterInst.SwitchCamera();
		}
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (curPlayerData?.CharacterInst != null)
		{
			return curPlayerData.CharacterInst.activeSkillVail;
		}
		return false;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "技能演出 - 妙手空空 释放");
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], skillConfig.PerformTarget, "技能演出 - 妙手空空 目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
