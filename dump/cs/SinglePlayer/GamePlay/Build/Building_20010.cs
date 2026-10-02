using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20010 : BuildingBase
{
	protected override void OnCreate()
	{
		base.OnCreate();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.Pass), BuildingShowType.Pass);
		trigger = true;
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
		if (buildingsByTags.Count != 0)
		{
			buildingsByTags.RemoveAll((BuildingBase b) => !b.HasDiceEffect());
			if (buildingsByTags.Count != 0)
			{
				int index = Random.Range(0, buildingsByTags.Count);
				int configParam = GetConfigParam(ParameterType.Pass, 1);
				buildingsByTags[index].ChangeOperateBonus(OperateType.ThrowDiceGold, configParam);
			}
		}
	}
}
