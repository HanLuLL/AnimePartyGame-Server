using Tools;

namespace GameLogic;

public class Skill_11902 : Skill_119
{
	public Skill_11902()
	{
		InitConfig(11902);
	}

	public override float MoveEffectSpeed(long playerId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).buffContainer.Contain(1190102))
		{
			return 1f;
		}
		return 1.5f;
	}
}
