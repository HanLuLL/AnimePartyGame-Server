using System.Linq;
using GameLogic;
using Tools;

namespace Core.Unit;

public class CharacterShowComponent_1047 : CharacterShowComponent
{
	public override void UpdateStopStatus()
	{
		if (Owner != null && SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas.Any((BattlePlayerData x) => x.characterType == CharacterType.Hero && x.buffContainer.Contain(10470101)))
		{
			Owner.ResetFromLandId(-1);
			Owner.ShowWalkDirections(null);
		}
	}
}
