using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class StoreLogic : IRPCSync
{
	private readonly StoreData storeData = new StoreData();

	public readonly StoreSignal signal = new StoreSignal();

	private readonly Dictionary<int, List<int>> NewGoodsDict = new Dictionary<int, List<int>>();

	public readonly ReactiveProperty<bool> storeRedSignal = new ReactiveProperty<bool>(initialValue: false);

	private List<int> _sortSkinGoodsGroupIds;

	public void InitFromServer(PlayerShopInfo playerShopInfo, MapField<int, Day7Reward> Day7, int MonthlyCardRemDays)
	{
		storeData.UpdateStoreData(playerShopInfo);
		storeData.UpdateDay7Gift(Day7);
		storeData.monthlyCard.InitDeadlineCount(MonthlyCardRemDays);
		UpdateNewGoods();
	}

	public StoreType GetStoreType(ShopTabType shopTabType)
	{
		if (shopTabType == ShopTabType.Groceries)
		{
			return StoreType.STARCOIN;
		}
		if (StaticConfigure.RechargeStore.InfoDict.TryGetValue((int)shopTabType, out var _))
		{
			return StoreType.RECHARGE;
		}
		if (StaticConfigure.ExchangeStore.InfoDict.TryGetValue((int)shopTabType, out var _))
		{
			return StoreType.EXCHARGE;
		}
		Debug.LogError("ExchangeStore 和 RechargeStore 无法找到 " + shopTabType.ToString() + " 配置");
		return StoreType.None;
	}

	public StoreType GetStoreTypeByGoods(ShopTabType shopTabType)
	{
		if (shopTabType == ShopTabType.Groceries)
		{
			return StoreType.STARCOIN;
		}
		if (StaticConfigure.RechargeStore.GoodsDict.TryGetValue((int)shopTabType, out var _))
		{
			return StoreType.RECHARGE;
		}
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue((int)shopTabType, out var _))
		{
			return StoreType.EXCHARGE;
		}
		Debug.LogWarning("ExchangeStore 和 RechargeStore 无法找到 " + shopTabType.ToString() + " 配置");
		return StoreType.None;
	}

	public GoodsPurchaseType GetGoodsPurchaseType(ShopTabType shopTabType)
	{
		if (StaticConfigure.ShopTab.InfoDict.TryGetValue((int)shopTabType, out var value))
		{
			return value.GoodsPurchaseType;
		}
		Debug.LogError("ShopTab.InfoDict 无法找到 " + shopTabType.ToString() + " 配置");
		return GoodsPurchaseType.None;
	}

	public List<BaseGoodsData> GetGoodsByShopType(ShopTabType shopType)
	{
		if (GetGoodsPurchaseType(shopType) == GoodsPurchaseType.Recharge)
		{
			return GetRechargeGoodsByShopType((int)shopType);
		}
		return GetExchangeGoodsByShopType((int)shopType);
	}

	public List<BaseGoodsData> GetRechargeGoodsByShopType(int shopType)
	{
		List<BaseGoodsData> list = new List<BaseGoodsData>();
		if (StaticConfigure.RechargeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (RechargeStoreGoodsConfigureItem rechargeStoreGoodsConfigureItem in value.RechargeStoreGoodsConfigureItems)
			{
				if (TimeHelper.ValidityTime(rechargeStoreGoodsConfigureItem.BeginTime, rechargeStoreGoodsConfigureItem.EndTime))
				{
					int goodsRechargeRecordsByGoodsId = storeData.GetGoodsRechargeRecordsByGoodsId(shopType, rechargeStoreGoodsConfigureItem.GoodsID);
					RechargeGoods item = new RechargeGoods((ShopTabType)shopType, rechargeStoreGoodsConfigureItem, goodsRechargeRecordsByGoodsId);
					list.Add(item);
				}
			}
		}
		else
		{
			ShopTabType shopTabType = (ShopTabType)shopType;
			Debug.LogError("RechargeStore中没有货架" + shopTabType.ToString() + "的商品");
		}
		return list;
	}

	public List<BaseGoodsData> GetExchangeGoodsByShopType(int shopType)
	{
		List<BaseGoodsData> result = new List<BaseGoodsData>();
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if (TimeHelper.ValidityTime(exchangeStoreGoodsConfigureItem.BeginTime, exchangeStoreGoodsConfigureItem.EndTime))
				{
					int goodsRecordsByGoodsId = storeData.GetGoodsRecordsByGoodsId(shopType, exchangeStoreGoodsConfigureItem.GoodsID);
					ExchangeGoods exchangeGoods = new ExchangeGoods((ShopTabType)shopType, exchangeStoreGoodsConfigureItem, goodsRecordsByGoodsId);
					if (value.ShopTabType != ShopTabType.GiftPackage || exchangeStoreGoodsConfigureItem.Param <= 0 || exchangeGoods.SellOut())
					{
						result.Add(exchangeGoods);
					}
				}
			}
			if (shopType == 9)
			{
				GetExchangeGiftChainGoods(ref result);
			}
		}
		else
		{
			ShopTabType shopTabType = (ShopTabType)shopType;
			Debug.LogError("ExchangeStore中没有货架" + shopTabType.ToString() + "的商品");
		}
		return result;
	}

	public RechargeGoods GetRechargeGoodsByShopTypeAndGoodsID(int shopType, int goodsId)
	{
		if (StaticConfigure.RechargeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (RechargeStoreGoodsConfigureItem rechargeStoreGoodsConfigureItem in value.RechargeStoreGoodsConfigureItems)
			{
				if (goodsId == rechargeStoreGoodsConfigureItem.GoodsID)
				{
					int goodsRechargeRecordsByGoodsId = storeData.GetGoodsRechargeRecordsByGoodsId(shopType, rechargeStoreGoodsConfigureItem.GoodsID);
					return new RechargeGoods((ShopTabType)shopType, rechargeStoreGoodsConfigureItem, goodsRechargeRecordsByGoodsId);
				}
			}
		}
		return null;
	}

	public ExchangeGoods GetExchangeGoodsByShopTypeAndGoodsID(int shopType, int GoodsID)
	{
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if (GoodsID == exchangeStoreGoodsConfigureItem.GoodsID)
				{
					int goodsRecordsByGoodsId = storeData.GetGoodsRecordsByGoodsId(shopType, exchangeStoreGoodsConfigureItem.GoodsID);
					return new ExchangeGoods((ShopTabType)shopType, exchangeStoreGoodsConfigureItem, goodsRecordsByGoodsId);
				}
			}
		}
		return null;
	}

	public ExchangeGoods GetExchangeGoodsByShopTypeAndItemID(int shopType, int itemID)
	{
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if (itemID == exchangeStoreGoodsConfigureItem.ItemID)
				{
					return new ExchangeGoods((ShopTabType)shopType, exchangeStoreGoodsConfigureItem, 0);
				}
			}
		}
		return null;
	}

	public ExchangeGoods GetExchangeGoodsByShopTypeAndParamID(int shopType, int itemID)
	{
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(shopType, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if (itemID == exchangeStoreGoodsConfigureItem.Param)
				{
					return new ExchangeGoods((ShopTabType)shopType, exchangeStoreGoodsConfigureItem, 0);
				}
			}
		}
		return null;
	}

	public List<DailRefreshGoods> GetDailyRefresh()
	{
		List<DailRefreshGoods> list = new List<DailRefreshGoods>();
		RepeatedField<int> randomGoodsIdsByType = storeData.GetRandomGoodsIdsByType(2);
		foreach (ExchangeStoreRefreshPoolConfigure refreshPool in StaticConfigure.ExchangeStore.RefreshPools)
		{
			foreach (ExchangeStoreRefreshPoolConfigureItem exchangeStoreRefreshPoolConfigureItem in refreshPool.ExchangeStoreRefreshPoolConfigureItems)
			{
				if (randomGoodsIdsByType.Contains(exchangeStoreRefreshPoolConfigureItem.GoodsID))
				{
					int goodsRecordsByGoodsId = storeData.GetGoodsRecordsByGoodsId(2, exchangeStoreRefreshPoolConfigureItem.GoodsID);
					list.Add(new DailRefreshGoods(ShopTabType.Groceries, exchangeStoreRefreshPoolConfigureItem, goodsRecordsByGoodsId));
				}
			}
		}
		return list;
	}

	public bool IsFinishFirstBug(int GoodsId)
	{
		if (storeData.FinishFirstBuy.TryGetValue(7, out var value))
		{
			return value.Contains(GoodsId);
		}
		return false;
	}

	private void PlaySuccessSFX()
	{
		Stage.inst.PlayOneShotSound(11);
	}

	public bool IsPurchaseChestGoods(int chestId)
	{
		ChestInfoConfigure chestInfoConfigure = chestId.GetChestInfoConfigure();
		if (chestInfoConfigure == null)
		{
			return false;
		}
		RepeatedField<int> randomReward = chestInfoConfigure.RandomReward;
		for (int i = 0; i < randomReward.Count; i++)
		{
			foreach (ChestRandomRewardConfigureItem chestRandomRewardConfigureItem in randomReward[i].GetChestRandomRewardConfigure().ChestRandomRewardConfigureItems)
			{
				ItemInfoConfigure itemInfoConfigure = chestRandomRewardConfigureItem.ItemID.GetItemInfoConfigure();
				if (itemInfoConfigure.ItemType == ItemType.HeroStandingPainting)
				{
					return SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemInfoConfigure.Id);
				}
			}
		}
		return false;
	}

	public int GetGoodsRechargeRecord(int shopType, int goodsId)
	{
		return storeData.GetGoodsRechargeRecordsByGoodsId(shopType, goodsId);
	}

	public int GetGoodsRecord(int shopType, int goodsId)
	{
		return storeData.GetGoodsRecordsByGoodsId(shopType, goodsId);
	}

	public bool CheckGoodsPurchaseLicense(ShopTabType showType, int goodsId)
	{
		foreach (BaseGoodsData item in GetGoodsByShopType(showType))
		{
			if (item.goodsId == goodsId)
			{
				return true;
			}
		}
		return false;
	}

	public SkinStandingPaintingConfigureItem GetSkinInfoByGoodsInfo(BaseGoodsData GoodsData)
	{
		if (GoodsData?.itemConfig == null)
		{
			return null;
		}
		int itemID = GoodsData.itemConfig.SubMeterID.GetChestInfoConfigure().RandomReward[0].GetChestRandomRewardConfigure().ChestRandomRewardConfigureItems[0].ItemID;
		return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(itemID);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RefMallS2C.OnRefMallS2CServerCallBackAsync = OnRefMallS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerShopBuyS2C.OnPlayerShopBuyS2CServerCallBackAsync = OnPlayerShopBuyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.Day7RewardS2C.OnDay7RewardS2CServerCallBackAsync = OnDay7RewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetDay7RewardS2C.OnGetDay7RewardS2CServerCallBackAsync = OnGetDay7RewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MonthlyCardS2C.OnMonthlyCardS2CServerCallBackAsync = OnMonthlyCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChargeCreateS2C.OnChargeCreateS2CServerCallBackAsync = OnChargeCreateS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChargeS2C.OnChargeS2CServerCallBackAsync = OnChargeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PayResultS2C.OnPayResultS2CServerCallBackAsync = OnPayResultS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.DevChargeS2C.OnDevChargeS2CServerCallBackAsync = OnDevChargeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TransferStarDiscS2C.OnTransferStarDiscS2CServerCallBackAsync = OnTransferStarDiscS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RefMallS2C.OnRefMallS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerShopBuyS2C.OnPlayerShopBuyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.Day7RewardS2C.OnDay7RewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetDay7RewardS2C.OnGetDay7RewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MonthlyCardS2C.OnMonthlyCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PayResultS2C.OnPayResultS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.DevChargeS2C.OnDevChargeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TransferStarDiscS2C.OnTransferStarDiscS2CServerCallBackAsync = null;
	}

	private async UniTask OnRefMallS2CServerCallBack(RefMallS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			Debug.Log("商店收到服务器主动推送的更新，协议id——1044");
			storeData.UpdateStoreData(model.ShopInfo);
			signal.refreshCurShelf.Dispatch(0);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestMallBuy(int type, int goodsId, int num, int chainGiftId = 0, int discountCardId = 0)
	{
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(goodsId, (ShopTabType)type, LogToServerType.SHOPPING);
		return MonoSingletonProvider<NetManager>.inst.RPC.PlayerShopBuyC2S.PlayerShopBuyC2SCall(new PlayerShopBuyC2S
		{
			Type = type,
			GoodsId = goodsId,
			BuyCount = num,
			ChainId = chainGiftId,
			DiscountCardId = discountCardId
		});
	}

	private async UniTask OnPlayerShopBuyS2CServerCallBack(PlayerShopBuyS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
			SimpleSingletonProvider<UIManager>.inst.purchase.Hide();
			SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(model.GoodsId, (ShopTabType)model.Type, LogToServerType.SHOPPING_OK);
			PlaySuccessSFX();
			MapField<int, int> mapField = new MapField<int, int> { [model.GoodsId] = model.BuyCount };
			if (GetGoodsPurchaseType((ShopTabType)model.Type) == GoodsPurchaseType.Recharge)
			{
				storeData.UpdateRechargeRecord(model.Type, mapField);
			}
			else
			{
				storeData.UpdateRecord(model.Type, mapField);
			}
			if (model.Type == 9 && model.ChainId != 0)
			{
				storeData.GiftChainData[model.ChainId] = model.GoodsId;
			}
			signal.refreshCurShelf.Dispatch(0);
		}
	}

	private async UniTask OnDay7RewardS2CServerCallBack(Day7RewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			storeData.UpdateDay7Gift(model.Day7);
			signal.refreshCurShelf.Dispatch(13);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestGetDay7RewardC2S(int goodsId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetDay7RewardC2S.GetDay7RewardC2SCall(new GetDay7RewardC2S
		{
			GoodsId = goodsId
		});
	}

	private async UniTask OnGetDay7RewardS2CServerCallBack(GetDay7RewardS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
			PlaySuccessSFX();
			storeData.UpdateDay7Gift(model.Day7);
			signal.refreshCurShelf.Dispatch(13);
		}
	}

	public RPCAsyncResult RequestTransferStarDiscC2S(int payTokenCount)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.TransferStarDiscC2S.TransferStarDiscC2SCall(new TransferStarDiscC2S
		{
			Count = payTokenCount
		});
	}

	private async UniTask OnTransferStarDiscS2CServerCallBackAsync(TransferStarDiscS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public async UniTask RequestCreateOrder(BaseGoodsData info, int count, int couponsId = 0)
	{
		if (info != null)
		{
			await UniTask.CompletedTask;
			SimpleSingletonProvider<GameLogicManager>.inst.match.CheckRechargeActionForMatch();
			await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowMask();
			SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(info.goodsId, info.shopTabType, LogToServerType.SHOPPING);
			SimpleSingletonProvider<GameLogicManager>.inst.bnSdk.CreateOrder(info.goodsId, count, (int)info.shopTabType);
			if (HackerConfig.EnableGm() && HackerConfig.EnableRechargeTest())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.store.RequestDevChargeC2S((int)info.shopTabType, info.goodsId, 1);
			}
		}
	}

	private async UniTask OnChargeCreateS2CServerCallBack(ChargeCreateS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		}
		else
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnChargeS2CServerCallBack(ChargeS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
			return;
		}
		PlaySuccessSFX();
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(model.GoodsId, (ShopTabType)model.Type, LogToServerType.SHOPPING_OK);
		if (model.Type == 23)
		{
			storeData.UpdateMonthlyCard(model.MonthlyCardRemDays);
			await ShowMonthReward();
		}
		else
		{
			storeData.UpdateFirstBug(model.FirstBuy);
		}
		signal.refreshCurShelf.Dispatch(0);
		await UniTask.CompletedTask;
	}

	private async UniTask OnPayResultS2CServerCallBack(PayResultS2C model, int errId, bool isDispatch)
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (errId != 0)
		{
			Debug.LogError($"购买物品失败：ErrorCode： {errId}");
			return;
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo != null)
		{
			if (HackerConfig.IsValid())
			{
				Debug.LogError($"当前下发的玩家充值：{model.Amount}");
			}
			playerInfo.RechargeSum = model.Amount;
		}
		PlaySuccessSFX();
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(model.GoodsId, (ShopTabType)model.Type, LogToServerType.SHOPPING_OK);
		if (model.Type == 23)
		{
			storeData.UpdateMonthlyCard(model.MonthlyCardRemDays);
			await ShowMonthReward();
		}
		else
		{
			storeData.UpdateFirstBug(model.FirstBuy);
		}
		signal.refreshCurShelf.Dispatch(0);
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestDevChargeC2S(int type, int goodsId, int count)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.DevChargeC2S.DevChargeC2SCall(new DevChargeC2S
		{
			GoodsId = goodsId,
			Count = count,
			Type = type
		});
	}

	private async UniTask OnDevChargeS2CServerCallBack(DevChargeS2C model, int errId, bool isdispatch)
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		if (errId == 0)
		{
			PlaySuccessSFX();
			if (model.Type == 23)
			{
				storeData.UpdateMonthlyCard(model.MonthlyCardRemDays);
				await ShowMonthReward();
			}
			else
			{
				storeData.UpdateFirstBug(model.FirstBuy);
			}
			signal.refreshCurShelf.Dispatch(0);
		}
	}

	public Day7GiftData Get7DailyGiftPackageConfig(ShopTabType tabType)
	{
		if (StaticConfigure.RechargeStore.GoodsDict.TryGetValue((int)tabType, out var value))
		{
			foreach (RechargeStoreGoodsConfigureItem rechargeStoreGoodsConfigureItem in value.RechargeStoreGoodsConfigureItems)
			{
				Day7GiftData day7GiftData = storeData.GetDay7GiftData(rechargeStoreGoodsConfigureItem.GoodsID);
				if (day7GiftData == null)
				{
					if (TimeHelper.ValidityTime(rechargeStoreGoodsConfigureItem.BeginTime, rechargeStoreGoodsConfigureItem.EndTime) && (storeData.GetGoodsRechargeRecordsByGoodsId((int)tabType, rechargeStoreGoodsConfigureItem.GoodsID) < rechargeStoreGoodsConfigureItem.NumLimit || rechargeStoreGoodsConfigureItem.NumLimit == 0))
					{
						return new Day7GiftData(rechargeStoreGoodsConfigureItem.GoodsID, _hasPurchase: false);
					}
				}
				else if (!day7GiftData.NoAvailTime)
				{
					return day7GiftData;
				}
			}
		}
		return null;
	}

	public bool Get7DailyGiftStatus(ShopTabType shopTabType)
	{
		Day7GiftData day7GiftData = Get7DailyGiftPackageConfig(shopTabType);
		if (day7GiftData == null)
		{
			return false;
		}
		if (day7GiftData.HasPurchase() && !day7GiftData.NoAvailTime)
		{
			return day7GiftData.RewardStatus;
		}
		return false;
	}

	public MonthlyCardData GetMonthlyCardConfig()
	{
		return storeData.monthlyCard;
	}

	private async UniTask OnMonthlyCardS2CServerCallBack(MonthlyCardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			storeData.UpdateMonthlyCard(model.RemDay);
			signal.refreshCurShelf.Dispatch(23);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask ShowMonthReward()
	{
		if (storeData.monthlyCard.monthlyCardConfig.MonthlyDays - 1 == storeData.monthlyCard.deadlineDay)
		{
			SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCardAllReward();
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCardPurchaseReward();
		}
		SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCard();
		await UniTask.CompletedTask;
	}

	public void RegisterRed()
	{
		storeRedSignal.Value = GetSystemStatus();
	}

	private bool GetSystemStatus()
	{
		if (GetDayRefreshStatus())
		{
			return true;
		}
		if (Get7DailyGiftStatus(ShopTabType.Day7GiftPackage))
		{
			return true;
		}
		if (Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePve))
		{
			return true;
		}
		if (ExchangeNewGoodsStatus())
		{
			return true;
		}
		if (RechargeNewGoodsStatus())
		{
			return true;
		}
		return false;
	}

	private bool GetDayRefreshStatus()
	{
		foreach (DailRefreshGoods item in GetDailyRefresh())
		{
			if (item.SalePrice == 0 && item.RemainingNum() > 0)
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateNewGoods()
	{
		RepeatedField<ExchangeStoreInfoConfigure> infos = StaticConfigure.ExchangeStore.Infos;
		for (int i = 0; i < infos.Count; i++)
		{
			if (TimeHelper.ValidityTime(infos[i].BeginTime, infos[i].EndTime))
			{
				List<int> newGoodsByType = GetNewGoodsByType((int)infos[i].ShopTabType);
				NewGoodsDict.TryAdd((int)infos[i].ShopTabType, newGoodsByType);
			}
		}
		RepeatedField<RechargeStoreInfoConfigure> infos2 = StaticConfigure.RechargeStore.Infos;
		for (int j = 0; j < infos2.Count; j++)
		{
			if (TimeHelper.ValidityTime(infos2[j].BeginTime, infos2[j].EndTime))
			{
				List<int> newGoodsByType2 = GetNewGoodsByType((int)infos2[j].ShopTabType);
				NewGoodsDict.TryAdd((int)infos2[j].ShopTabType, newGoodsByType2);
			}
		}
	}

	public List<int> GetNewGoodsByType(int ShopType)
	{
		List<int> list = new List<int>();
		RechargeStoreGoodsConfigure value2;
		if (GetStoreTypeByGoods((ShopTabType)ShopType) == StoreType.EXCHARGE)
		{
			if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(ShopType, out var value))
			{
				foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
				{
					if (exchangeStoreGoodsConfigureItem.GoodsLabelType == GoodsLabelType.New && storeData.GetGoodsRecordsByGoodsId((int)value.ShopTabType, exchangeStoreGoodsConfigureItem.GoodsID) == 0 && TimeHelper.ValidityTime(exchangeStoreGoodsConfigureItem.BeginTime, exchangeStoreGoodsConfigureItem.EndTime))
					{
						list.Add(exchangeStoreGoodsConfigureItem.GoodsID);
					}
				}
				return list;
			}
		}
		else if (StaticConfigure.RechargeStore.GoodsDict.TryGetValue(ShopType, out value2))
		{
			foreach (RechargeStoreGoodsConfigureItem rechargeStoreGoodsConfigureItem in value2.RechargeStoreGoodsConfigureItems)
			{
				if (rechargeStoreGoodsConfigureItem.GoodsLabelType == GoodsLabelType.New && storeData.GetGoodsRechargeRecordsByGoodsId((int)value2.ShopTabType, rechargeStoreGoodsConfigureItem.GoodsID) == 0 && TimeHelper.ValidityTime(rechargeStoreGoodsConfigureItem.BeginTime, rechargeStoreGoodsConfigureItem.EndTime))
				{
					list.Add(rechargeStoreGoodsConfigureItem.GoodsID);
				}
			}
			return list;
		}
		return list;
	}

	public bool ExchangeNewGoodsStatus()
	{
		RepeatedField<ExchangeStoreInfoConfigure> infos = StaticConfigure.ExchangeStore.Infos;
		for (int i = 0; i < infos.Count; i++)
		{
			if (NewGoodsStatusByType((int)infos[i].ShopTabType))
			{
				return true;
			}
		}
		return false;
	}

	public bool RechargeNewGoodsStatus()
	{
		RepeatedField<RechargeStoreInfoConfigure> infos = StaticConfigure.RechargeStore.Infos;
		for (int i = 0; i < infos.Count; i++)
		{
			if (NewGoodsStatusByType((int)infos[i].ShopTabType))
			{
				return true;
			}
		}
		return false;
	}

	public bool NewGoodsStatusByType(int ShopType)
	{
		if (NewGoodsDict.TryGetValue(ShopType, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				if (!LocalCache.StoreOldGoods.Contains(value[i]))
				{
					return true;
				}
			}
		}
		return false;
	}

	public string GetRefreshTime(GoodsRefreshType _goodsRefreshType, Timestamp _configEndTime)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		switch (_goodsRefreshType)
		{
		case GoodsRefreshType.Daily:
			return TimeHelper.GetDailyTime();
		case GoodsRefreshType.Weekly:
			return TimeHelper.GetWeeklyTime();
		case GoodsRefreshType.Monthly:
			return TimeHelper.GetMonthlyTime();
		default:
			return null;
		case GoodsRefreshType.None:
			if ((object)_configEndTime != null)
			{
				return TimeHelper.RefreshTimeText(1010, 1011, serverTime, _configEndTime.ToDateTime());
			}
			return null;
		}
	}

	public bool GetRecommendTabStatus()
	{
		RepeatedField<RechargeStoreAdsAdsConfigureItem> rechargeStoreAdsAdsConfigureItems = StaticConfigure.RechargeStoreAds.RecommendPackage.RechargeStoreAdsAdsConfigureItems;
		for (int i = 0; i < rechargeStoreAdsAdsConfigureItems.Count; i++)
		{
			if (!StaticConfigure.Way.DataDict.TryGetValue(rechargeStoreAdsAdsConfigureItems[i].Way, out var value))
			{
				return false;
			}
			RechargeGoods rechargeGoodsByShopTypeAndGoodsID = GetRechargeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
			if (TimeHelper.ValidityTime(rechargeGoodsByShopTypeAndGoodsID.goodsConfig.BeginTime, rechargeGoodsByShopTypeAndGoodsID.goodsConfig.EndTime))
			{
				return true;
			}
		}
		return false;
	}

	public List<RechargeStoreAdsAdsConfigureItem> GetRecommendAdsData()
	{
		List<RechargeStoreAdsAdsConfigureItem> list = new List<RechargeStoreAdsAdsConfigureItem>();
		RepeatedField<RechargeStoreAdsAdsConfigureItem> rechargeStoreAdsAdsConfigureItems = StaticConfigure.RechargeStoreAds.RecommendPackage.RechargeStoreAdsAdsConfigureItems;
		for (int i = 0; i < rechargeStoreAdsAdsConfigureItems.Count; i++)
		{
			if (StaticConfigure.Way.DataDict.TryGetValue(rechargeStoreAdsAdsConfigureItems[i].Way, out var value))
			{
				RechargeGoods rechargeGoodsByShopTypeAndGoodsID = GetRechargeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
				if (TimeHelper.ValidityTime(rechargeGoodsByShopTypeAndGoodsID.goodsConfig.BeginTime, rechargeGoodsByShopTypeAndGoodsID.goodsConfig.EndTime))
				{
					list.Add(rechargeStoreAdsAdsConfigureItems[i]);
				}
			}
		}
		return list;
	}

	public SkinSellData GetSkinComboInfo()
	{
		int num = 32;
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(num, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if ((!(exchangeStoreGoodsConfigureItem.BeginTime == null) || !(exchangeStoreGoodsConfigureItem.EndTime == null)) && TimeHelper.ValidityTime(exchangeStoreGoodsConfigureItem.BeginTime, exchangeStoreGoodsConfigureItem.EndTime))
				{
					int goodsRecordsByGoodsId = storeData.GetGoodsRecordsByGoodsId(num, exchangeStoreGoodsConfigureItem.GoodsID);
					return new SkinSellData((ShopTabType)num, exchangeStoreGoodsConfigureItem, goodsRecordsByGoodsId);
				}
			}
		}
		else
		{
			ShopTabType shopTabType = (ShopTabType)num;
			Debug.LogError("ExchangeStore中没有货架" + shopTabType.ToString() + "的商品");
		}
		return null;
	}

	public SkinSellData GetSkinComboInfoById(int skinComboInfoId)
	{
		int num = 32;
		if (StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(num, out var value))
		{
			foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
			{
				if (exchangeStoreGoodsConfigureItem.Param == skinComboInfoId)
				{
					int goodsRecordsByGoodsId = storeData.GetGoodsRecordsByGoodsId(num, exchangeStoreGoodsConfigureItem.GoodsID);
					return new SkinSellData((ShopTabType)num, exchangeStoreGoodsConfigureItem, goodsRecordsByGoodsId);
				}
			}
		}
		else
		{
			ShopTabType shopTabType = (ShopTabType)num;
			Debug.LogError("ExchangeStore中没有货架" + shopTabType.ToString() + "的商品");
		}
		return null;
	}

	public ExchangeGoods GetExchangeGoodsByGiftChain(int giftChainId)
	{
		if (!storeData.GiftChainData.TryGetValue(giftChainId, out var value))
		{
			return null;
		}
		if (!StaticConfigure.ExchangeStore.GiftChainDict.TryGetValue(giftChainId, out var value2))
		{
			return null;
		}
		int num = 0;
		foreach (ExchangeStoreGiftChainConfigureItem exchangeStoreGiftChainConfigureItem in value2.ExchangeStoreGiftChainConfigureItems)
		{
			if ((value <= 0 && exchangeStoreGiftChainConfigureItem.Param <= 0) || exchangeStoreGiftChainConfigureItem.Param == value)
			{
				num = exchangeStoreGiftChainConfigureItem.GoodsID;
				break;
			}
		}
		if (num > 0)
		{
			return GetExchangeGoodsByShopTypeAndGoodsID(9, num);
		}
		return null;
	}

	public void GetExchangeGiftChainGoods(ref List<BaseGoodsData> result)
	{
		if (result == null)
		{
			return;
		}
		foreach (KeyValuePair<int, int> giftChainDatum in storeData.GiftChainData)
		{
			ExchangeGoods exchangeGoodsByGiftChain = GetExchangeGoodsByGiftChain(giftChainDatum.Key);
			if (exchangeGoodsByGiftChain != null)
			{
				result.Add(exchangeGoodsByGiftChain);
			}
		}
	}

	public int GetSkinGroupIdByGoodsId(int goodsID)
	{
		return storeData.skinGroupData.GetGroupIdByGoodsId(goodsID);
	}

	public List<int> GetSkinGoodsIdsByGroupId(int groupId)
	{
		storeData.skinGroupData.skinGoodsGroupDatas.TryGetValue(groupId, out var value);
		return value;
	}

	public List<int> GetSortSkinGoodsGroupConfigures()
	{
		if (_sortSkinGoodsGroupIds != null)
		{
			return _sortSkinGoodsGroupIds;
		}
		_sortSkinGoodsGroupIds = new List<int>(storeData.skinGroupData.skinGoodsGroupDatas.Keys);
		_sortSkinGoodsGroupIds.Sort(delegate(int a, int b)
		{
			StaticConfigure.ExchangeStore.SkinGroupDict.TryGetValue(a, out var value);
			StaticConfigure.ExchangeStore.SkinGroupDict.TryGetValue(b, out var value2);
			if (value == null && value2 == null)
			{
				return 0;
			}
			if (value == null)
			{
				return -1;
			}
			return (value2 == null) ? 1 : value.TabOrder.CompareTo(value2.TabOrder);
		});
		return _sortSkinGoodsGroupIds;
	}
}
