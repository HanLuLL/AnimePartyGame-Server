using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60010 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
		int num = Mathf.Min(info.BuffData.Config.RelicParam.GetSafeByIndex(0), buildingsByTags.Count);
		for (int i = 0; i < num; i++)
		{
			int index = UnityEngine.Random.Range(0, buildingsByTags.Count);
			buildingsByTags[index].ChangeOperateBonus(OperateType.ThrowDiceGold, 1);
			buildingsByTags.RemoveAt(index);
		}
	}
}
