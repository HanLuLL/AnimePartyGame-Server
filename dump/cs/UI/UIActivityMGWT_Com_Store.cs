using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class UIActivityMGWT_Com_Store : GComponent
{
	private ShopTypeData _shopTypeData;

	private List<BaseGoodsData> _goodsDatas;

	public GTextField txt_timeTip;

	public GList list_Store_Goods;

	public Transition Cut_in;

	public const string URL = "ui://wdl8l4hslwm17";

	public void Init(int activityId)
	{
		if (list_Store_Goods != null)
		{
			if (!list_Store_Goods.isVirtual)
			{
				list_Store_Goods.SetVirtual();
			}
			list_Store_Goods.itemRenderer = RendererGoods;
		}
	}

	public void OnShow()
	{
		RefreshStoreList();
		RefreshShopTime();
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store?.signal?.refreshCurShelf.AddListener(OnShopRefresh);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store?.signal?.refreshCurShelf.RemoveListener(OnShopRefresh);
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
		if (list_Store_Goods == null)
		{
			return;
		}
		int num = 43;
		ExchangeStoreConfigure exchangeStore = StaticConfigure.ExchangeStore;
		if (exchangeStore?.InfoDict == null || !exchangeStore.InfoDict.TryGetValue(num, out var value) || value == null)
		{
			Debug.LogError($"[ActivityMGWTPanel] 未找到 ActivityMgwt 商店配置:{num}");
			_goodsDatas = new List<BaseGoodsData>();
			list_Store_Goods.numItems = 0;
			return;
		}
		_shopTypeData = new ShopTypeData
		{
			tabType = ShopTabType.ActivityMgwt,
			tabName = value.NameID.GetLocal(UIStringType.ExchangeStore),
			currencyBar = value.CurrencyBar,
			beginTime = value.BeginTime,
			endTime = value.EndTime
		};
		StoreLogic store = SimpleSingletonProvider<GameLogicManager>.inst.store;
		if (store == null)
		{
			Debug.LogError("[ActivityMGWTPanel] StoreLogic 未初始化");
			_goodsDatas = new List<BaseGoodsData>();
		}
		else
		{
			_goodsDatas = store.GetGoodsByShopType(ShopTabType.ActivityMgwt) ?? new List<BaseGoodsData>();
			_goodsDatas.Sort(ToCompare);
		}
		list_Store_Goods.numItems = _goodsDatas.Count;
	}

	private void RefreshShopTime()
	{
		if (txt_timeTip == null)
		{
			return;
		}
		Timestamp timestamp = _shopTypeData?.endTime;
		if (timestamp == null || timestamp.Seconds == 0L)
		{
			txt_timeTip.visible = false;
			return;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime dateTime = timestamp.ToDateTime();
		if (serverTime >= dateTime)
		{
			txt_timeTip.visible = false;
			return;
		}
		string value = TimeHelper.RefreshTimeText(1010, 1011, serverTime, dateTime);
		txt_timeTip.text = value;
		txt_timeTip.visible = !string.IsNullOrEmpty(value);
	}

	private void RendererGoods(int index, GObject item)
	{
		if (item is UIActivityMGWT_Button_GoodsItem uIActivityMGWT_Button_GoodsItem)
		{
			if (_shopTypeData == null || _goodsDatas == null || index < 0 || index >= _goodsDatas.Count)
			{
				uIActivityMGWT_Button_GoodsItem.enabled = false;
				uIActivityMGWT_Button_GoodsItem.isEmpty.selectedIndex = 1;
			}
			else
			{
				uIActivityMGWT_Button_GoodsItem.enabled = true;
				uIActivityMGWT_Button_GoodsItem.isEmpty.selectedIndex = 0;
				uIActivityMGWT_Button_GoodsItem.InitData((int)_shopTypeData.tabType, _goodsDatas[index]);
			}
		}
	}

	private static int ToCompare(BaseGoodsData x, BaseGoodsData y)
	{
		int num = x.SellOut().CompareTo(y.SellOut());
		if (num != 0)
		{
			return num;
		}
		int num2 = x.IsOwn().CompareTo(y.IsOwn());
		if (num2 != 0)
		{
			return num2;
		}
		return x.goodsOrder.CompareTo(y.goodsOrder);
	}

	private void OnShopRefresh(int shopTab)
	{
		if ((shopTab == 0 || shopTab == 43) && list_Store_Goods != null)
		{
			list_Store_Goods.touchable = false;
			RefreshStoreList();
			RefreshShopTime();
			list_Store_Goods.touchable = true;
		}
	}

	public static UIActivityMGWT_Com_Store CreateInstance()
	{
		return (UIActivityMGWT_Com_Store)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Com_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_timeTip = (GTextField)GetChildAt(1);
		list_Store_Goods = (GList)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
