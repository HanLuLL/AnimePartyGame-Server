using System;
using System.Collections.Generic;

namespace UI;

public class AutoTransformRewardsData : IComparable<AutoTransformRewardsData>
{
	public KeyValuePair<int, int> Item;

	public KeyValuePair<int, int> GainItem = new KeyValuePair<int, int>(0, 0);

	public AutoTransformRewardsData(int itemId, int count)
	{
		Item = new KeyValuePair<int, int>(itemId, count);
	}

	public int CompareTo(AutoTransformRewardsData other)
	{
		if (other == null)
		{
			return 1;
		}
		ItemInfoConfigure itemInfoConfigure = Item.Key.GetItemInfoConfigure();
		ItemInfoConfigure itemInfoConfigure2 = other.Item.Key.GetItemInfoConfigure();
		int num = ((int)(itemInfoConfigure2?.QualityType ?? QualityType.None)).CompareTo((int)(itemInfoConfigure?.QualityType ?? QualityType.None));
		if (num != 0)
		{
			return num;
		}
		return (itemInfoConfigure?.Id ?? 0).CompareTo(itemInfoConfigure2?.Id ?? 0);
	}
}
