using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60013 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
		if (Game.GetModel<GameData>().MapData.GetLandTypeById(standLandId) == SinglePlayerLandType.Start)
		{
			List<BuildingBase> randomBuildings = Game.GetSystem<BoardManager>().buildingManager.GetRandomBuildings(1);
			if (randomBuildings.Count > 0)
			{
				int id = randomBuildings[0].Card.CardConfigure.Id;
				Game.GetSystem<BoardManager>().cardManager.AddCardToBag(id);
			}
		}
	}
}
