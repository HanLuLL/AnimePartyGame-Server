using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIAnniversary_2nd_Com_Item : GComponent
{
	public SkinSellData SkinSellData;

	private readonly List<ItemInfoConfigure> _labelConfigList = new List<ItemInfoConfigure>();

	private float autoScrollTime;

	public bool startScrollStatus;

	public GList list_Hero;

	public GList list_Labels;

	public GList list_Page;

	public GList list_Goods;

	public UIAnniversary_2nd_Button_Price btn_Purchase;

	public GRichTextField txt_Explain;

	public Transition Cut_in;

	public const string URL = "ui://k49wk9ftnqmf2c";

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
		if (SkinSellData == null)
		{
			return;
		}
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = SkinSellData.skinSellConfig.SkinSellInfoConfigureItems;
		if (skinSellInfoConfigureItems == null)
		{
			return;
		}
		_labelConfigList.Clear();
		foreach (SkinSellInfoConfigureItem item in skinSellInfoConfigureItems)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.bag.TryGetSkinInfoFromRandomChest(item.Id, out var _, out var labelItemInfo);
			if (labelItemInfo != null)
			{
				_labelConfigList.Add(labelItemInfo);
			}
		}
		list_Labels.numItems = _labelConfigList.Count;
		list_Labels.touchable = _labelConfigList.Count > 1;
		list_Page.numItems = _labelConfigList.Count;
		list_Page.visible = _labelConfigList.Count > 1;
		startScrollStatus = true;
		list_Labels.scrollPane.onScroll.Call();
		txt_Explain.visible = _labelConfigList.Count > 1;
		txt_Explain.text = 2025071.GetLocal(UIStringType.SkinSell);
	}

	private void RefreshLabelItem(int index, GObject item)
	{
		if (item is UIAnniversary_2nd_Com_LabelItem uIAnniversary_2nd_Com_LabelItem)
		{
			ItemInfoConfigure itemInfoConfigure = _labelConfigList[index];
			UICom_PlayerLabel com_Label = (UICom_PlayerLabel)uIAnniversary_2nd_Com_LabelItem.com_Label;
			string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
			int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
			ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
			(string, bool) playerLabel = itemInfoConfigure.SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
			CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
			CommonUIManager.RendererLabel(UIType.Window, 347, com_Label, playerLabel.Item1, playerLabel.Item2);
			string fashionAccountHeadShot = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
			uIAnniversary_2nd_Com_LabelItem.txt_Title.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
			bool flag = SkinSellData.IsOwnGoods(itemInfoConfigure.Id);
			uIAnniversary_2nd_Com_LabelItem.txt_GetLabel.visible = flag;
			uIAnniversary_2nd_Com_LabelItem.graph_LabelGray.visible = flag;
			uIAnniversary_2nd_Com_LabelItem.txt_GetLabel.text = 1085.GetLocal(UIStringType.Message);
		}
	}

	public static UIAnniversary_2nd_Com_Item CreateInstance()
	{
		return (UIAnniversary_2nd_Com_Item)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Hero = (GList)GetChildAt(1);
		list_Labels = (GList)GetChildAt(2);
		list_Page = (GList)GetChildAt(3);
		list_Goods = (GList)GetChildAt(4);
		btn_Purchase = (UIAnniversary_2nd_Button_Price)GetChildAt(5);
		txt_Explain = (GRichTextField)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
