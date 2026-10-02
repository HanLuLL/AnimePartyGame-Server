using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.protocol;

namespace UI;

public class UIBoxProp_Com_OpenChest : GComponent
{
	private BoxPropWindow Window;

	private BagItem PropItem;

	private int CurrentSelectNum;

	private readonly List<KeyValuePair<int, int>> ChestItemList = new List<KeyValuePair<int, int>>();

	private UICom_Item SelectedItem;

	public Controller isMoreLeft;

	public Controller isMoreRight;

	public Controller muliti;

	public GList list_Item;

	public GTextField txt_title;

	public GTextField txt_ItemDesc;

	public GButton btn_addNum;

	public GButton btn_delNum;

	public GTextField txt_itemSelectNum;

	public GButton btn_Min;

	public GButton btn_Max;

	public GTextField txt_Num;

	public const string URL = "ui://crbpicgjaxwd10";

	public void AddEvent()
	{
		btn_addNum.onClick.Add(OnAddOne);
		btn_delNum.onClick.Add(OnDelOne);
		btn_Max.onClick.Add(OnSetMaxSelect);
		btn_Min.onClick.Add(OnSetMinSelect);
		list_Item.onClickItem.Add(OnSelectReward);
	}

	public void RemoveEvent()
	{
		btn_addNum.onClick.Remove(OnAddOne);
		btn_delNum.onClick.Remove(OnDelOne);
		btn_Max.onClick.Remove(OnSetMaxSelect);
		btn_Min.onClick.Remove(OnSetMinSelect);
		list_Item.onClickItem.Remove(OnSelectReward);
	}

	public void ShowWin(BagItem item, BoxPropWindow window)
	{
		Window = window;
		PropItem = item;
		RefreshSelectGroup(1);
		txt_Num.SetVar("num", PropItem.count.ToString()).FlushVars();
		txt_title.text = PropItem.config.Id.GetLocal(UIStringType.Item);
		ChestInfoConfigure chestInfoConfigure = PropItem.config.SubMeterID.GetChestInfoConfigure();
		muliti.selectedIndex = ((chestInfoConfigure.UseMax > 1) ? 1 : 0);
		ChestItemList.Clear();
		foreach (KeyValuePair<int, int> item2 in chestInfoConfigure.OptionalReward)
		{
			ChestItemList.Add(item2);
		}
		list_Item.itemRenderer = RendererBoxList;
		list_Item.numItems = ChestItemList.Count;
		list_Item.GetChildAt(0).onClick.Call();
	}

	private void RendererBoxList(int index, GObject item)
	{
		if (item is UICom_Item uICom_Item)
		{
			ItemInfoConfigure itemInfoConfigure = ChestItemList[index].Key.GetItemInfoConfigure();
			uICom_Item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
			uICom_Item.loader_Icon.url = itemInfoConfigure.ShowIcon;
			uICom_Item.txt_itemNum.text = ChestItemList[index].Value.ToString();
			uICom_Item.isOwn.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemInfoConfigure.Id) ? 1 : 0);
		}
	}

	private int MaxCount()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(PropItem.config.Id);
	}

	private void OnAddOne()
	{
		if (MaxCount() != 1)
		{
			btn_addNum.onClick.Retain();
			RefreshSelectGroup(Mathf.Min(MaxCount(), CurrentSelectNum + 1));
			btn_addNum.onClick.Release();
		}
	}

	private void OnDelOne()
	{
		btn_delNum.onClick.Retain();
		RefreshSelectGroup(Mathf.Max(CurrentSelectNum - 1, 1));
		btn_delNum.onClick.Release();
	}

	private void OnSetMaxSelect(EventContext context)
	{
		btn_Max.onClick.Retain();
		RefreshSelectGroup(MaxCount());
		btn_Max.onClick.Release();
	}

	private void OnSetMinSelect(EventContext context)
	{
		btn_Min.onClick.Retain();
		RefreshSelectGroup(1);
		btn_Min.onClick.Release();
	}

	private void RefreshSelectGroup(int curNum)
	{
		int num = (CurrentSelectNum = Mathf.Min(curNum, 99));
		txt_itemSelectNum.text = num.ToString();
	}

	private void OnSelectReward()
	{
		ItemInfoConfigure itemInfoConfigure = ChestItemList[list_Item.selectedIndex].Key.GetItemInfoConfigure();
		txt_ItemDesc.text = itemInfoConfigure.DescriptionID.GetLocal(UIStringType.Item);
		if (list_Item.GetChildAt(list_Item.selectedIndex) is UICom_Item selectedItem)
		{
			if (SelectedItem != null)
			{
				SelectedItem.isSelected.selectedIndex = 0;
			}
			SelectedItem = selectedItem;
			SelectedItem.isSelected.selectedIndex = 1;
		}
	}

	public void OnOpenBox(Action callback)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.UseTreasureC2S.UseTreasureC2SCall(new UseTreasureC2S
		{
			DefId = PropItem.config.Id,
			Count = CurrentSelectNum,
			SelectItemId = ChestItemList[list_Item.selectedIndex].Key
		}).OnFinishedOnly.AddOnce(callback);
	}

	public static UIBoxProp_Com_OpenChest CreateInstance()
	{
		return (UIBoxProp_Com_OpenChest)UIPackage.CreateObject("BoxProp", "BoxProp_Com_OpenChest");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isMoreLeft = GetControllerAt(0);
		isMoreRight = GetControllerAt(1);
		muliti = GetControllerAt(2);
		list_Item = (GList)GetChildAt(1);
		txt_title = (GTextField)GetChildAt(2);
		txt_ItemDesc = (GTextField)GetChildAt(4);
		btn_addNum = (GButton)GetChildAt(5);
		btn_delNum = (GButton)GetChildAt(6);
		txt_itemSelectNum = (GTextField)GetChildAt(8);
		btn_Min = (GButton)GetChildAt(9);
		btn_Max = (GButton)GetChildAt(10);
		txt_Num = (GTextField)GetChildAt(15);
	}
}
