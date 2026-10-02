using System;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace UI;

public class UIStore_Item_GiftPackage : GButton
{
	public BaseGoodsData info;

	public Controller sellOut;

	public Controller quality;

	public GLoader loader_Goods;

	public GTextField text_itemName;

	public GImage newLbale;

	public GTextField text_origami;

	public GGroup origami;

	public GTextField txt_Remaining;

	public GComponent com_Price;

	public GTextField txt_timeTip;

	public GTextField txt_soldout;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00vs31qq50";

	public void InitData(BaseGoodsData _info)
	{
		ExchangeGoods exchangeGoods = _info.child<ExchangeGoods>();
		if (exchangeGoods != null)
		{
			info = _info;
			ExchangeStoreGoodsConfigureItem goodsConfig = exchangeGoods.goodsConfig;
			text_itemName.text = info.GetName();
			loader_Goods.url = (string.IsNullOrEmpty(goodsConfig.RechargeIcon) ? info.itemConfig.ShowIcon : goodsConfig.RechargeIcon);
			quality.selectedIndex = (int)_info.itemConfig.QualityType;
			UICom_Price uICom_Price = (UICom_Price)com_Price;
			string showIcon = info.currencyID.GetItemInfoConfigure().ShowIcon;
			uICom_Price.txt_SalePrice.text = "<img src='" + showIcon + "' width='40' height='40'/>" + _info.SalePrice;
			bool flag = _info.SalePrice < _info.GetOriginalPrice();
			uICom_Price.isDiscount.selectedIndex = (flag ? 1 : 0);
			if (flag)
			{
				double num = 100.0 - Math.Round((double)_info.SalePrice * 100.0 / (double)_info.GetOriginalPrice());
				uICom_Price.txt_DiscountPercent.SetVar("discount", num.ToString("f0")).FlushVars();
				uICom_Price.txt_OriginalPrice.text = _info.GetOriginalPrice().ToString();
			}
			sellOut.selectedIndex = (info.SellOut() ? 1 : 0);
			text_origami.text = $"{goodsConfig.Great}%";
			origami.visible = goodsConfig.Great > 0;
			string timeTxt = GetTimeTxt(exchangeGoods.goodsConfig.GoodsRefreshType, exchangeGoods.goodsConfig.EndTime, exchangeGoods.goodsConfig.BeginTimeLimited, exchangeGoods.goodsConfig.EndTimeLimited);
			txt_timeTip.text = timeTxt;
			txt_timeTip.visible = !string.IsNullOrWhiteSpace(timeTxt);
			txt_Remaining.SetVar("remainingNum", _info.RemainingNum().ToString()).SetVar("limitNum", _info.LimitNum().ToString()).FlushVars();
			txt_Remaining.visible = sellOut.selectedIndex == 0 && _info.LimitNum() != 0;
			newLbale.visible = goodsConfig.GoodsLabelType == GoodsLabelType.New;
			base.onClick.Set(OpenGoodsDetail);
			base.touchable = sellOut.selectedIndex == 0;
			base.grayed = !base.touchable;
		}
	}

	private void OpenGoodsDetail(EventContext context)
	{
		if (sellOut.selectedIndex != 1)
		{
			base.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(info).Forget();
			base.onClick.Release();
		}
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

	public static UIStore_Item_GiftPackage CreateInstance()
	{
		return (UIStore_Item_GiftPackage)UIPackage.CreateObject("Store", "Store_Item_GiftPackage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sellOut = GetControllerAt(1);
		quality = GetControllerAt(2);
		loader_Goods = (GLoader)GetChildAt(8);
		text_itemName = (GTextField)GetChildAt(9);
		newLbale = (GImage)GetChildAt(10);
		text_origami = (GTextField)GetChildAt(12);
		origami = (GGroup)GetChildAt(14);
		txt_Remaining = (GTextField)GetChildAt(15);
		com_Price = (GComponent)GetChildAt(16);
		txt_timeTip = (GTextField)GetChildAt(17);
		txt_soldout = (GTextField)GetChildAt(18);
		Cut_in = GetTransitionAt(0);
	}
}
