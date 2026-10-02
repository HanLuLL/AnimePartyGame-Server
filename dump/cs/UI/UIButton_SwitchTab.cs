using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIButton_SwitchTab : GButton
{
	public StoreTabData storeTabData;

	public Controller setStatus;

	public Controller redPoint;

	public GGraph di;

	public const string URL = "ui://m6sn3r22o1g0c2";

	public void InitComponent_Store(StoreTabData _storeTabData)
	{
		storeTabData = _storeTabData;
		data = (int)storeTabData.shopTypes[0].tabType;
		base.title = storeTabData.parentName;
		RefreshRedStatus_Store();
	}

	public void UpdateRedStatus_Store(ShopTypeData shopTypeData)
	{
		if (shopTypeData.newGoods)
		{
			List<int> newGoodsByType = SimpleSingletonProvider<GameLogicManager>.inst.store.GetNewGoodsByType((int)shopTypeData.tabType);
			if (newGoodsByType.Count != 0)
			{
				LocalCache.UpdateStoreGoodsCache(newGoodsByType);
				RefreshRedStatus_Store();
			}
		}
	}

	public void RefreshRedStatus_Store()
	{
		ShopTabType tabType = storeTabData.shopTypes[0].tabType;
		if (tabType == ShopTabType.Day7GiftPackage || tabType == ShopTabType.Day7GiftPackagePve)
		{
			redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(tabType) ? 1 : 0);
		}
		else
		{
			redPoint.selectedIndex = (storeTabData.newGoods ? 1 : 0);
		}
	}

	public static UIButton_SwitchTab CreateInstance()
	{
		return (UIButton_SwitchTab)UIPackage.CreateObject("Common_External", "Button_SwitchTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		setStatus = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		di = (GGraph)GetChildAt(0);
	}
}
