using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60009 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		int safeByIndex2 = info.BuffData.Config.RelicParam.GetSafeByIndex(1);
		Game.GetModel<GameData>().heroProperty.UpdateCardCostReductionBonus(safeByIndex);
		Game.GetModel<GameData>().heroProperty.UpdateCardRevenueBonus(safeByIndex2);
	}
}
