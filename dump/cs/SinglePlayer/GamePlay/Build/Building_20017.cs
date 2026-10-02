using System.Collections.Generic;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class Building_20017 : BuildingBase
{
	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		trigger = true;
		int configParam = GetConfigParam(ParameterType.Pass, 0);
		int configParam2 = GetConfigParam(ParameterType.Pass, 1);
		List<BuildingBase> buildingsByConfigId = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByConfigId(configParam);
		if (buildingsByConfigId.Count > 0)
		{
			for (int i = 0; i < configParam2; i++)
			{
				int index = Random.Range(0, buildingsByConfigId.Count);
				buildingsByConfigId[index]?.AddForceExecuteStayQueue();
			}
		}
	}

	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		trigger = true;
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Stay);
		this.ChangeGold(goldMultiValue, BuildingShowType.Stay);
		int configParam = GetConfigParam(ParameterType.Stay, 1);
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Dice);
		buildingsByTags = buildingsByTags.FindAll((BuildingBase x) => x.HasDiceEffect());
		if (buildingsByTags.Count > 0)
		{
			for (int num = 0; num < configParam; num++)
			{
				int index = Random.Range(0, buildingsByTags.Count);
				buildingsByTags[index]?.AddForcedExecuteDiceQueue();
			}
		}
	}
}
