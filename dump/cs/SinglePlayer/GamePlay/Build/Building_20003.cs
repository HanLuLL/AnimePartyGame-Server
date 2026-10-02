using System.Linq;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20003 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int num = Game.GetSystem<BoardManager>().gameManager.DicePoints.Count((int i) => i == 6);
		if (num > 0)
		{
			ChangeOperateBonus(OperateType.ThrowDiceGold, GetConfigParam(ParameterType.ThrowDice, 1) * num);
		}
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice) * count, BuildingShowType.ThrowDice);
	}
}
