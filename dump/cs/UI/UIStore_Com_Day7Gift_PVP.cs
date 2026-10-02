using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIStore_Com_Day7Gift_PVP : GComponent
{
	private ShopTypeData typeData;

	private Day7GiftData giftPackage;

	public Controller language;

	public Controller status;

	public GLoader loader_BG;

	public GTextField txt_TitleDesc;

	public GTextField txt_Content;

	public UIStore_Button_Purchase7DayGift btn_Purchase;

	public UIStore_Button_Reward7DayGift btn_Reward;

	public UIStore_Button_GetStatus com_GetStatus;

	public UIStore_Com_7GiftRewardTab com_One;

	public UIStore_Com_7GiftRewardTab com_Two;

	public UIStore_Com_7GiftRewardTab com_Three;

	public UIStore_Com_7GiftRewardTab com_Four;

	public UIStore_Com_7GiftRewardTab com_Five;

	public UIStore_Com_7GiftRewardTab com_Six;

	public UIStore_Com_7GiftRewardTab com_Seven;

	public UIStore_Button_FinishStatus com_FinishStatus;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl0011biqu";

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
		switch (GameSettings.languageType)
		{
		case LanguageType.SimplifiedChinese:
			language.selectedIndex = 0;
			break;
		case LanguageType.English:
			language.selectedIndex = 1;
			break;
		case LanguageType.Japanese:
			language.selectedIndex = 2;
			break;
		case LanguageType.TraditionalChinese:
			language.selectedIndex = 1;
			break;
		default:
			Debug.LogError($"七日礼包界面未处理该语言：{GameSettings.languageType}");
			break;
		}
		status.selectedIndex = (giftPackage.HasPurchase() ? 1 : 0);
		txt_TitleDesc.text = giftPackage.giftConfig.TitleID.GetLocal(UIStringType.Day7GiftPackage);
		txt_Content.text = giftPackage.giftConfig.DescriptionID.GetLocal(UIStringType.Day7GiftPackage);
		RefreshPurchaseBtn();
		RefreshRewardBtn();
		RefreshProgress();
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(giftPackage.goodsId, ShopTabType.Day7GiftPackage, LogToServerType.GOODS_DETAIL);
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
			btn_Purchase.txt_Price.text = "$[color=#FFF32B]" + giftPackage.GetDiscountPriceText() + "[/color]";
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
		com_GetStatus.com_progress.status.selectedIndex = giftPackage.TodayRewardProgress;
		com_GetStatus.FinishStatus.selectedIndex = giftPackage.HasFinishDay;
		com_FinishStatus.status.selectedIndex = giftPackage.HasFinishDay;
		for (int i = 0; i < 7; i++)
		{
			UIStore_Com_7GiftRewardTab tab = GetTab(i);
			if (giftPackage.HasPurchase())
			{
				tab.GetStatus.selectedIndex = ((giftPackage.TodayRewardProgress > i) ? 1 : 0);
				tab.nextStatus.selectedIndex = ((giftPackage.TodayRewardProgress == i) ? 1 : 0);
			}
			else
			{
				tab.GetStatus.selectedIndex = 0;
				tab.nextStatus.selectedIndex = 0;
			}
			Day7GiftPackageGoodsConfigureItem day7GiftPackageGoodsConfigureItem = giftPackage.giftConfig.Day7GiftPackageGoodsConfigureItems[i];
			tab.txt_Day.SetVar("day", day7GiftPackageGoodsConfigureItem.DayNumb.ToString()).FlushVars();
			if (tab.day.selectedIndex == 1)
			{
				if (day7GiftPackageGoodsConfigureItem.Reward.Count > 0)
				{
					KeyValuePair<int, int> keyValuePair = day7GiftPackageGoodsConfigureItem.Reward.ElementAt(0);
					tab.txt_Name_2.text = keyValuePair.Key.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item) + keyValuePair.Value;
				}
				continue;
			}
			if (day7GiftPackageGoodsConfigureItem.Reward.Count > 0)
			{
				KeyValuePair<int, int> keyValuePair2 = day7GiftPackageGoodsConfigureItem.Reward.ElementAt(0);
				tab.txt_Name_0.text = keyValuePair2.Key.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item) + keyValuePair2.Value;
			}
			if (day7GiftPackageGoodsConfigureItem.Reward.Count > 1)
			{
				KeyValuePair<int, int> keyValuePair3 = day7GiftPackageGoodsConfigureItem.Reward.ElementAt(1);
				tab.txt_Name_1.text = keyValuePair3.Key.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item) + keyValuePair3.Value;
			}
		}
	}

	private UIStore_Com_7GiftRewardTab GetTab(int index)
	{
		return index switch
		{
			0 => com_One, 
			1 => com_Two, 
			2 => com_Three, 
			3 => com_Four, 
			4 => com_Five, 
			5 => com_Six, 
			6 => com_Seven, 
			_ => com_One, 
		};
	}

	public static UIStore_Com_Day7Gift_PVP CreateInstance()
	{
		return (UIStore_Com_Day7Gift_PVP)UIPackage.CreateObject("Store", "Store_Com_Day7Gift_PVP");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		status = GetControllerAt(1);
		loader_BG = (GLoader)GetChildAt(0);
		txt_TitleDesc = (GTextField)GetChildAt(6);
		txt_Content = (GTextField)GetChildAt(7);
		btn_Purchase = (UIStore_Button_Purchase7DayGift)GetChildAt(11);
		btn_Reward = (UIStore_Button_Reward7DayGift)GetChildAt(12);
		com_GetStatus = (UIStore_Button_GetStatus)GetChildAt(16);
		com_One = (UIStore_Com_7GiftRewardTab)GetChildAt(24);
		com_Two = (UIStore_Com_7GiftRewardTab)GetChildAt(25);
		com_Three = (UIStore_Com_7GiftRewardTab)GetChildAt(26);
		com_Four = (UIStore_Com_7GiftRewardTab)GetChildAt(27);
		com_Five = (UIStore_Com_7GiftRewardTab)GetChildAt(28);
		com_Six = (UIStore_Com_7GiftRewardTab)GetChildAt(29);
		com_Seven = (UIStore_Com_7GiftRewardTab)GetChildAt(30);
		com_FinishStatus = (UIStore_Button_FinishStatus)GetChildAt(33);
		Cut_in = GetTransitionAt(0);
	}
}
