using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60008 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		int safeByIndex = info.BuffData.Config.RelicParam.GetSafeByIndex(0);
		info.BuffData.ChargeCount++;
		if (info.BuffData.ChargeCount % safeByIndex == 0)
		{
			info.BuffData.ChargeCount = 0;
			Game.GetModel<GameData>().heroProperty.UpdateDoubleDiceTime(1);
		}
	}
}
