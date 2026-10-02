using System.Collections.Generic;
using Core.Unit;
using Tools;

namespace GameLogic;

public class Skill_30601 : Skill_306
{
	public Skill_30601()
	{
		InitConfig(30601);
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (!curPlayerData.CharacterInst.activeSkillVail)
		{
			return false;
		}
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		bool result = false;
		foreach (BattlePlayerData item in playerDatas)
		{
			if (item.Property.HP.Value != 0 && curPlayerData.player.Id != item.player.Id && item.player.characterType == CharacterType.Hero && !(curPlayerData.CharacterInst == null) && !(item.CharacterInst == null) && SimpleSingletonProvider<LandManager>.inst.CheckDistance(curPlayerData.CharacterInst.standLand.Id, item.CharacterInst.standLand.Id, safeByIndex, 0))
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
