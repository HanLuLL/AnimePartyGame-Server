namespace GameLogic.Data;

public class ActivityPassItemRewardData
{
	public ActivityPassItemData PassItemData;

	public ItemInfoConfigure ItemConfig;

	public int rewardCount;

	public ActivityPassItemRewardData(ItemInfoConfigure _itemConfig, int _rewardCount, ActivityPassItemData _data)
	{
		PassItemData = _data;
		ItemConfig = _itemConfig;
		rewardCount = _rewardCount;
	}
}
