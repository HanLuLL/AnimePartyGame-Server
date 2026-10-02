using System;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class UIActivityMGWT_Button_GoodsItem : GButton
{
	private BaseGoodsData _info;

	public Controller sellOut;

	public Controller isrecharge;

	public Controller owned;

	public Controller isEmpty;

	public GButton Item;

	public GTextField txt_Remaining;

	public UIActivityMGWT_Com_GoodsItemName com_ItemName;

	public GTextField txt_soldout;

	public GTextField txt_timeTip;

	public GComponent com_Price;

	public const string URL = "ui://wdl8l4hslwm1x";

	public void InitData(int shopType, BaseGoodsData info)
	{
		_info = info;
		if (!(Item is UICom_Item item))
		{
			Debug.LogError("[ActivityMGWTPanel] 商品项 Item 不是 UICom_Item");
			base.enabled = false;
			isEmpty.selectedIndex = 1;
			return;
		}
		ResetItem(item);
		if (_info?.itemConfig == null)
		{
			base.enabled = false;
			isEmpty.selectedIndex = 1;
			return;
		}
		base.enabled = true;
		isEmpty.selectedIndex = 0;
		sellOut.selectedIndex = (_info.SellOut() ? 1 : 0);
		owned.selectedIndex = (_info.IsOwn() ? 1 : 0);
		isrecharge.selectedIndex = 0;
		RefreshItem(item);
		txt_Remaining.SetVar("remainingNum", _info.RemainingNum().ToString()).SetVar("limitNum", _info.LimitNum().ToString()).FlushVars();
		txt_Remaining.visible = sellOut.selectedIndex == 0 && _info.LimitNum() != 0;
		GTextField gTextField = com_ItemName?.txt_Name;
		if (gTextField != null)
		{
			AutoSizeType autoSize = gTextField.autoSize;
			gTextField.autoSize = AutoSizeType.Both;
			gTextField.TryScrollTextField(_info.GetName());
			gTextField.autoSize = autoSize;
		}
		RefreshPrice();
		RefreshGoodsTime();
		base.touchable = owned.selectedIndex == 0 && sellOut.selectedIndex == 0;
		base.grayed = !base.touchable;
		base.onClick.Set(OpenGoodsDetail);
	}

	private void RefreshItem(UICom_Item item)
	{
		ItemInfoConfigure itemConfig = _info.itemConfig;
		item.isEmpty.selectedIndex = 0;
		item.isShowNum.selectedIndex = 0;
		item.qualityType.selectedIndex = (int)itemConfig.QualityType;
		item.loader_Icon.url = itemConfig.ShowIcon;
		item.txt_itemNum.text = _info.SingleNum().ToString();
		ItemTagConfigure itemTagConfigure = ((int)itemConfig.ItemType).GetItemTagConfigure();
		if (item.com_ItemType?.itemType != null)
		{
			item.com_ItemType.itemType.selectedIndex = itemTagConfigure?.SelectedIndex ?? 0;
		}
	}

	private static void ResetItem(UICom_Item item)
	{
		item.selected = false;
		item.touchable = false;
		item.qualityType.selectedIndex = 0;
		item.isDeleteByTime.selectedIndex = 0;
		item.isNew.selectedIndex = 0;
		item.isEmpty.selectedIndex = 1;
		item.isSelected.selectedIndex = 0;
		item.isShowNum.selectedIndex = 1;
		item.isOwn.selectedIndex = 0;
		item.loader_Icon.url = string.Empty;
		item.txt_itemNum.text = string.Empty;
		if (item.com_ItemType?.itemType != null)
		{
			item.com_ItemType.itemType.selectedIndex = 0;
		}
	}

	private void RefreshPrice()
	{
		if (com_Price is UICom_Price uICom_Price && _info != null)
		{
			int originalPrice = _info.GetOriginalPrice();
			bool flag = originalPrice > 0 && _info.SalePrice < originalPrice;
			uICom_Price.isDiscount.selectedIndex = (flag ? 1 : 0);
			uICom_Price.txt_OriginalPrice.text = originalPrice.ToString();
			if (flag)
			{
				double num = 100.0 - Math.Round((double)_info.SalePrice * 100.0 / (double)originalPrice);
				uICom_Price.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
			}
			if (_info.currencyID > 0)
			{
				string arg = _info.currencyID.GetItemInfoConfigure()?.ShowIcon ?? string.Empty;
				uICom_Price.txt_SalePrice.text = $"<img src='{arg}' width='40' height='40'/>{_info.SalePrice}";
			}
			else
			{
				uICom_Price.txt_SalePrice.text = _info.GetPriceText(_info.SalePrice);
				uICom_Price.txt_SalePrice.AddCurrencySymbols(uICom_Price.txt_SalePrice.text);
			}
		}
	}

	private async void OpenGoodsDetail(EventContext context)
	{
		if (_info == null || sellOut.selectedIndex == 1 || owned.selectedIndex == 1)
		{
			return;
		}
		StoreLogic store = SimpleSingletonProvider<GameLogicManager>.inst.store;
		if (store == null)
		{
			return;
		}
		base.onClick.Retain();
		try
		{
			if (store.GetGoodsPurchaseType(_info.shopTabType) == GoodsPurchaseType.Recharge)
			{
				await store.RequestCreateOrder(_info, 1);
			}
			else
			{
				await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(_info);
			}
		}
		finally
		{
			base.onClick.Release();
		}
	}

	private void RefreshGoodsTime()
	{
		if (_info == null)
		{
			txt_timeTip.visible = false;
			return;
		}
		string value = null;
		StoreLogic store = SimpleSingletonProvider<GameLogicManager>.inst.store;
		if (store != null && store.GetGoodsPurchaseType(_info.shopTabType) == GoodsPurchaseType.Exchange)
		{
			ExchangeGoods exchangeGoods = _info.child<ExchangeGoods>();
			if (exchangeGoods != null)
			{
				value = GetTimeText(exchangeGoods.goodsConfig.GoodsRefreshType, exchangeGoods.goodsConfig.EndTime, exchangeGoods.goodsConfig.BeginTimeLimited, exchangeGoods.goodsConfig.EndTimeLimited);
			}
		}
		else
		{
			RechargeGoods rechargeGoods = _info.child<RechargeGoods>();
			if (rechargeGoods != null)
			{
				value = GetTimeText(rechargeGoods.goodsConfig.GoodsRefreshType, rechargeGoods.goodsConfig.EndTime, rechargeGoods.goodsConfig.BeginTimeLimited, rechargeGoods.goodsConfig.EndTimeLimited);
			}
		}
		txt_timeTip.text = value;
		txt_timeTip.visible = !string.IsNullOrWhiteSpace(value);
	}

	private static string GetTimeText(GoodsRefreshType refreshType, Timestamp endTime, Timestamp beginTimeLimited, Timestamp endTimeLimited)
	{
		if (beginTimeLimited != null && endTimeLimited != null && TimeHelper.ValidityTime(beginTimeLimited, endTimeLimited))
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			return TimeHelper.RefreshTimeText(1066, 1067, serverTime, endTimeLimited.ToDateTime());
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.store?.GetRefreshTime(refreshType, endTime);
	}

	public static UIActivityMGWT_Button_GoodsItem CreateInstance()
	{
		return (UIActivityMGWT_Button_GoodsItem)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Button_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sellOut = GetControllerAt(1);
		isrecharge = GetControllerAt(2);
		owned = GetControllerAt(3);
		isEmpty = GetControllerAt(4);
		Item = (GButton)GetChildAt(1);
		txt_Remaining = (GTextField)GetChildAt(3);
		com_ItemName = (UIActivityMGWT_Com_GoodsItemName)GetChildAt(4);
		txt_soldout = (GTextField)GetChildAt(6);
		txt_timeTip = (GTextField)GetChildAt(7);
		com_Price = (GComponent)GetChildAt(8);
	}
}
