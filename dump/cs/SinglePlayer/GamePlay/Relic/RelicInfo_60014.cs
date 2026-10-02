using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Relic;

[Preserve]
public class RelicInfo_60014 : RelicBuff
{
	public class RelicBuffModule_60014 : BaseRelicBuffModule
	{
		public override void Apply(RelicInfo info)
		{
			int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
			if (Game.GetModel<GameData>().MapData.GetLandTypeById(standLandId) == SinglePlayerLandType.Start)
			{
				SinglePlayerTagType tag = SinglePlayerTagType.Pirate;
				int count = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(tag).Count;
				info.ChangeGold(count);
			}
		}
	}

	public override void Initialize(int relicId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicId, out Config))
		{
			Id = relicId;
			OnPassLand = new RelicBuffModule_60014();
		}
	}
}
