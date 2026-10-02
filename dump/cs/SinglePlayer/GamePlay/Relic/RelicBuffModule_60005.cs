using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60005 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		List<BuildingBase> randomBuildings = Game.GetSystem<BoardManager>().buildingManager.GetRandomBuildings(1);
		for (int i = 0; i < randomBuildings.Count; i++)
		{
			randomBuildings[i].AddExp(safeByIndex);
		}
	}
}
