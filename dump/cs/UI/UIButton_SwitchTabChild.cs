using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIButton_SwitchTabChild : GButton
{
	public ShopTypeData shopTypeData;

	public Controller redPoint;

	public GGraph di;

	public const string URL = "ui://m6sn3r22o1g0c3";

	public void InitComponent_Store(ShopTypeData _shopTypeData)
	{
		shopTypeData = _shopTypeData;
		base.title = shopTypeData.tabName;
		redPoint.selectedIndex = (shopTypeData.newGoods ? 1 : 0);
	}

	public void UpdateRedStatus_Store()
	{
		if (shopTypeData.newGoods)
		{
			List<int> newGoodsByType = SimpleSingletonProvider<GameLogicManager>.inst.store.GetNewGoodsByType((int)shopTypeData.tabType);
			if (newGoodsByType.Count != 0)
			{
				LocalCache.UpdateStoreGoodsCache(newGoodsByType);
				redPoint.selectedIndex = (shopTypeData.newGoods ? 1 : 0);
				((UIButton_SwitchTab)base.treeNode.parent.cell).RefreshRedStatus_Store();
			}
		}
	}

	public static UIButton_SwitchTabChild CreateInstance()
	{
		return (UIButton_SwitchTabChild)UIPackage.CreateObject("Common_External", "Button_SwitchTabChild");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		di = (GGraph)GetChildAt(0);
	}
}
