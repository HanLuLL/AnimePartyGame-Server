using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIHome_Com_Activity : GComponent
{
	private float autoScrollTime;

	public bool startScrollStatus;

	private readonly List<ActivityActivityEntrance2Configure> ActivityDataList = new List<ActivityActivityEntrance2Configure>();

	public GList list_Activity;

	public GList list_Page;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcguwi2kq48";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Activity.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Activity.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Activity.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		list_Activity.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Activity.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		list_Activity.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Activity.SetVirtualAndLoop();
		list_Activity.scrollPane.decelerationRate = 0.05f;
		list_Activity.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		ActivityDataList.Clear();
		ReadyBannerData();
		list_Activity.numItems = ActivityDataList.Count;
		if (ActivityDataList.Count > 1)
		{
			list_Activity.scrollPane.touchEffect = true;
			list_Page.numItems = ActivityDataList.Count;
			startScrollStatus = true;
			list_Activity.scrollPane.onScroll.Call();
		}
		else
		{
			list_Page.numItems = 0;
			startScrollStatus = false;
			list_Activity.scrollPane.touchEffect = false;
		}
		base.visible = ActivityDataList.Count > 0;
	}

	public void AddEvent()
	{
		list_Activity.scrollPane.onScroll.Add(ScrollActivity);
		base.onRollOver.Add(StopAutoScroll);
		base.onRollOut.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Activity.scrollPane.onScroll.Remove(ScrollActivity);
		base.onRollOver.Remove(StopAutoScroll);
		base.onRollOut.Remove(StartAutoScroll);
	}

	private void ScrollActivity(EventContext context)
	{
		if (list_Activity.numItems > 0)
		{
			int selectedIndex = list_Activity.scrollPane.currentPageX % list_Activity.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private void ReadyBannerData()
	{
		List<ActivityActivityEntrance2Configure> activityInfos = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfos(2);
		if (activityInfos.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < activityInfos.Count; i++)
		{
			int activityId = activityInfos[i].InfoConfig.Id;
			TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
			if (taskActivityData != null && !taskActivityData.IsActivityComplete())
			{
				ActivityDataList.Add(activityInfos[i]);
			}
		}
	}

	private void RefreshActivityButton(int index, GObject item)
	{
		UIHome_Button_SpecialActivity btn_Special = item as UIHome_Button_SpecialActivity;
		if (btn_Special != null)
		{
			switch (ActivityDataList[index].InfoConfig.UiType)
			{
			case UIType.Window:
			{
				TaskActivityData taskActivityData2 = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(ActivityDataList[index].InfoConfig.Id);
				btn_Special.redPoint.selectedIndex = (taskActivityData2.GetActivityStatus() ? 1 : 0);
				btn_Special.txt_Time.text = taskActivityData2.GetDurationText();
				btn_Special.title = ActivityDataList[index].Title.GetLocal(UIStringType.Activity);
				btn_Special.loader_Icon.url = ActivityDataList[index].Icon;
				break;
			}
			case UIType.Panel:
			{
				TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(ActivityDataList[index].InfoConfig.Id);
				btn_Special.redPoint.selectedIndex = (taskActivityData.GetActivityStatus() ? 1 : 0);
				btn_Special.txt_Time.text = taskActivityData.GetDurationText();
				btn_Special.title = ActivityDataList[index].Title.GetLocal(UIStringType.Activity);
				btn_Special.loader_Icon.url = ActivityDataList[index].Icon;
				break;
			}
			}
			btn_Special.onClick.Set((EventCallback0)delegate
			{
				btn_Special.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.home.OpenActivity(ActivityDataList[index]).Forget();
				btn_Special.onClick.Release();
			});
		}
	}

	public static UIHome_Com_Activity CreateInstance()
	{
		return (UIHome_Com_Activity)UIPackage.CreateObject("Home", "Home_Com_Activity");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Activity = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
