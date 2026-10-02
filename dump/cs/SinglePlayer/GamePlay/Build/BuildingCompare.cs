using System.Collections.Generic;
using SinglePlayer.GamePlay.Map;

namespace SinglePlayer.GamePlay.Build;

public class BuildingCompare : IComparer<BuildingExecutionItem>
{
	public int Compare(BuildingExecutionItem x, BuildingExecutionItem y)
	{
		if (y?.Building == null)
		{
			return -1;
		}
		if (x?.Building == null)
		{
			return 1;
		}
		int num = y.Building.Card.CardConfigure.TriggerPriority.CompareTo(x.Building.Card.CardConfigure.TriggerPriority);
		if (num == 0)
		{
			GameData model = Game.GetModel<GameData>();
			Land landByBuildingFoundationId = model.MapData.GetLandByBuildingFoundationId(x.Building.BuildingFoundationId);
			Land landByBuildingFoundationId2 = model.MapData.GetLandByBuildingFoundationId(y.Building.BuildingFoundationId);
			if (landByBuildingFoundationId == null || landByBuildingFoundationId2 == null)
			{
				return 0;
			}
			int value = model.MapData.Distance(model.heroProperty.StandLandId, landByBuildingFoundationId.Id);
			return model.MapData.Distance(model.heroProperty.StandLandId, landByBuildingFoundationId2.Id).CompareTo(value);
		}
		return num;
	}
}
