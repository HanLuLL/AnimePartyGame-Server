using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;

namespace SinglePlayer.GamePlay.Relic;

public class RelicInfo_60017 : RelicBuff
{
	public class RelicBuffModule_60017 : BaseRelicBuffModule
	{
		public override void Apply(RelicInfo info)
		{
			int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
			if (Game.GetModel<GameData>().MapData.GetLandTypeById(standLandId) != SinglePlayerLandType.Start)
			{
				return;
			}
			SinglePlayerTagType tag = SinglePlayerTagType.Pirate;
			List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(tag);
			int relicParam = info.GetRelicParam(0);
			foreach (BuildingBase item in buildingsByTags)
			{
				item.ChangeOperateBonus(OperateType.PassGold, relicParam);
				item.ChangeOperateBonus(OperateType.StayGold, relicParam);
			}
		}
	}

	public override void Initialize(int relicId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicId, out Config))
		{
			Id = relicId;
			OnPassLand = new RelicBuffModule_60017();
		}
	}
}
