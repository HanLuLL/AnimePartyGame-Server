using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UIActivityPopup_Com_Adv : GComponent
{
	private float _AutoScrollTime;

	private bool _StartScrollStatus;

	public GList list_Adv;

	public GList list_Page;

	public const string URL = "ui://3tvdl51qnhqm39";

	private void StartAutoScroll(EventContext context)
	{
		list_Adv.onTouchEnd.Retain();
		_AutoScrollTime = 0f;
		_StartScrollStatus = true;
		list_Adv.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Adv.onTouchBegin.Retain();
		_AutoScrollTime = 0f;
		_StartScrollStatus = false;
		list_Adv.onTouchBegin.Release();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (_StartScrollStatus && list_Adv.numItems > 1)
		{
			if (_AutoScrollTime >= 3f)
			{
				list_Adv.scrollPane.ScrollRight(1f, ani: true);
				_AutoScrollTime = 0f;
			}
			_AutoScrollTime += Time.deltaTime;
		}
	}

	public void InitComponent()
	{
		list_Adv.SetVirtualAndLoop();
		list_Adv.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		list_Adv.numItems = 3;
		list_Page.numItems = 3;
		list_Adv.scrollPane.onScroll.Call();
		_StartScrollStatus = true;
	}

	public void AddEvent()
	{
		list_Adv.scrollPane.onScroll.Add(ScrollActivity);
		list_Adv.onTouchBegin.Add(StopAutoScroll);
		list_Adv.onTouchEnd.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Adv.scrollPane.onScroll.Remove(ScrollActivity);
		list_Adv.onTouchBegin.Remove(StopAutoScroll);
		list_Adv.onTouchEnd.Remove(StartAutoScroll);
	}

	private void ScrollActivity()
	{
		if (list_Adv.numItems > 0)
		{
			int selectedIndex = list_Adv.scrollPane.currentPageX % list_Adv.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private void RefreshActivityButton(int index, GObject item)
	{
		if (item is UIActivityPopup_Button_Adv uIActivityPopup_Button_Adv)
		{
			uIActivityPopup_Button_Adv.page.selectedIndex = index % 3;
		}
	}

	public static UIActivityPopup_Com_Adv CreateInstance()
	{
		return (UIActivityPopup_Com_Adv)UIPackage.CreateObject("ActivityPopup", "ActivityPopup_Com_Adv");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Adv = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
	}
}
