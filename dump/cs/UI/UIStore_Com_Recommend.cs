using System;
using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIStore_Com_Recommend : GComponent
{
	private ShopTypeData typeData;

	private List<RechargeStoreAdsAdsConfigureItem> recommendAds;

	private int curIndex;

	public GLoader loader_BG;

	public UIStore_Com_RecommendEntity com_Entity;

	public UIStore_Button_Recommend btn_GoWay;

	public const string URL = "ui://zyd0rl007h6sqq34";

	public void InitComponents()
	{
		com_Entity.ShowDown.SetHook("Refresh", RefreshItems);
	}

	public void Refresh(ShopTypeData _typeData)
	{
		typeData = _typeData;
		RechargeStoreShelfConfigure rechargeStoreShelfConfigure = StaticConfigure.RechargeStore.ShelfDict[typeData.ShelfId];
		loader_BG.Background(rechargeStoreShelfConfigure.Background);
		recommendAds = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRecommendAdsData();
		curIndex = 0;
		RefreshItems();
	}

	public void AddEvent()
	{
		com_Entity.onClick.Add(ShowDown);
		btn_GoWay.onClick.Add(GoWay);
	}

	public void RemoveEvent()
	{
		btn_GoWay.onClick.Remove(GoWay);
		com_Entity.onClick.Remove(ShowDown);
	}

	private void ShowDown(EventContext context)
	{
		com_Entity.onClick.Retain();
		curIndex = (curIndex + 1) % recommendAds.Count;
		com_Entity.ShowDown.Play(delegate
		{
			com_Entity.onClick.Release();
		});
	}

	private void RefreshItems()
	{
		RendererItem(com_Entity.btn_First, curIndex);
		RendererItem(com_Entity.btn_Second, curIndex + 1);
		RendererItem(com_Entity.btn_Third, curIndex + 2);
		RefreshPurchaseBtn();
	}

	private void RendererItem(UIStore_Button_RecommendItem btn, int index)
	{
		int index2 = index % recommendAds.Count;
		btn.icon = GetImage(index2);
	}

	private string GetImage(int index)
	{
		RechargeStoreAdsAdsConfigureItem rechargeStoreAdsAdsConfigureItem = recommendAds[index];
		return GameSettings.GetDataForLanguage(rechargeStoreAdsAdsConfigureItem.BackgroundEN, rechargeStoreAdsAdsConfigureItem.BackgroundJP, rechargeStoreAdsAdsConfigureItem.BackgroundCN, rechargeStoreAdsAdsConfigureItem.BackgroundTC);
	}

	private async void GoWay()
	{
		btn_GoWay.onClick.Retain();
		RechargeStoreAdsAdsConfigureItem rechargeStoreAdsAdsConfigureItem = recommendAds[curIndex];
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(rechargeStoreAdsAdsConfigureItem.Way);
		btn_GoWay.onClick.Release();
	}

	private void RefreshPurchaseBtn()
	{
		RechargeStoreAdsAdsConfigureItem rechargeStoreAdsAdsConfigureItem = recommendAds[curIndex];
		if (!StaticConfigure.Way.DataDict.TryGetValue(rechargeStoreAdsAdsConfigureItem.Way, out var value))
		{
			return;
		}
		if (value.WayParam.Count < 2)
		{
			Debug.LogError("无法拿到当前广告跳转的信息");
			return;
		}
		RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
		if (rechargeGoodsByShopTypeAndGoodsID == null)
		{
			Debug.LogError($"无法拿到当前商品{value.WayParam[1]}信息");
			return;
		}
		btn_GoWay.touchable = !rechargeGoodsByShopTypeAndGoodsID.SellOut() && !rechargeGoodsByShopTypeAndGoodsID.IsOwn();
		btn_GoWay.grayed = !btn_GoWay.touchable;
		btn_GoWay.txt_DiscountPrice.text = rechargeGoodsByShopTypeAndGoodsID.GetDiscountPriceText();
		btn_GoWay.txt_OriginalPrice.text = rechargeGoodsByShopTypeAndGoodsID.GetOriginalPriceText();
		bool flag = (double)Math.Abs(rechargeGoodsByShopTypeAndGoodsID.SalePrice - rechargeGoodsByShopTypeAndGoodsID.GetOriginalPrice()) > 0.001;
		btn_GoWay.isDiscount.selectedIndex = (flag ? 1 : 0);
		if (flag)
		{
			double num = 100.0 - Math.Round((double)rechargeGoodsByShopTypeAndGoodsID.SalePrice * 100.0 / (double)rechargeGoodsByShopTypeAndGoodsID.GetOriginalPrice());
			btn_GoWay.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
		}
		string refreshTime = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRefreshTime(rechargeGoodsByShopTypeAndGoodsID.goodsConfig.GoodsRefreshType, rechargeGoodsByShopTypeAndGoodsID.goodsConfig.EndTime);
		if (string.IsNullOrWhiteSpace(refreshTime))
		{
			btn_GoWay.txt_Time.visible = false;
		}
		btn_GoWay.txt_Time.text = refreshTime;
		btn_GoWay.txt_Time.visible = true;
	}

	public static UIStore_Com_Recommend CreateInstance()
	{
		return (UIStore_Com_Recommend)UIPackage.CreateObject("Store", "Store_Com_Recommend");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_BG = (GLoader)GetChildAt(0);
		com_Entity = (UIStore_Com_RecommendEntity)GetChildAt(1);
		btn_GoWay = (UIStore_Button_Recommend)GetChildAt(2);
	}
}
