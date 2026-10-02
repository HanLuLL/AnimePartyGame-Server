using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_101011 : Skill
{
	public Skill_101011()
	{
		skillId = 101011;
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
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.EffectDatas[i].Hp != null)
			{
				await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, skillConfig.PerformTarget, "技能演出 - 红剑被动 对目标玩家");
				if (perform.isCancel)
				{
					break;
				}
			}
		}
	}
}
