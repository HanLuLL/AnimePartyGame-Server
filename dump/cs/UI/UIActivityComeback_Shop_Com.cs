using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIActivityComeback_Shop_Com : GComponent
{
	private ShopTypeData _shopTypeData;

	private List<BaseGoodsData> _goodsDatas;

	public UIActivityComeback_Shop_Com_BottomBg buttom;

	public UIActivityComeback_Shop_Com_ItemBg bg;

	public GList list_Store_Goods;

	public GTextField txt_timeTip;

	public const string URL = "ui://hconmwfcy9qna";

	public void Init(int activityId)
	{
		list_Store_Goods.SetVirtual();
		list_Store_Goods.itemRenderer = RendererGoods;
	}

	public void OnShow()
	{
		RefreshStoreList();
		RefreshShopTime();
	}

	private void RefreshShopTime()
	{
		if (txt_timeTip == null)
		{
			return;
		}
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (returnInfo == null || returnInfo.EndTime == 0L)
		{
			txt_timeTip.visible = false;
			return;
		}
		DateTime originUtcDT = TimeUtils.OriginUtcDT;
		DateTime dateTime = originUtcDT.AddSeconds(returnInfo.EndTime);
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (serverTime > dateTime)
		{
			txt_timeTip.visible = false;
			return;
		}
		txt_timeTip.text = TimeHelper.RefreshTimeText(1010, 1011, serverTime, dateTime);
		txt_timeTip.visible = true;
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(OnShopRefresh);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(OnShopRefresh);
	}

	public void ClearData()
	{
		_shopTypeData = null;
		_goodsDatas = null;
	}

	public override void Dispose()
	{
		_shopTypeData = null;
		_goodsDatas = null;
		base.Dispose();
	}

	private void RefreshStoreList()
	{
		int num = 41;
		if (!StaticConfigure.ExchangeStore.InfoDict.TryGetValue(num, out var value))
		{
			Debug.LogError($"[ActivityComebackPanel] 未找到 ComebackShop 配置: {num}");
			return;
		}
		_shopTypeData = new ShopTypeData
		{
			tabType = ShopTabType.ComebackShop,
			tabName = value.NameID.GetLocal(UIStringType.ExchangeStore),
			currencyBar = value.CurrencyBar,
			beginTime = value.BeginTime,
			endTime = value.EndTime
		};
		_goodsDatas = SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsByShopType(ShopTabType.ComebackShop);
		_goodsDatas.Sort(ToCompare);
		list_Store_Goods.numItems = _goodsDatas.Count;
	}

	private void RendererGoods(int index, GObject item)
	{
		if (item is UIButton_GoodsItem uIButton_GoodsItem)
		{
			if (index < _goodsDatas.Count)
			{
				uIButton_GoodsItem.isEmpty.selectedIndex = 0;
				uIButton_GoodsItem.InitData((int)_shopTypeData.tabType, _goodsDatas[index]);
			}
			else
			{
				uIButton_GoodsItem.enabled = false;
				uIButton_GoodsItem.isEmpty.selectedIndex = 1;
			}
		}
	}

	private int ToCompare(BaseGoodsData x, BaseGoodsData y)
	{
		if (x.SellOut().CompareTo(y.SellOut()) != 0)
		{
			return x.SellOut().CompareTo(y.SellOut());
		}
		if (x.IsOwn().CompareTo(y.IsOwn()) != 0)
		{
			return x.IsOwn().CompareTo(y.IsOwn());
		}
		return x.goodsOrder.CompareTo(y.goodsOrder);
	}

	private void OnShopRefresh(int shopTab)
	{
		list_Store_Goods.touchable = false;
		RefreshStoreList();
		list_Store_Goods.touchable = true;
	}

	public static UIActivityComeback_Shop_Com CreateInstance()
	{
		return (UIActivityComeback_Shop_Com)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Shop_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIActivityComeback_Shop_Com_BottomBg)GetChildAt(0);
		bg = (UIActivityComeback_Shop_Com_ItemBg)GetChildAt(2);
		list_Store_Goods = (GList)GetChildAt(3);
		txt_timeTip = (GTextField)GetChildAt(4);
	}
}
