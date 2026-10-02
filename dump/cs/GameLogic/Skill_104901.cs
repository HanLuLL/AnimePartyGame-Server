using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class Skill_104901 : Skill
{
	public Skill_104901()
	{
		skillId = 104901;
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
}
