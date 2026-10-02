using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_101911 : Skill
{
	public Skill_101911()
	{
		skillId = 101911;
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
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.EffectDatas[i].Hp != null)
			{
				await actionEffectShow.PlayPlayerShow(model.EffectDatas[i].PlayerId, skillConfig.PerformSelf, "技能演出 - 蛋糕王嘎呜被动 吃不到小怪掉10%的血");
				break;
			}
		}
	}
}
