using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60012 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		int safeByIndex2 = info.BuffData.Config.RelicParam.GetSafeByIndex(1);
		info.BuffData.ChargeCount++;
		if (info.BuffData.ChargeCount % safeByIndex == 0)
		{
			info.BuffData.ChargeCount = 0;
			List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
			if (buildingsByTags.Count != 0)
			{
				int index = UnityEngine.Random.Range(0, buildingsByTags.Count);
				buildingsByTags[index].ChangeOperateBonus(OperateType.ThrowDiceGold, safeByIndex2);
			}
		}
	}
}
