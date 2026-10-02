using UI;

namespace GameLogic;

public class WeeklyLimitPropData
{
	public int itemId;

	public int Count;

	public int LimitCount => StaticGlobalData.PVETOKEN_WEEK_LIMIT;

	public ItemInfoConfigure itemInfo => itemId.GetItemInfoConfigure();
}
