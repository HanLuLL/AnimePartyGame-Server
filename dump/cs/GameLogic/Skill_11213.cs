using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_11213 : Skill
{
	public Skill_11213()
	{
		skillId = 11213;
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
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "史莱姆突破被动技能触发", ignoreDuration: true);
	}
}
