using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWT_Com_Goods : GComponent
{
	private CollaborationInfoConfigure InfoConfig;

	private List<int> PreviewIndex = new List<int>();

	private int pageIndex;

	public UIMGWT_Com_ExtraAward _extraAward;

	public Controller page;

	public Controller haspage;

	public UIMGWT_Store_Com_GoodsItem goods_item1;

	public UIMGWT_Store_Com_GoodsItem goods_item2;

	public UIMGWT_Store_Com_GoodsItem goods_pack;

	public GButton page_up;

	public GButton page_down;

	public const string URL = "ui://2p754tqkilj515";

	public void Init(CollaborationInfoConfigure info)
	{
		InfoConfig = info;
		goods_item1.InitComponent();
		goods_item1.SetShowPopup(ShowPopPage);
		goods_item2.InitComponent();
		goods_item2.SetShowPopup(ShowPopPage);
		goods_pack.InitComponent();
		goods_pack.SetShowPopup(ShowPopPage);
		PreviewIndex.Clear();
		pageIndex = 0;
		page.selectedIndex = 0;
		for (int i = 0; i < InfoConfig.PreviewIndex.Count; i++)
		{
			PreviewIndex.Add(InfoConfig.PreviewIndex[i]);
		}
		if (PreviewIndex.Count > 3)
		{
			haspage.selectedIndex = 1;
		}
		else
		{
			haspage.selectedIndex = 0;
		}
		OnShow();
	}

	public void OnShow(int showPage = 0)
	{
		if (pageIndex * 3 < PreviewIndex.Count && pageIndex >= 0)
		{
			pageIndex = showPage;
			if (pageIndex == 0)
			{
				page.selectedIndex = 0;
			}
			else if ((pageIndex + 1) * 3 >= PreviewIndex.Count)
			{
				page.selectedIndex = 2;
			}
			else
			{
				page.selectedIndex = 1;
			}
			int num = pageIndex * 3;
			goods_item1.Clear();
			goods_item1.RenderGoodsItem(PreviewIndex[num]);
			goods_item1.herostate.selectedIndex = num;
			if (num + 1 < PreviewIndex.Count)
			{
				goods_item2.Clear();
				goods_item2.RenderGoodsItem(PreviewIndex[num + 1]);
				goods_item2.herostate.selectedIndex = num + 1;
				goods_item2.visible = true;
			}
			else
			{
				goods_item2.visible = false;
			}
			if (num + 2 < PreviewIndex.Count)
			{
				goods_pack.Clear();
				goods_pack.RenderGoodsItem(PreviewIndex[num + 2]);
				goods_pack.herostate.selectedIndex = num + 2;
				goods_pack.visible = true;
			}
			else
			{
				goods_pack.visible = false;
			}
		}
	}

	public void AddEvent()
	{
		page_up.onClick.Add(OnPageUpClicked);
		page_down.onClick.Add(OnPageDownClicked);
		goods_item1.AddEvent();
		goods_item2.AddEvent();
		goods_pack.AddEvent();
	}

	public void RemoveEvent()
	{
		page_up.onClick.Remove(OnPageUpClicked);
		page_down.onClick.Remove(OnPageDownClicked);
		goods_item1.RemoveEvent();
		goods_item2.RemoveEvent();
		goods_pack.RemoveEvent();
	}

	private void OnPageUpClicked()
	{
		OnShow(pageIndex - 1);
	}

	private void OnPageDownClicked()
	{
		OnShow(pageIndex + 1);
	}

	private void ShowPopPage(float x, float y, int GoodsId)
	{
		GRoot.inst.ShowPopup(_extraAward, this);
		_extraAward.SetXY(x, y);
		_extraAward.SetData(GoodsId);
	}

	public static UIMGWT_Com_Goods CreateInstance()
	{
		return (UIMGWT_Com_Goods)UIPackage.CreateObject("MGWTStore", "MGWT_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		haspage = GetControllerAt(1);
		goods_item1 = (UIMGWT_Store_Com_GoodsItem)GetChildAt(0);
		goods_item2 = (UIMGWT_Store_Com_GoodsItem)GetChildAt(1);
		goods_pack = (UIMGWT_Store_Com_GoodsItem)GetChildAt(2);
		page_up = (GButton)GetChildAt(3);
		page_down = (GButton)GetChildAt(4);
	}
}
