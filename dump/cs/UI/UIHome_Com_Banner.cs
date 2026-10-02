using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIHome_Com_Banner : GComponent
{
	private float autoScrollTime;

	public bool startScrollStatus;

	private readonly List<BannerInfoConfigure> bannerConfigList = new List<BannerInfoConfigure>();

	public GList list_Activity;

	public GList list_Page;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgursc721";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Activity.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Activity.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Activity.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		list_Activity.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Activity.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		list_Activity.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Activity.SetVirtualAndLoop();
		list_Activity.scrollPane.decelerationRate = 0.05f;
		list_Activity.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		bannerConfigList.Clear();
		foreach (BannerInfoConfigure info in StaticConfigure.Banner.Infos)
		{
			if (TimeHelper.ValidityTime(info.BeginTime, info.EndTime) && info.LanguageType.Contains(GameSettings.languageType) && CheckBannerByWay(info))
			{
				bannerConfigList.Add(info);
			}
		}
		bannerConfigList.Sort((BannerInfoConfigure xBanner, BannerInfoConfigure yBanner) => xBanner.OrderWeight.CompareTo(yBanner.OrderWeight));
		list_Activity.numItems = bannerConfigList.Count;
		list_Page.numItems = bannerConfigList.Count;
		startScrollStatus = true;
		list_Activity.scrollPane.onScroll.Call();
	}

	public void AddEvent()
	{
		list_Activity.scrollPane.onScroll.Add(ScrollActivity);
		list_Activity.onTouchBegin.Add(StopAutoScroll);
		list_Activity.onTouchEnd.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Activity.scrollPane.onScroll.Remove(ScrollActivity);
		list_Activity.onTouchBegin.Remove(StopAutoScroll);
		list_Activity.onTouchEnd.Remove(StartAutoScroll);
	}

	private void ScrollActivity(EventContext context)
	{
		if (list_Activity.numItems > 0)
		{
			int selectedIndex = list_Activity.scrollPane.currentPageX % list_Activity.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private void RefreshActivityButton(int index, GObject item)
	{
		UIHome_Button_Banner btn = item as UIHome_Button_Banner;
		if (btn == null)
		{
			return;
		}
		RepeatedField<string> bannerImages = bannerConfigList[index].BannerImages;
		if (GameSettings.angelMode && bannerConfigList[index].BannerImagesSFW.Count > 0)
		{
			bannerImages = bannerConfigList[index].BannerImagesSFW;
		}
		btn.loader_Image.url = GetBanner(bannerImages);
		if (bannerConfigList[index].DescId1 != 0)
		{
			btn.txt_Title_1.text = bannerConfigList[index].DescId1.GetLocal(UIStringType.Banner);
			btn.txt_Title_3.text = bannerConfigList[index].DescId1.GetLocal(UIStringType.Banner);
			btn.txt_Title_1.visible = true;
			btn.txt_Title_3.visible = true;
		}
		else
		{
			btn.txt_Title_1.visible = false;
			btn.txt_Title_3.visible = false;
		}
		if (bannerConfigList[index].DescId2 != 0)
		{
			btn.txt_Title_2.text = bannerConfigList[index].DescId2.GetLocal(UIStringType.Banner);
			btn.txt_Title_2.visible = true;
		}
		else
		{
			btn.txt_Title_2.visible = false;
		}
		int selectedIndex = ((bannerConfigList[index].Style != 0) ? (bannerConfigList[index].Style - 1) : 0);
		btn.style.selectedIndex = selectedIndex;
		btn.onClick.Set((EventCallback0)delegate
		{
			SkipTargetPanel(btn, bannerConfigList[index]);
		});
		if (bannerConfigList[index].MoneySwitch && StaticConfigure.Way.DataDict.TryGetValue(bannerConfigList[index].Way, out var value))
		{
			if (!StaticConfigure.Way.InfoDict.TryGetValue((int)value.WayType, out var value2) || value2.PanelType != UIPanelType.Store || value.WayParam.Count <= 1)
			{
				return;
			}
			int num = value.WayParam[0];
			int num2 = value.WayParam[1];
			switch (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType((ShopTabType)num))
			{
			case GoodsPurchaseType.Recharge:
			{
				RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(num, num2);
				if (rechargeGoodsByShopTypeAndGoodsID != null)
				{
					btn.txt_Price.text = rechargeGoodsByShopTypeAndGoodsID.GetDiscountPriceText();
					btn.txt_Price.AddCurrencySymbols(btn.txt_Price.text);
					btn.txt_Price.visible = true;
				}
				break;
			}
			case GoodsPurchaseType.Exchange:
			{
				ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID(num, num2);
				if (exchangeGoodsByShopTypeAndGoodsID != null)
				{
					string showIcon = exchangeGoodsByShopTypeAndGoodsID.currencyID.GetItemInfoConfigure().ShowIcon;
					btn.txt_Price.text = 1078.GetLocal(UIStringType.Message) + "<img src='" + showIcon + "' width='60' height='60'/>" + exchangeGoodsByShopTypeAndGoodsID.SalePrice;
					btn.txt_Price.visible = true;
				}
				break;
			}
			}
		}
		else
		{
			btn.txt_Price.visible = false;
		}
	}

	private async void SkipTargetPanel(UIHome_Button_Banner btn, BannerInfoConfigure _info)
	{
		if (StaticConfigure.Way.DataDict.TryGetValue(_info.Way, out var _) && SimpleSingletonProvider<UIManager>.inst.GoWayAvailable(_info.Way))
		{
			btn.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(_info.Way);
			btn.onClick.Release();
		}
	}

	private string GetBanner(RepeatedField<string> BannerImages)
	{
		return GameSettings.GetDataForLanguage(BannerImages[1], BannerImages[2], BannerImages[0], BannerImages[3]);
	}

	private bool CheckBannerByWay(BannerInfoConfigure infoConfigure)
	{
		if (infoConfigure == null || infoConfigure.Way == 0)
		{
			return true;
		}
		if (!StaticConfigure.Way.DataDict.TryGetValue(infoConfigure.Way, out var value))
		{
			return true;
		}
		if (value.WayType == WayType.Mall)
		{
			ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
			if (exchangeGoodsByShopTypeAndGoodsID != null)
			{
				if (!exchangeGoodsByShopTypeAndGoodsID.SellOut())
				{
					return !exchangeGoodsByShopTypeAndGoodsID.IsOwn();
				}
				return false;
			}
			RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
			if (rechargeGoodsByShopTypeAndGoodsID != null)
			{
				if (!rechargeGoodsByShopTypeAndGoodsID.SellOut())
				{
					return !rechargeGoodsByShopTypeAndGoodsID.IsOwn();
				}
				return false;
			}
		}
		return true;
	}

	public static UIHome_Com_Banner CreateInstance()
	{
		return (UIHome_Com_Banner)UIPackage.CreateObject("Home", "Home_Com_Banner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Activity = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
