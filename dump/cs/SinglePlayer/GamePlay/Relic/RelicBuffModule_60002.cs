using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60002 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
		if (Game.GetModel<GameData>().MapData.GetLandTypeById(standLandId) != SinglePlayerLandType.Start)
		{
			return;
		}
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		if (buildingsByTags.Count == 0)
		{
			return;
		}
		foreach (BuildingBase item in buildingsByTags.RandomTake(safeByIndex))
		{
			item.ChangeOperateBonus(OperateType.ThrowDiceGold, info.BuffData.Config.RelicParam.GetSafeByIndex(1));
		}
	}
}
