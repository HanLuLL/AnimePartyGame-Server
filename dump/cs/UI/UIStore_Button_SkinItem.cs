using System;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace UI;

public class UIStore_Button_SkinItem : GButton
{
	private BaseGoodsData info;

	public Controller sellOut;

	public Controller owned;

	public Controller isDiscountLimited;

	public GComponent com_ItemType;

	public GLabel loader_Skin;

	public GTextField txt_ItemName;

	public GTextField txt_Remaining;

	public UIStore_Com_Label com_Label;

	public GComponent com_Price;

	public GTextField txt_DiscountLimitedTime;

	public GTextField txt_soldout;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00qdq5r";

	public void InitData(int _shopType, BaseGoodsData _info)
	{
		info = _info;
		((UICom_ItemType)com_ItemType).itemType.selectedIndex = ((int)_info.itemConfig.ItemType).GetItemTagConfigure().SelectedIndex;
		bool flag = _info.SalePrice < _info.GetOriginalPrice();
		UICom_Price uICom_Price = (UICom_Price)com_Price;
		uICom_Price.isDiscount.selectedIndex = (flag ? 1 : 0);
		if (flag)
		{
			double num = 100.0 - Math.Round((double)_info.SalePrice * 100.0 / (double)_info.GetOriginalPrice());
			uICom_Price.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
		}
		sellOut.selectedIndex = (_info.SellOut() ? 1 : 0);
		if (info.currencyID > 0)
		{
			string showIcon = info.currencyID.GetItemInfoConfigure().ShowIcon;
			uICom_Price.txt_SalePrice.text = "<img src='" + showIcon + "' width='40' height='40'/>" + _info.SalePrice;
		}
		else
		{
			uICom_Price.txt_SalePrice.text = _info.GetPriceText(_info.SalePrice);
			uICom_Price.txt_SalePrice.AddCurrencySymbols(uICom_Price.txt_SalePrice.text);
		}
		txt_Remaining.SetVar("remainingNum", _info.RemainingNum().ToString()).SetVar("limitNum", _info.LimitNum().ToString()).FlushVars();
		txt_Remaining.visible = sellOut.selectedIndex == 0 && _info.LimitNum() != 0;
		com_Label.labelController.selectedIndex = _info.label;
		string characterReady = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinInfoByGoodsInfo(_info).GetCharacterReady();
		if (_info is ExchangeGoods exchangeGoods)
		{
			loader_Skin.icon = ((!string.IsNullOrEmpty(characterReady)) ? characterReady : exchangeGoods.goodsConfig.RechargeIcon);
			txt_ItemName.text = exchangeGoods.GetName();
			uICom_Price.txt_OriginalPrice.text = _info.GetOriginalPrice().ToString();
			RefreshLimitPriceTxt(exchangeGoods.goodsConfig.BeginTimeLimited, exchangeGoods.goodsConfig.EndTimeLimited);
		}
		else if (_info is RechargeGoods rechargeGoods)
		{
			loader_Skin.icon = ((!string.IsNullOrEmpty(characterReady)) ? characterReady : rechargeGoods.goodsConfig.RechargeIcon);
			txt_ItemName.text = rechargeGoods.GetName();
			uICom_Price.txt_OriginalPrice.text = rechargeGoods.GetOriginalPriceText();
			RefreshLimitPriceTxt(rechargeGoods.goodsConfig.BeginTimeLimited, rechargeGoods.goodsConfig.EndTimeLimited);
		}
		owned.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.store.IsPurchaseChestGoods(_info.itemConfig.SubMeterID) ? 1 : 0);
		base.touchable = owned.selectedIndex == 0 && sellOut.selectedIndex == 0;
		base.grayed = !base.touchable;
		base.onClick.Set(OpenGoodsDetail);
	}

	private void RefreshLimitPriceTxt(Timestamp beginTimeLimited, Timestamp endTimeLimited)
	{
		if ((object)beginTimeLimited != null && (object)endTimeLimited != null && TimeHelper.ValidityTime(beginTimeLimited, endTimeLimited))
		{
			isDiscountLimited.selectedIndex = 1;
			txt_DiscountLimitedTime.text = TimeHelper.GetDurationText(beginTimeLimited, endTimeLimited, OnlyDuration: true);
		}
		else
		{
			isDiscountLimited.selectedIndex = 0;
		}
	}

	private async void OpenGoodsDetail(EventContext context)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.store.IsPurchaseChestGoods(info.itemConfig.SubMeterID))
		{
			base.onClick.Retain();
			SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(info.goodsId, info.shopTabType, LogToServerType.GOODS_DETAIL);
			await SimpleSingletonProvider<UIManager>.inst.showSkin.ShowSkinGoods(info);
			base.onClick.Release();
		}
	}

	public static UIStore_Button_SkinItem CreateInstance()
	{
		return (UIStore_Button_SkinItem)UIPackage.CreateObject("Store", "Store_Button_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sellOut = GetControllerAt(1);
		owned = GetControllerAt(2);
		isDiscountLimited = GetControllerAt(3);
		com_ItemType = (GComponent)GetChildAt(1);
		loader_Skin = (GLabel)GetChildAt(2);
		txt_ItemName = (GTextField)GetChildAt(4);
		txt_Remaining = (GTextField)GetChildAt(5);
		com_Label = (UIStore_Com_Label)GetChildAt(6);
		com_Price = (GComponent)GetChildAt(7);
		txt_DiscountLimitedTime = (GTextField)GetChildAt(9);
		txt_soldout = (GTextField)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
