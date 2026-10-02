using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UINGOStore_Com_GoodsItem : GComponent
{
	public CollaborationGoodsConfigure CollaborationGoods;

	private readonly List<int> LabelIds = new List<int>();

	private float autoScrollTime;

	private int _displayCount;

	public bool startScrollStatus;

	public Controller BGType;

	public Controller type;

	public GGraph graph_FirstTheme;

	public GGraph graph_SecondTheme;

	public GGraph graph_Skin;

	public GTextField txt_RoleTitle;

	public GList list_Labels;

	public GList list_Page;

	public GTextField txt_LabelTitle;

	public GList list_Prop;

	public UINGOStore_Button_Purchase btn_Purchase;

	public const string URL = "ui://na6sy4s6kqgj15";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Labels.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Labels.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Labels.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		list_Labels.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Labels.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		list_Labels.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Labels.SetVirtualAndLoop();
		list_Labels.scrollPane.decelerationRate = 0.05f;
		list_Labels.itemRenderer = RefreshLabelItem;
	}

	public void AddEvent()
	{
		list_Labels.scrollPane.onScroll.Add(ScrollActivity);
		list_Labels.onTouchBegin.Add(StopAutoScroll);
		list_Labels.onTouchEnd.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Labels.scrollPane.onScroll.Remove(ScrollActivity);
		list_Labels.onTouchBegin.Remove(StopAutoScroll);
		list_Labels.onTouchEnd.Remove(StartAutoScroll);
	}

	public void Close()
	{
		startScrollStatus = false;
	}

	private void ScrollActivity(EventContext context)
	{
		if (list_Labels.numItems > 0)
		{
			int selectedIndex = list_Labels.scrollPane.currentPageX % list_Labels.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	public void RefreshLabels()
	{
		if (CollaborationGoods == null)
		{
			return;
		}
		LabelIds.Clear();
		foreach (int item in CollaborationGoods.AccountBackgroundID)
		{
			LabelIds.Add(item);
		}
		_displayCount = Mathf.Max(LabelIds.Count, CollaborationGoods.PlayerPhotoID.Count);
		list_Labels.numItems = _displayCount;
		list_Labels.touchable = _displayCount > 1;
		list_Page.numItems = _displayCount;
		list_Page.visible = _displayCount > 1;
		startScrollStatus = true;
		list_Labels.scrollPane.onScroll.Call();
	}

	private void RefreshLabelItem(int index, GObject item)
	{
		if (item is UICom_PlayerLabel com_Label && _displayCount > 0)
		{
			string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
			int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
			int photoAndBackgroundName = CollaborationGoods.PhotoAndBackgroundName;
			txt_LabelTitle.text = photoAndBackgroundName.GetLocal(UIStringType.Collaboration);
			if (LabelIds.Count > 0)
			{
				int index2 = index % LabelIds.Count;
				(string, bool) playerLabel = LabelIds[index2].GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
				CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
				CommonUIManager.RendererLabel(UIType.Window, 323, com_Label, playerLabel.Item1, playerLabel.Item2);
			}
			else
			{
				CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
			}
			RepeatedField<int> playerPhotoID = CollaborationGoods.PlayerPhotoID;
			if (playerPhotoID.Count > 0)
			{
				int index3 = index % playerPhotoID.Count;
				string fashionAccountHeadShot = playerPhotoID[index3].GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
				CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
			}
			else
			{
				CommonUIManager.RendererHeadShot(com_Label, "", isVideo: false);
			}
		}
	}

	public static UINGOStore_Com_GoodsItem CreateInstance()
	{
		return (UINGOStore_Com_GoodsItem)UIPackage.CreateObject("NGOStore", "NGOStore_Com_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		BGType = GetControllerAt(0);
		type = GetControllerAt(1);
		graph_FirstTheme = (GGraph)GetChildAt(2);
		graph_SecondTheme = (GGraph)GetChildAt(3);
		graph_Skin = (GGraph)GetChildAt(5);
		txt_RoleTitle = (GTextField)GetChildAt(6);
		list_Labels = (GList)GetChildAt(7);
		list_Page = (GList)GetChildAt(8);
		txt_LabelTitle = (GTextField)GetChildAt(9);
		list_Prop = (GList)GetChildAt(10);
		btn_Purchase = (UINGOStore_Button_Purchase)GetChildAt(11);
	}
}
