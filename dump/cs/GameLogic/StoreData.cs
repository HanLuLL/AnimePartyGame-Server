using System.Collections.Generic;
using Google.Protobuf.Collections;
using party.model;

namespace GameLogic;

public class StoreData
{
	public Dictionary<int, RepeatedField<int>> randGoodsDict = new Dictionary<int, RepeatedField<int>>();

	public Dictionary<int, MapField<int, int>> recordDict = new Dictionary<int, MapField<int, int>>();

	public Dictionary<int, MapField<int, int>> rechargeRecordDict = new Dictionary<int, MapField<int, int>>();

	public Dictionary<int, RepeatedField<int>> FinishFirstBuy = new Dictionary<int, RepeatedField<int>>();

	public Dictionary<int, Day7GiftData> day7GiftDataDict = new Dictionary<int, Day7GiftData>();

	public MonthlyCardData monthlyCard = new MonthlyCardData();

	public Dictionary<int, int> GiftChainData = new Dictionary<int, int>();

	public StoreSkinGroupData skinGroupData = new StoreSkinGroupData();

	public void UpdateStoreData(PlayerShopInfo _shopInfo)
	{
		randGoodsDict.Clear();
		recordDict.Clear();
		rechargeRecordDict.Clear();
		GiftChainData.Clear();
		skinGroupData.Clear();
		if (_shopInfo.ShopRandomItem != null)
		{
			foreach (KeyValuePair<int, ShopRandomItem> item in _shopInfo.ShopRandomItem)
			{
				randGoodsDict.TryAdd(item.Key, item.Value.GoodsId);
			}
		}
		if (_shopInfo.Record != null)
		{
			foreach (KeyValuePair<int, ShopBuyRecord> item2 in _shopInfo.Record)
			{
				UpdateRecord(item2.Key, item2.Value.Records);
			}
		}
		if (_shopInfo.RechargeRecord != null)
		{
			foreach (KeyValuePair<int, ShopBuyRecord> item3 in _shopInfo.RechargeRecord)
			{
				UpdateRechargeRecord(item3.Key, item3.Value.Records);
			}
		}
		UpdateFirstBug(_shopInfo.FirstBuy);
		foreach (KeyValuePair<int, ExchangeStoreGiftChainConfigure> item4 in StaticConfigure.ExchangeStore.GiftChainDict)
		{
			GiftChainData.Add(item4.Key, _shopInfo.ShopRookieGift.GetValueOrDefault(item4.Key, 0));
		}
		skinGroupData.InitData();
	}

	public void UpdateRecord(int _shopType, MapField<int, int> _recordsData)
	{
		if (recordDict.TryGetValue(_shopType, out var value))
		{
			foreach (KeyValuePair<int, int> _recordsDatum in _recordsData)
			{
				if (value.ContainsKey(_recordsDatum.Key))
				{
					value.Remove(_recordsDatum.Key);
				}
				value.TryAdd(_recordsDatum.Key, _recordsDatum.Value);
			}
			return;
		}
		recordDict.TryAdd(_shopType, _recordsData);
	}

	public void UpdateRechargeRecord(int _shopType, MapField<int, int> _rechargeRecordsData)
	{
		if (rechargeRecordDict.TryGetValue(_shopType, out var value))
		{
			foreach (KeyValuePair<int, int> _rechargeRecordsDatum in _rechargeRecordsData)
			{
				if (value.ContainsKey(_rechargeRecordsDatum.Key))
				{
					value.Remove(_rechargeRecordsDatum.Key);
				}
				value.TryAdd(_rechargeRecordsDatum.Key, _rechargeRecordsDatum.Value);
			}
			return;
		}
		rechargeRecordDict.TryAdd(_shopType, _rechargeRecordsData);
	}

	public void UpdateFirstBug(MapField<int, RechargeBuyFirst> _data)
	{
		FinishFirstBuy.Clear();
		if (_data == null)
		{
			return;
		}
		foreach (KeyValuePair<int, RechargeBuyFirst> _datum in _data)
		{
			FinishFirstBuy.TryAdd(_datum.Key, _datum.Value.GoodsId);
		}
	}

	public int GetGoodsRecordsByGoodsId(int shopType, int goodsId)
	{
		if (!recordDict.TryGetValue(shopType, out var value))
		{
			return 0;
		}
		if (!value.TryGetValue(goodsId, out var value2))
		{
			return 0;
		}
		return value2;
	}

	public RepeatedField<int> GetRandomGoodsIdsByType(int shopType)
	{
		if (!randGoodsDict.TryGetValue(shopType, out var value))
		{
			return null;
		}
		return value;
	}

	public int GetGoodsRechargeRecordsByGoodsId(int shopType, int goodsId)
	{
		if (!rechargeRecordDict.TryGetValue(shopType, out var value))
		{
			return 0;
		}
		if (!value.TryGetValue(goodsId, out var value2))
		{
			return 0;
		}
		return value2;
	}

	public void UpdateDay7Gift(MapField<int, Day7Reward> day7)
	{
		day7GiftDataDict.Clear();
		foreach (KeyValuePair<int, Day7Reward> item in day7)
		{
			UpdateDay7Gift(item.Value);
		}
	}

	public void UpdateDay7Gift(Day7Reward _data)
	{
		if (day7GiftDataDict.ContainsKey(_data.GoodsId))
		{
			day7GiftDataDict[_data.GoodsId].UpdateData(_data);
			return;
		}
		Day7GiftData day7GiftData = new Day7GiftData(_data.GoodsId, _hasPurchase: true);
		day7GiftData.UpdateData(_data);
		day7GiftDataDict.TryAdd(_data.GoodsId, day7GiftData);
	}

	public Day7GiftData GetDay7GiftData(int goodsId)
	{
		return day7GiftDataDict.GetValueOrDefault(goodsId);
	}

	public void UpdateMonthlyCard(int MonthlyCardRemDays)
	{
		monthlyCard.UpdateDeadLineCount(MonthlyCardRemDays);
	}
}
