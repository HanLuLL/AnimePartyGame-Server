using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60001 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		int safeByIndex2 = info.BuffData.Config.RelicParam.GetSafeByIndex(1);
		info.BuffData.ChargeCount++;
		if (info.BuffData.ChargeCount % safeByIndex == 0)
		{
			info.BuffData.ChargeCount = 0;
			Game.GetSystem<BoardManager>().cardManager.AddCardToBag(safeByIndex2);
		}
	}
}
