using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class Skill_300401 : Skill
{
	public Skill_300401()
	{
		skillId = 300401;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelf, "技能演出 - 鲤鱼主动 释放");
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}
}
