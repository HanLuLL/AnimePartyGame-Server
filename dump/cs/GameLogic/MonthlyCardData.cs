using System;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class MonthlyCardData : RechargeGoods
{
	private int DEADLINEDAY;

	private bool isShowMonthCardTip;

	public readonly MonthlyCardGoodsConfigure monthlyCardConfig;

	public int deadlineDay => DEADLINEDAY;

	public bool PurchaseLic
	{
		get
		{
			if (monthlyCardConfig == null)
			{
				return false;
			}
			if (deadlineDay >= monthlyCardConfig.SubscribeLimit)
			{
				return false;
			}
			return true;
		}
	}

	private void ShowMonthCardReward(int initDays, int _times)
	{
		if (initDays == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCardAllReward();
			_times--;
		}
		for (int i = 0; i < _times; i++)
		{
			SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCardPurchaseReward();
		}
		SimpleSingletonProvider<UIManager>.inst.reward.ShowMonthCard();
	}

	public override bool HasPurchase()
	{
		return deadlineDay != 0;
	}

	public MonthlyCardData()
	{
		if (!StaticConfigure.RechargeStore.GoodsDict.TryGetValue(23, out var value))
		{
			Debug.LogError("RechargeStore.Goods 无法取到 ShopTabType.MonthlyCard 配置");
			return;
		}
		RepeatedField<RechargeStoreGoodsConfigureItem> rechargeStoreGoodsConfigureItems = value.RechargeStoreGoodsConfigureItems;
		if (rechargeStoreGoodsConfigureItems == null || rechargeStoreGoodsConfigureItems.Count == 0)
		{
			Debug.LogError("ShopTabType.MonthlyCard 配置的数据为空数据");
			return;
		}
		RefreshRechargeGoodsInfo(ShopTabType.MonthlyCard, rechargeStoreGoodsConfigureItems[0], 0);
		if (!StaticConfigure.MonthlyCard.GoodsDict.TryGetValue(rechargeStoreGoodsConfigureItems[0].GoodsID, out monthlyCardConfig))
		{
			Debug.LogError($"MonthlyCard.Goods 无法取到 {rechargeStoreGoodsConfigureItems[0].GoodsID} 配置的数据");
		}
	}

	public void InitDeadlineCount(int monthlyCardRemDays)
	{
		DEADLINEDAY = monthlyCardRemDays;
	}

	public void UpdateDeadLineCount(int remDays)
	{
		isShowMonthCardTip = false;
		int dateRecord = SimpleSingletonProvider<GameLogicManager>.inst.account.GetDateRecord(DataChangeType.MONTH_CARD);
		if (dateRecord != remDays)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateDateData(DataChangeType.MONTH_CARD, remDays);
			if (dateRecord > 0 && remDays == 0)
			{
				isShowMonthCardTip = true;
				TriggerMonthCard();
			}
		}
		DEADLINEDAY = remDays;
	}

	public bool TriggerMonthCard(Action CancelCallBack = null)
	{
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel))
		{
			return false;
		}
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.GetDateRecord(DataChangeType.MONTH_CARD) > 0 && deadlineDay == 0;
		if (isShowMonthCardTip || flag)
		{
			isShowMonthCardTip = false;
			if (flag)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateDateData(DataChangeType.MONTH_CARD, deadlineDay);
			}
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(11, GOMonthCard, CancelCallBack).Forget();
			return true;
		}
		return false;
	}

	private void GOMonthCard()
	{
		SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, ShopTabType.MonthlyCard).Forget();
	}
}
