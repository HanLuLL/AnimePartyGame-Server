using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class Skill_11101 : Skill
{
	public Skill_11101()
	{
		skillId = 11101;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		List<LandBuffData> summonById = SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.GetSummonById(safeByIndex);
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return summonById.Count > 0;
		}
		return false;
	}
}
