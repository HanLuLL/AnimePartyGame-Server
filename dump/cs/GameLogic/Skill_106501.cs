using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_106501 : Skill
{
	public Skill_106501()
	{
		skillId = 106501;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelf, "技能演出 释放");
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
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			RepeatedField<HeroAttrEffect> attrDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetAttrDataById(playerIds[i]);
			if (attrDataById == null)
			{
				continue;
			}
			int performId = 0;
			using (IEnumerator<HeroAttrEffect> enumerator = attrDataById.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					performId = ((enumerator.Current.Hp == null) ? skillConfig.PerformTargets[0] : skillConfig.PerformTargets[1]);
				}
			}
			await perform.PlayPlayerShow(playerIds[i], performId, "技能演出 - 物理主义者 目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
