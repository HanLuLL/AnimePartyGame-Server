using System.Collections.Generic;
using Google.Protobuf.Collections;
using UI;

namespace GameLogic;

public class GachaPoolInfo
{
	public readonly GachaPoolConfigure poolData;

	public Dictionary<int, List<GachaGroupItemData>> itemsDict = new Dictionary<int, List<GachaGroupItemData>>();

	public GachaPoolInfo(GachaPoolConfigure _poolData)
	{
		poolData = _poolData;
		if (poolData.CombDefault == 0)
		{
			return;
		}
		RepeatedField<GachaCombConfigureItem> gachaCombConfigureItems = poolData.CombDefault.GetGachaCombConfigure().GachaCombConfigureItems;
		int num = 0;
		foreach (GachaCombConfigureItem item2 in gachaCombConfigureItems)
		{
			num += item2.GroupWeight;
		}
		for (int i = 0; i < gachaCombConfigureItems.Count; i++)
		{
			float num2 = (float)gachaCombConfigureItems[i].GroupWeight * 1f / (float)num;
			GachaGroupConfigure gachaGroupConfigure = gachaCombConfigureItems[i].GroupID.GetGachaGroupConfigure();
			if (!itemsDict.TryGetValue((int)gachaGroupConfigure.GachaItemSortType, out var value))
			{
				value = new List<GachaGroupItemData>();
				itemsDict.TryAdd((int)gachaGroupConfigure.GachaItemSortType, value);
			}
			int num3 = 0;
			foreach (GachaGroupConfigureItem gachaGroupConfigureItem in gachaGroupConfigure.GachaGroupConfigureItems)
			{
				num3 += gachaGroupConfigureItem.ItemWeight;
			}
			foreach (GachaGroupConfigureItem gachaGroupConfigureItem2 in gachaGroupConfigure.GachaGroupConfigureItems)
			{
				float num4 = (float)gachaGroupConfigureItem2.ItemWeight * 1f / (float)num3;
				GachaGroupItemData item = new GachaGroupItemData(gachaGroupConfigureItem2, num2 * num4);
				value.Add(item);
			}
		}
	}

	public string GetDesc(int type)
	{
		return type switch
		{
			1 => poolData.RoleDecriptionID.GetLocal(UIStringType.Gacha), 
			2 => poolData.FashionDecriptionID.GetLocal(UIStringType.Gacha), 
			3 => poolData.GiftDecriptionID.GetLocal(UIStringType.Gacha), 
			4 => poolData.OtherDecriptionID.GetLocal(UIStringType.Gacha), 
			_ => "", 
		};
	}

	public KeyValuePair<int, List<GachaGroupItemData>> GetItemsByIndex(int index)
	{
		if (index >= itemsDict.Count)
		{
			return default(KeyValuePair<int, List<GachaGroupItemData>>);
		}
		int num = 0;
		foreach (KeyValuePair<int, List<GachaGroupItemData>> item in itemsDict)
		{
			if (index == num)
			{
				return item;
			}
			num++;
		}
		return default(KeyValuePair<int, List<GachaGroupItemData>>);
	}
}
