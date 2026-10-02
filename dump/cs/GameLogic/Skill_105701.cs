using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_105701 : Skill
{
	public Skill_105701()
	{
		skillId = 105701;
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
		return true;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		if (model == null)
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "1057 主动技能释放");
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			await perform.PlayPlayerShow(effectData.PlayerId, skillConfig.PerformTarget, "1057技能释放 受影响玩家演出");
		}
	}
}
