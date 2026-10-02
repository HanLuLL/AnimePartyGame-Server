using System.Collections.Generic;
using SinglePlayer.GamePlay.Card;

namespace SinglePlayer.GamePlay.Build;

public class Building_20018 : BuildingBase
{
	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		trigger = true;
		int configParam = GetConfigParam(ParameterType.Stay, 0);
		List<BuildingBase> buildingsByConfigId = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByConfigId(configParam);
		foreach (BuildingBase item in buildingsByConfigId)
		{
			item.AddForceExecuteStayQueue();
		}
		int configParam2 = GetConfigParam(ParameterType.Stay, 1);
		foreach (BuildingBase item2 in buildingsByConfigId)
		{
			SinglePlayer.GamePlay.Card.Card card = item2?.Card;
			if (card != null && Game.GetSystem<BoardManager>().cardManager.TryRemoveCardOnTheTable(card.UID, isDelayRemove: true))
			{
				ChangeOperateBonus(OperateType.PassGold, configParam2);
			}
		}
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		trigger = true;
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Pass);
		this.ChangeGold(goldMultiValue, BuildingShowType.Pass);
	}
}
