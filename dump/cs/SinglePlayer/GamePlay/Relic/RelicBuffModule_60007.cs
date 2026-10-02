using System.Collections.Generic;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60007 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		int safeByIndex2 = info.BuffData.Config.RelicParam.GetSafeByIndex(1);
		IReadOnlyList<int> dicePoints = Game.GetSystem<BoardManager>().gameManager.DicePoints;
		for (int i = 0; i < dicePoints.Count; i++)
		{
			if (dicePoints[i] == safeByIndex)
			{
				Game.GetModel<GameData>().heroProperty.SetForceFirstDicePoint(safeByIndex2);
				break;
			}
		}
	}
}
