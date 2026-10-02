using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_11314 : Skill
{
	public Skill_11314()
	{
		skillId = 11314;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override UniTask SkillTrigger(long _playerId)
	{
		return UniTask.CompletedTask;
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
		if (!model.EffectDatas.Any((HeroAttrEffect x) => x.Hp != null))
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			perform.PlayPlayerShow(effectData.PlayerId, skillConfig.PerformTargets[1], "113潜突 被动技能 11314 触发").Forget();
		}
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformTargets[0], "113潜突 被动技能 11314 触发");
		if (!perform.isCancel)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
		}
	}
}
