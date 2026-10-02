using SinglePlayer.GamePlay.Map;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60011 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
		Land landById = Game.GetModel<GameData>().MapData.GetLandById(standLandId);
		SinglePlayerLandType landType = landById.LandType;
		if (landType == SinglePlayerLandType.Gold || landType == SinglePlayerLandType.Card)
		{
			BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(landById.BuildingFoundationId);
			if (!(buildingFoundationById == null) && Game.GetModel<GameData>().BuildingData.TryGetByFoundationId(buildingFoundationById.Id, out var building))
			{
				int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
				building.AddExp(safeByIndex);
			}
		}
	}
}
