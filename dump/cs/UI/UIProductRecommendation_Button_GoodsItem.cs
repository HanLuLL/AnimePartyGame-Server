using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIProductRecommendation_Button_GoodsItem : GButton
{
	private BaseGoodsData info;

	public Controller sellOut;

	public Controller owned;

	public Controller isEmpty;

	public GComponent com_Quality;

	public GLoader loader_Goods;

	public GTextField txt_Remaining;

	public GTextField txt_ItemName;

	public UIProductRecommendation_Com_Label com_Label;

	public GComponent com_ItemType;

	public GTextField txt_soldout;

	public UIProductRecommendation_Com_Price com_Price;

	public GTextField txt_ItemNum;

	public const string URL = "ui://z8uldgkyqdq5c";

	public void InitData(BaseGoodsData _info)
	{
		info = _info;
		((UICom_ItemType)com_ItemType).itemType.selectedIndex = ((int)_info.itemConfig.ItemType).GetItemTagConfigure().SelectedIndex;
		com_Price.free.selectedIndex = ((_info.SalePrice == 0) ? 1 : 0);
		com_Price.txt_DiscountPrice.text = _info.SalePrice.ToString();
		com_Price.txt_OriginalPrice.text = _info.GetOriginalPrice().ToString();
		bool flag = (double)Math.Abs(_info.SalePrice - _info.GetOriginalPrice()) > 0.001;
		com_Price.isDiscount.selectedIndex = (flag ? 1 : 0);
		if (flag)
		{
			double num = 100.0 - Math.Round((double)_info.SalePrice * 100.0 / (double)_info.GetOriginalPrice());
			com_Price.txt_DiscountPercent.SetVar("discount", num.ToString()).FlushVars();
		}
		sellOut.selectedIndex = (_info.SellOut() ? 1 : 0);
		if (info.currencyID > 0)
		{
			com_Price.isToken.selectedIndex = 0;
			com_Price.loader_Icon.url = info.currencyID.GetItemInfoConfigure().ShowIcon;
		}
		else
		{
			com_Price.isToken.selectedIndex = 1;
		}
		((UICom_GoodsQuality)com_Quality).quality.selectedIndex = (int)_info.itemConfig.QualityType;
		txt_Remaining.SetVar("remainingNum", _info.RemainingNum().ToString()).SetVar("limitNum", _info.LimitNum().ToString()).FlushVars();
		txt_Remaining.visible = sellOut.selectedIndex == 0 && _info.LimitNum() != 0;
		txt_ItemNum.text = _info.SingleNum().ToString();
		com_Label.labelController.selectedIndex = _info.label;
		loader_Goods.url = _info.itemConfig.ShowIcon;
		txt_ItemName.text = info.GetName();
		owned.selectedIndex = (_info.IsOwn() ? 1 : 0);
		base.touchable = owned.selectedIndex == 0 && sellOut.selectedIndex == 0;
		base.grayed = !base.touchable;
		base.onClick.Set(OpenGoodsDetail);
	}

	private async void OpenGoodsDetail()
	{
		if (sellOut.selectedIndex != 1 && owned.selectedIndex != 1)
		{
			base.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(info);
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			base.onClick.Release();
		}
	}

	public static UIProductRecommendation_Button_GoodsItem CreateInstance()
	{
		return (UIProductRecommendation_Button_GoodsItem)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Button_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sellOut = GetControllerAt(1);
		owned = GetControllerAt(2);
		isEmpty = GetControllerAt(3);
		com_Quality = (GComponent)GetChildAt(0);
		loader_Goods = (GLoader)GetChildAt(1);
		txt_Remaining = (GTextField)GetChildAt(2);
		txt_ItemName = (GTextField)GetChildAt(3);
		com_Label = (UIProductRecommendation_Com_Label)GetChildAt(4);
		com_ItemType = (GComponent)GetChildAt(6);
		txt_soldout = (GTextField)GetChildAt(7);
		com_Price = (UIProductRecommendation_Com_Price)GetChildAt(8);
		txt_ItemNum = (GTextField)GetChildAt(12);
	}
}
