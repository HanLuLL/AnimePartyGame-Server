using System.Collections.Generic;
using UI;

namespace GameLogic;

public class StoreSkinGroupData
{
	public Dictionary<int, List<int>> skinGoodsGroupDatas = new Dictionary<int, List<int>>(8);

	private Dictionary<int, int> goodsGroupIds = new Dictionary<int, int>(32);

	public void InitData()
	{
		Clear();
		foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in 14.GetExchangeStoreGoodsConfigure().ExchangeStoreGoodsConfigureItems)
		{
			if (StaticConfigure.ExchangeStore.SkinGroupDict.TryGetValue(exchangeStoreGoodsConfigureItem.Param, out var value))
			{
				if (!skinGoodsGroupDatas.ContainsKey(value.Id))
				{
					skinGoodsGroupDatas.Add(value.Id, new List<int>(8));
				}
				skinGoodsGroupDatas[value.Id].Add(exchangeStoreGoodsConfigureItem.GoodsID);
				goodsGroupIds[exchangeStoreGoodsConfigureItem.GoodsID] = value.Id;
			}
		}
	}

	public void Clear()
	{
		goodsGroupIds?.Clear();
		skinGoodsGroupDatas?.Clear();
	}

	public int GetGroupIdByGoodsId(int goodsID)
	{
		goodsGroupIds.TryGetValue(goodsID, out var value);
		return value;
	}
}
