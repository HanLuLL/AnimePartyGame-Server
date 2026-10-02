using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20012 : BuildingBase
{
	protected override bool CheckDicePoint(List<int> values)
	{
		return values.Count == 2;
	}

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		IReadOnlyList<int> dicePoints = Game.GetSystem<BoardManager>().gameManager.DicePoints;
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice, dicePoints.Sum()), BuildingShowType.ThrowDice);
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		Game.GetModel<GameData>().heroProperty.UpdateDoubleDiceTime(1);
		trigger = true;
	}
}
