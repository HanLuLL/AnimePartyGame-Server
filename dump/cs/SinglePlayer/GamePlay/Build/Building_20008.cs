using System.Collections.Generic;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20008 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		List<BuildingBase> neighborBuildings = GetNeighborBuildings();
		neighborBuildings.RemoveAll((BuildingBase b) => !b.HasDiceEffect());
		neighborBuildings.RemoveAll((BuildingBase b) => !b.Card.CardConfigure.CardTag.Contains(SinglePlayerTagType.Prosperity));
		foreach (BuildingBase item in neighborBuildings)
		{
			item.AddForcedExecuteDiceQueue();
		}
	}

	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
		buildingsByTags.RemoveAll((BuildingBase b) => !b.HasDiceEffect());
		int configParam = GetConfigParam(ParameterType.Stay, 0);
		foreach (BuildingBase item in buildingsByTags)
		{
			trigger = true;
			item.ChangeOperateBonus(OperateType.ThrowDiceGold, configParam);
		}
	}
}
