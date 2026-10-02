using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20002 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice) * count, BuildingShowType.ThrowDice);
	}

	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		List<BuildingBase> neighborBuildings = GetNeighborBuildings();
		neighborBuildings.RemoveAll((BuildingBase b) => !b.HasDiceEffect());
		int configParam = GetConfigParam(ParameterType.Stay, 0);
		Debug.Log($"throwDiceGoldBounds:{configParam}");
		foreach (BuildingBase item in neighborBuildings)
		{
			if (item.Card.CardConfigure.CardTag.Contains(SinglePlayerTagType.Prosperity))
			{
				trigger = true;
				item.ChangeOperateBonus(OperateType.ThrowDiceGold, configParam);
			}
		}
	}
}
