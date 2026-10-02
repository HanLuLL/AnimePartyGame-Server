using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIStore_Com_Day7Gift_PVE : GComponent
{
	private ShopTypeData typeData;

	private Day7GiftData giftPackage;

	public Controller status;

	public GLoader loader_BG;

	public GTextField txt_Theme;

	public GTextField txt_Duration;

	public GTextField txt_Advocacy;

	public UIStore_Button_Purchase7DayGift btn_Purchase;

	public UIStore_Button_Reward7DayGift btn_Reward;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day1;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day2;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day3;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day4;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day5;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day6;

	public UIStore_Com_Day7Gift_PVE_Reward btn_day7;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00px78qq3h";

	public void InitComponents()
	{
	}

	public void Refresh(ShopTypeData _typeData)
	{
		typeData = _typeData;
		RechargeStoreShelfConfigure rechargeStoreShelfConfigure = StaticConfigure.RechargeStore.ShelfDict[typeData.ShelfId];
		loader_BG.Background(rechargeStoreShelfConfigure.Background);
		giftPackage = SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftPackageConfig(typeData.tabType);
		if (giftPackage == null)
		{
			base.visible = false;
			return;
		}
		base.visible = true;
		status.selectedIndex = (giftPackage.HasPurchase() ? 1 : 0);
		txt_Theme.text = giftPackage.giftConfig.TitleID.GetLocal(UIStringType.Day7GiftPackage);
		txt_Advocacy.text = giftPackage.giftConfig.DescriptionID.GetLocal(UIStringType.Day7GiftPackage);
		txt_Duration.visible = !giftPackage.HasPurchase();
		txt_Duration.text = TimeHelper.GetDurationText(_typeData.beginTime, _typeData.endTime, OnlyDuration: true);
		RefreshPurchaseBtn();
		RefreshRewardBtn();
		RefreshProgress();
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(giftPackage.goodsId, ShopTabType.Day7GiftPackagePve, LogToServerType.GOODS_DETAIL);
	}

	public void AddEvent()
	{
		btn_Purchase.onClick.Add(Purchase7DayGift);
		btn_Reward.onClick.Add(GetReward7DayGift);
	}

	public void RemoveEvent()
	{
		btn_Purchase.onClick.Remove(Purchase7DayGift);
		btn_Reward.onClick.Add(GetReward7DayGift);
	}

	public void AddListener()
	{
	}

	public void RemoveListener()
	{
	}

	private async void Purchase7DayGift()
	{
		btn_Purchase.onClick.Retain();
		await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(giftPackage, 1);
		btn_Purchase.onClick.Release();
	}

	private void GetReward7DayGift()
	{
		btn_Reward.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.store.RequestGetDay7RewardC2S(giftPackage.goodsId).OnFinishedOnly.AddListener(delegate
		{
			btn_Reward.onClick.Release();
		});
	}

	private void RefreshRewardBtn()
	{
		if (giftPackage.HasPurchase())
		{
			string residualTime = giftPackage.GetResidualTime();
			if (!string.IsNullOrEmpty(residualTime))
			{
				btn_Reward.txt_Time.text = residualTime;
			}
			btn_Reward.avalible.selectedIndex = ((!giftPackage.RewardStatus) ? 1 : 0);
			btn_Reward.touchable = giftPackage.RewardStatus;
		}
	}

	private void RefreshPurchaseBtn()
	{
		if (!giftPackage.HasPurchase())
		{
			btn_Purchase.txt_Price.text = "[color=#FFF32B]" + giftPackage.GetDiscountPriceText() + "[/color]";
			btn_Purchase.txt_Price.AddCurrencySymbols(btn_Purchase.txt_Price.text);
			btn_Purchase.txt_Time.text = typeData.GetTabTime();
			btn_Purchase.txt_OriginalPrice.text = giftPackage.GetOriginalPriceText();
			bool flag = giftPackage.SalePrice < giftPackage.GetOriginalPrice();
			btn_Purchase.isDiscount.selectedIndex = (flag ? 1 : 0);
			if (flag)
			{
				double num = 100.0 - Math.Round((double)giftPackage.SalePrice * 100.0 / (double)giftPackage.GetOriginalPrice());
				btn_Purchase.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
			}
		}
	}

	private void RefreshProgress()
	{
		int todayRewardProgress = giftPackage.TodayRewardProgress;
		int hasFinishDay = giftPackage.HasFinishDay;
		for (int i = 0; i < 7; i++)
		{
			UIStore_Com_Day7Gift_PVE_Reward tab = GetTab(i);
			if (giftPackage.HasPurchase())
			{
				tab.stateType.selectedIndex = ((hasFinishDay > i) ? 2 : ((todayRewardProgress > i) ? 1 : 0));
			}
			else
			{
				tab.stateType.selectedIndex = 0;
			}
			Day7GiftPackageGoodsConfigureItem day7GiftPackageGoodsConfigureItem = giftPackage.giftConfig.Day7GiftPackageGoodsConfigureItems[i];
			tab.txt_Day.text = $"DAY {day7GiftPackageGoodsConfigureItem.DayNumb}";
			if (day7GiftPackageGoodsConfigureItem.Reward.Count > 0)
			{
				KeyValuePair<int, int> keyValuePair = day7GiftPackageGoodsConfigureItem.Reward.ElementAt(0);
				ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
				tab.Icon.url = itemInfoConfigure.Icon;
				tab.txt_ItemNum.text = $"x{keyValuePair.Value}";
			}
		}
	}

	private UIStore_Com_Day7Gift_PVE_Reward GetTab(int index)
	{
		return index switch
		{
			0 => btn_day1, 
			1 => btn_day2, 
			2 => btn_day3, 
			3 => btn_day4, 
			4 => btn_day5, 
			5 => btn_day6, 
			6 => btn_day7, 
			_ => btn_day1, 
		};
	}

	public static UIStore_Com_Day7Gift_PVE CreateInstance()
	{
		return (UIStore_Com_Day7Gift_PVE)UIPackage.CreateObject("Store", "Store_Com_Day7Gift_PVE");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		txt_Theme = (GTextField)GetChildAt(1);
		txt_Duration = (GTextField)GetChildAt(2);
		txt_Advocacy = (GTextField)GetChildAt(3);
		btn_Purchase = (UIStore_Button_Purchase7DayGift)GetChildAt(5);
		btn_Reward = (UIStore_Button_Reward7DayGift)GetChildAt(6);
		btn_day1 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(8);
		btn_day2 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(9);
		btn_day3 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(10);
		btn_day4 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(11);
		btn_day5 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(12);
		btn_day6 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(13);
		btn_day7 = (UIStore_Com_Day7Gift_PVE_Reward)GetChildAt(14);
		Cut_in = GetTransitionAt(0);
	}
}
