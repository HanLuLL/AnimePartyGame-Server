using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class StoreTabData
{
	public string parentName;

	public int tabOrder;

	public RepeatedField<int> showShopTabTypes = new RepeatedField<int>();

	public readonly List<ShopTypeData> shopTypes = new List<ShopTypeData>();

	public bool _IsShow => shopTypes.Count > 0;

	public bool newGoods
	{
		get
		{
			foreach (ShopTypeData shopType in shopTypes)
			{
				if (shopType.newGoods)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool has7DailyGift_PVP
	{
		get
		{
			foreach (ShopTypeData shopType in shopTypes)
			{
				if (shopType.tabType == ShopTabType.Day7GiftPackage)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool has7DailyGift_PVE
	{
		get
		{
			foreach (ShopTypeData shopType in shopTypes)
			{
				if (shopType.tabType == ShopTabType.Day7GiftPackagePve)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool has7DailyGift_PVEYear
	{
		get
		{
			foreach (ShopTypeData shopType in shopTypes)
			{
				if (shopType.tabType == ShopTabType.Day7GiftPackagePveYear)
				{
					return true;
				}
			}
			return false;
		}
	}

	public StoreTabData(int _shelfId, RepeatedField<ShopTabType> _shopTabTypes, string name, int order)
	{
		shopTypes.Clear();
		parentName = name;
		tabOrder = order;
		foreach (ShopTabType _shopTabType in _shopTabTypes)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetStoreType(_shopTabType) == StoreType.EXCHARGE)
			{
				ExchangeStoreInfoConfigure exchangeStoreInfoConfigure = ((int)_shopTabType).GetExchangeStoreInfoConfigure();
				if (AdjustAvailableTime(_shopTabType, exchangeStoreInfoConfigure.BeginTime, exchangeStoreInfoConfigure.EndTime))
				{
					ShopTypeData item = new ShopTypeData
					{
						tabType = _shopTabType,
						tabName = exchangeStoreInfoConfigure.NameID.GetLocal(UIStringType.ExchangeStore),
						currencyBar = exchangeStoreInfoConfigure.CurrencyBar,
						beginTime = exchangeStoreInfoConfigure.BeginTime,
						endTime = exchangeStoreInfoConfigure.EndTime,
						ShelfId = _shelfId
					};
					shopTypes.Add(item);
					showShopTabTypes.Add((int)_shopTabType);
				}
				continue;
			}
			RechargeStoreInfoConfigure rechargeStoreInfoConfigure = ((int)_shopTabType).GetRechargeStoreInfoConfigure();
			if (_shopTabType == ShopTabType.AlternateRecommendation)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetRecommendTabStatus())
				{
					UpdateRechargeData(_shopTabType, rechargeStoreInfoConfigure, _shelfId);
				}
			}
			else if (AdjustAvailableTime(_shopTabType, rechargeStoreInfoConfigure.BeginTime, rechargeStoreInfoConfigure.EndTime))
			{
				if (_shopTabType == ShopTabType.BeginnerPack && !AvailableBeginnerPack())
				{
					break;
				}
				UpdateRechargeData(_shopTabType, rechargeStoreInfoConfigure, _shelfId);
			}
		}
	}

	private bool AvailableBeginnerPack()
	{
		RechargeStoreAdsAdsConfigureItem noviceGiftPackage = StaticConfigure.RechargeStoreAds.NoviceGiftPackage;
		if (noviceGiftPackage != null && StaticConfigure.Way.DataDict.TryGetValue(noviceGiftPackage.Way, out var value) && value.WayType == WayType.Mall)
		{
			ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
			if (exchangeGoodsByShopTypeAndGoodsID == null)
			{
				Debug.LogError($"ExchangeStore 未找到商品数据: shopTabType:{value.WayParam[0]} goodsId:{value.WayParam[1]}");
				return false;
			}
			if (!exchangeGoodsByShopTypeAndGoodsID.SellOut())
			{
				return !exchangeGoodsByShopTypeAndGoodsID.IsOwn();
			}
			return false;
		}
		return false;
	}

	public void UpdateRechargeData(ShopTabType _tabType, RechargeStoreInfoConfigure _info, int _shelfId)
	{
		ShopTypeData item = new ShopTypeData
		{
			tabType = _tabType,
			tabName = _info.NameID.GetLocal(UIStringType.RechargeStore),
			currencyBar = _info.CurrencyBar,
			beginTime = _info.BeginTime,
			endTime = _info.EndTime,
			ShelfId = _shelfId
		};
		shopTypes.Add(item);
		showShopTabTypes.Add((int)_tabType);
	}

	public int GetTabNodeIndex(ShopTabType type)
	{
		for (int i = 0; i < showShopTabTypes.Count; i++)
		{
			if (showShopTabTypes[i] == (int)type)
			{
				return i;
			}
		}
		return -1;
	}

	private bool AdjustAvailableTime(ShopTabType tabType, Timestamp beginTime, Timestamp endTime)
	{
		switch (tabType)
		{
		case ShopTabType.Day7GiftPackage:
		case ShopTabType.Day7GiftPackagePve:
		case ShopTabType.Day7GiftPackagePveYear:
			if (SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftPackageConfig(tabType) == null)
			{
				return false;
			}
			return true;
		case ShopTabType.ComebackShop:
			return SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.IsInPeriod() == true;
		default:
			return TimeHelper.ValidityTime(beginTime, endTime);
		}
	}
}
