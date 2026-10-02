using Tools;

namespace GameLogic;

public class Skill_11901 : Skill_119
{
	public Skill_11901()
	{
		InitConfig(11901);
	}

	public override float MoveEffectSpeed(long playerId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).buffContainer.Contain(1190101))
		{
			return 1f;
		}
		return 1.5f;
	}
}
