using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_105501 : Skill
{
	public Skill_105501()
	{
		skillId = 105501;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelf, "1055 技能释放 指挥舞狮噶唔召唤");
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		return true;
	}

	public override UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		return UniTask.CompletedTask;
	}
}
