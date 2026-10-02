using System;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace UI;

public class UIButton_GoodsItem : GButton
{
	public BaseGoodsData info;

	public Controller sellOut;

	public Controller isrecharge;

	public Controller owned;

	public Controller isEmpty;

	public UICom_GoodsQuality com_Quality;

	public GLoader loader_Goods;

	public GRichTextField txt_FirstRecharge;

	public GTextField txt_Remaining;

	public UICom_GoodsItemName com_ItemName;

	public UICom_Label com_Label;

	public UICom_ItemType com_ItemType;

	public GTextField txt_soldout;

	public GTextField txt_timeTip;

	public UICom_Price com_Price;

	public GTextField txt_ItemNum;

	public const string URL = "ui://m6sn3r22ot0w0";

	public void InitData(int _shopType, BaseGoodsData _info)
	{
		info = _info;
		com_ItemType.itemType.selectedIndex = ((int)_info.itemConfig.ItemType).GetItemTagConfigure().SelectedIndex;
		bool flag = _info.SalePrice < _info.GetOriginalPrice();
		com_Price.isDiscount.selectedIndex = (flag ? 1 : 0);
		if (flag)
		{
			double num = 100.0 - Math.Round((double)_info.SalePrice * 100.0 / (double)_info.GetOriginalPrice());
			com_Price.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
		}
		sellOut.selectedIndex = (_info.SellOut() ? 1 : 0);
		com_Quality.quality.selectedIndex = (int)_info.itemConfig.QualityType;
		txt_Remaining.SetVar("remainingNum", _info.RemainingNum().ToString()).SetVar("limitNum", _info.LimitNum().ToString()).FlushVars();
		txt_Remaining.visible = sellOut.selectedIndex == 0 && _info.LimitNum() != 0;
		txt_ItemNum.text = _info.SingleNum().ToString();
		com_Label.labelController.selectedIndex = _info.label;
		if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType((ShopTabType)_shopType) == GoodsPurchaseType.Recharge)
		{
			RechargeGoods rechargeGoods = _info.child<RechargeGoods>();
			ItemInfoConfigure itemInfoConfigure = GameSettings.SPECIAL_ITEM_STARDISC_FREE.GetItemInfoConfigure();
			txt_FirstRecharge.text = string.Format(1086.GetLocal(UIStringType.Message), rechargeGoods.GetGoodsBonuses(), "<img src='" + itemInfoConfigure.ShowIcon + "' width='18' height='18'/>");
			txt_FirstRecharge.visible = !_info.HasPurchase();
			GTextField txt_Name = com_ItemName.txt_Name;
			AutoSizeType autoSize = txt_Name.autoSize;
			txt_Name.autoSize = AutoSizeType.Both;
			txt_Name.TryScrollTextField(rechargeGoods.GetName());
			txt_Name.autoSize = autoSize;
			loader_Goods.url = (string.IsNullOrEmpty(rechargeGoods.goodsConfig.RechargeIcon) ? _info.itemConfig.ShowIcon : rechargeGoods.goodsConfig.RechargeIcon);
			com_Price.txt_OriginalPrice.text = rechargeGoods.GetOriginalPriceText();
		}
		else
		{
			loader_Goods.url = _info.itemConfig.ShowIcon;
			GTextField txt_Name2 = com_ItemName.txt_Name;
			AutoSizeType autoSize2 = txt_Name2.autoSize;
			txt_Name2.autoSize = AutoSizeType.Both;
			txt_Name2.TryScrollTextField(info.GetName());
			txt_Name2.autoSize = autoSize2;
			com_Price.txt_OriginalPrice.text = _info.GetOriginalPrice().ToString();
		}
		if (info.currencyID > 0)
		{
			string showIcon = info.currencyID.GetItemInfoConfigure().ShowIcon;
			com_Price.txt_SalePrice.text = "<img src='" + showIcon + "' width='40' height='40'/>" + _info.SalePrice;
		}
		else
		{
			com_Price.txt_SalePrice.text = _info.GetPriceText(_info.SalePrice);
			com_Price.txt_SalePrice.AddCurrencySymbols(com_Price.txt_SalePrice.text);
		}
		if (_shopType == 7)
		{
			isrecharge.selectedIndex = ((!SimpleSingletonProvider<GameLogicManager>.inst.store.IsFinishFirstBug(_info.goodsId)) ? 1 : 0);
		}
		else
		{
			isrecharge.selectedIndex = 0;
		}
		owned.selectedIndex = (_info.IsOwn() ? 1 : 0);
		base.touchable = owned.selectedIndex == 0 && sellOut.selectedIndex == 0;
		base.grayed = !base.touchable;
		RefreshGoodsTime();
		base.onClick.Set(OpenGoodsDetail);
	}

	private async void OpenGoodsDetail(EventContext context)
	{
		if (sellOut.selectedIndex != 1 && owned.selectedIndex != 1)
		{
			base.onClick.Retain();
			if (info.shopTabType == ShopTabType.Recharge)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(info, 1);
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(info).Forget();
			}
			base.onClick.Release();
		}
	}

	private void RefreshGoodsTime()
	{
		string value = null;
		if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType(info.shopTabType) == GoodsPurchaseType.Exchange)
		{
			ExchangeGoods exchangeGoods = info.child<ExchangeGoods>();
			if (exchangeGoods != null)
			{
				value = GetTimeTxt(exchangeGoods.goodsConfig.GoodsRefreshType, exchangeGoods.goodsConfig.EndTime, exchangeGoods.goodsConfig.BeginTimeLimited, exchangeGoods.goodsConfig.EndTimeLimited);
			}
		}
		else
		{
			RechargeGoods rechargeGoods = info.child<RechargeGoods>();
			if (rechargeGoods != null)
			{
				value = GetTimeTxt(rechargeGoods.goodsConfig.GoodsRefreshType, rechargeGoods.goodsConfig.EndTime, rechargeGoods.goodsConfig.BeginTimeLimited, rechargeGoods.goodsConfig.EndTimeLimited);
			}
		}
		txt_timeTip.text = value;
		txt_timeTip.visible = !string.IsNullOrWhiteSpace(value);
	}

	private string GetTimeTxt(GoodsRefreshType GoodsRefreshType, Timestamp EndTime, Timestamp BeginTimeLimited, Timestamp EndTimeLimited)
	{
		if ((object)BeginTimeLimited != null && (object)EndTimeLimited != null && TimeHelper.ValidityTime(BeginTimeLimited, EndTimeLimited))
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			return TimeHelper.RefreshTimeText(1066, 1067, serverTime, EndTimeLimited.ToDateTime());
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.store.GetRefreshTime(GoodsRefreshType, EndTime);
	}

	public static UIButton_GoodsItem CreateInstance()
	{
		return (UIButton_GoodsItem)UIPackage.CreateObject("Common_External", "Button_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sellOut = GetControllerAt(1);
		isrecharge = GetControllerAt(2);
		owned = GetControllerAt(3);
		isEmpty = GetControllerAt(4);
		com_Quality = (UICom_GoodsQuality)GetChildAt(0);
		loader_Goods = (GLoader)GetChildAt(1);
		txt_FirstRecharge = (GRichTextField)GetChildAt(3);
		txt_Remaining = (GTextField)GetChildAt(4);
		com_ItemName = (UICom_GoodsItemName)GetChildAt(5);
		com_Label = (UICom_Label)GetChildAt(6);
		com_ItemType = (UICom_ItemType)GetChildAt(8);
		txt_soldout = (GTextField)GetChildAt(9);
		txt_timeTip = (GTextField)GetChildAt(10);
		com_Price = (UICom_Price)GetChildAt(11);
		txt_ItemNum = (GTextField)GetChildAt(15);
	}
}
