using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Map;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60006 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
		Land landById = Game.GetModel<GameData>().MapData.GetLandById(standLandId);
		if (Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(landById.BuildingFoundationId, out var building) && building.Card.CardConfigure.CardTag.Contains(SinglePlayerTagType.Prosperity))
		{
			int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
			Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
			{
				Source = (type: AttributeChangeSource.Relic, id: info.BuffData.Id),
				ChangeGold = safeByIndex,
				ConfigId = info.BuffData.Id
			}).Forget();
		}
	}
}
