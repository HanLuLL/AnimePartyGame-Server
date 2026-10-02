using System;
using Core.Net;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class Day7GiftData : RechargeGoods
{
	private DateTime _CreateTime;

	private int _HasFinishDay;

	private bool TodayFinishReward;

	public readonly Day7GiftPackageGoodsConfigure giftConfig;

	public int HasFinishDay => Mathf.Clamp(_HasFinishDay, 0, MaxFinishDay);

	private int MaxFinishDay => giftConfig.Day7GiftPackageGoodsConfigureItems.Count;

	public int TodayRewardProgress
	{
		get
		{
			if (!TodayFinishReward)
			{
				return Mathf.Clamp(_HasFinishDay + 1, 0, MaxFinishDay);
			}
			return HasFinishDay;
		}
	}

	public bool RewardStatus
	{
		get
		{
			if (!TodayFinishReward)
			{
				return HasFinishDay != MaxFinishDay;
			}
			return false;
		}
	}

	public bool NoAvailTime
	{
		get
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			DateTime dateTime = _CreateTime.AddDays(giftConfig.ValidityPeriod);
			return serverTime > dateTime;
		}
	}

	public Day7GiftData(int GoodsId, bool _hasPurchase)
	{
		goodsId = GoodsId;
		StaticConfigure.Day7GiftPackage.GoodsDict.TryGetValue(goodsId, out giftConfig);
		foreach (RechargeStoreGoodsConfigure item in StaticConfigure.RechargeStore.Goodss)
		{
			RepeatedField<RechargeStoreGoodsConfigureItem> rechargeStoreGoodsConfigureItems = item.RechargeStoreGoodsConfigureItems;
			for (int i = 0; i < rechargeStoreGoodsConfigureItems.Count; i++)
			{
				if (rechargeStoreGoodsConfigureItems[i].GoodsID == goodsId)
				{
					RefreshRechargeGoodsInfo(item.ShopTabType, rechargeStoreGoodsConfigureItems[i], _hasPurchase ? 1 : 0);
					break;
				}
			}
		}
	}

	public void UpdateData(Day7Reward _data)
	{
		_CreateTime = (_data.CreateTime * 1000).StampMillisecondsToDateTime();
		_HasFinishDay = _data.MaxRewardDay;
		TodayFinishReward = _data.TodayReward;
	}

	public string GetResidualTime()
	{
		if (!HasPurchase())
		{
			return null;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime dateTime = _CreateTime.AddDays(giftConfig.ValidityPeriod);
		if (serverTime > dateTime)
		{
			return null;
		}
		TimeSpan timeSpan = dateTime - serverTime;
		return string.Format(1036.GetLocal(UIStringType.Message), timeSpan.Days.ToString().PadLeft(2, '0'), timeSpan.Hours.ToString().PadLeft(2, '0'), timeSpan.Minutes.ToString().PadLeft(2, '0'));
	}
}
