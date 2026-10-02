using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_105311 : Skill
{
	public Skill_105311()
	{
		skillId = 105311;
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
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null || playerDataById.CharacterInst == null)
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		bool flag = false;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Buff != null)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, $"被动技{skillConfig.Id} 自身演出");
			_ = perform.isCancel;
		}
	}
}
