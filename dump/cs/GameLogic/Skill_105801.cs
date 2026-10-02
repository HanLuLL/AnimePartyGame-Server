using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_105801 : Skill
{
	public Skill_105801()
	{
		skillId = 105801;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await UniTask.CompletedTask;
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
		await curPlayerData.CharacterInst.SwitchCamera();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "1058 主动技能释放");
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Hp != null && effectData.Hp.ChangeHp < 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(effectData.PlayerId)?.DisposeMonster_Hide();
			}
		}
	}
}
