using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIStore_Com_CharacterGift : GComponent
{
	private RechargeStoreAdsAdsConfigureItem adsConfig;

	private ShopTypeData typeData;

	public Controller country;

	public GLoader loader_BG;

	public GTextField txt_Desc;

	public UIStore_Button_Way btn_way;

	public Transition Cut_in;

	public Transition StarLoop;

	public const string URL = "ui://zyd0rl0011biq1s";

	public void InitComponents()
	{
	}

	public void Refresh(ShopTypeData _typeData)
	{
		typeData = _typeData;
		RechargeStoreShelfConfigure rechargeStoreShelfConfigure = StaticConfigure.RechargeStore.ShelfDict[typeData.ShelfId];
		loader_BG.Background(rechargeStoreShelfConfigure.Background);
		adsConfig = StaticConfigure.RechargeStoreAds.NoviceGiftPackage;
		country.selectedIndex = GameSettings.COUNTRY;
		if (adsConfig != null)
		{
			base.visible = true;
			if (!StaticConfigure.Way.DataDict.TryGetValue(adsConfig.Way, out var value) || value.WayType != WayType.Mall)
			{
				return;
			}
			ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID(value.WayParam[0], value.WayParam[1]);
			if (exchangeGoodsByShopTypeAndGoodsID == null)
			{
				Debug.LogError($"ExchangeStore 未找到商品数据: shopTabType:{value.WayParam[0]} goodsId:{value.WayParam[1]}");
				return;
			}
			btn_way.touchable = !exchangeGoodsByShopTypeAndGoodsID.SellOut() && !exchangeGoodsByShopTypeAndGoodsID.IsOwn();
			if (exchangeGoodsByShopTypeAndGoodsID.currencyID > 0)
			{
				string showIcon = exchangeGoodsByShopTypeAndGoodsID.currencyID.GetItemInfoConfigure().ShowIcon;
				btn_way.txt_DiscountPrice.text = "<img src='" + showIcon + "' width='100' height='100'/>" + exchangeGoodsByShopTypeAndGoodsID.SalePrice;
			}
			btn_way.grayed = !btn_way.touchable;
			txt_Desc.text = adsConfig.DescriptionID.GetLocal(UIStringType.RechargeStoreAds);
		}
		else
		{
			base.visible = false;
		}
	}

	public void AddEvent()
	{
		btn_way.onClick.Add(GoWay);
	}

	public void RemoveEvent()
	{
		btn_way.onClick.Remove(GoWay);
	}

	private async void GoWay()
	{
		btn_way.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(adsConfig.Way);
		btn_way.onClick.Release();
	}

	public static UIStore_Com_CharacterGift CreateInstance()
	{
		return (UIStore_Com_CharacterGift)UIPackage.CreateObject("Store", "Store_Com_CharacterGift");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		country = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		txt_Desc = (GTextField)GetChildAt(1);
		btn_way = (UIStore_Button_Way)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
		StarLoop = GetTransitionAt(1);
	}
}
