using System.Collections.Generic;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20009 : BuildingBase
{
	protected override void OnDice(int count)
	{
		Game.GetModel<GameData>().heroProperty.UpdateDoubleDiceTime(1);
	}

	protected override void OnCreate()
	{
		Game.GetModel<GlobalSignal>().GenerateDicePoint.AddListener(OnGenerateDicePoint);
	}

	protected override void OnDestroy()
	{
		Game.GetModel<GlobalSignal>().GenerateDicePoint.RemoveListener(OnGenerateDicePoint);
	}

	private void OnGenerateDicePoint(List<int> values)
	{
		if (values.Count == 2)
		{
			this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice), BuildingShowType.ThrowDice);
		}
	}
}
