using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_101001 : Skill
{
	public Skill_101001()
	{
		skillId = 101001;
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
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "技能演出 - 红剑攻击");
		if (perform.isCancel)
		{
			return;
		}
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], skillConfig.PerformTarget, "技能演出 - 红剑攻击 对目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
