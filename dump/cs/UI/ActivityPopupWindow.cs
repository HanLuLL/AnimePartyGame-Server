using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityPopupWindow : BaseWindow
{
	private ActivityInfoConfigure CurActivityInfo;

	private TaskActivityData ActivityDataType1;

	public ActivityPopupWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIActivityPopupWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIActivityPopupWindow uIActivityPopupWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			uIActivityPopupWindow.btn_Close.onClick.Add(ClosePopup);
			uIActivityPopupWindow.di.onClick.Add(base.Hide);
			AddEvent_Type2();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIActivityPopupWindow uIActivityPopupWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uIActivityPopupWindow.btn_Close.onClick.Remove(ClosePopup);
			uIActivityPopupWindow.di.onClick.Remove(base.Hide);
			uIActivityPopupWindow.type.selectedIndex = 0;
			RemoveEvent_Type2();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIActivityPopupWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			Hide();
		}
	}

	private void ClosePopup()
	{
		Hide();
	}

	private async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async UniTask ShowActivity(int activityId)
	{
		if (StaticConfigure.Activity.InfoDict.TryGetValue(activityId, out CurActivityInfo))
		{
			if (CurActivityInfo.UiTab == 1)
			{
				await ShowActivity_Type1(activityId);
			}
			else if (CurActivityInfo.UiTab == 2)
			{
				await ShowActivity_Type2(activityId);
			}
			else if (CurActivityInfo.UiTab == 3)
			{
				await ShowActivity_Type3(activityId);
			}
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async UniTask ShowActivity_Type1(int activityId)
	{
		ActivityDataType1 = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIActivityPopupWindow win = gComponent as UIActivityPopupWindow;
		if (win == null)
		{
			return;
		}
		win.com_Type1.Cut_in.Play();
		win.com_Type1.btn_Go.title = 31.GetLocal(UIStringType.Message);
		win.com_Type1.txt_desc.text = 32.GetLocal(UIStringType.Message);
		win.type.selectedIndex = ActivityDataType1.activityConfig.UiTab;
		win.com_Type1.loader_BG.url = ActivityDataType1.activityConfig.ActivityImage;
		KeyValuePair<int, BaseTaskData> taskInfo = ActivityDataType1.taskDataDict.ElementAt(0);
		KeyValuePair<int, int> keyValuePair = taskInfo.Value.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)win.com_Type1.btn_Item, keyValuePair.Key, keyValuePair.Value);
		RefreshRewardStatus();
		win.com_Type1.btn_Go.onClick.Set((EventCallback1)async delegate
		{
			win.com_Type1.btn_Go.onClick.Retain();
			if (taskInfo.Value.TaskRunning && !taskInfo.Value._FinishStatus && taskInfo.Value is ActivityTaskData activityTaskData)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestClientCheckTaskC2S(activityTaskData._Config.Ref);
			}
			await UniTask.Delay(1000);
			TryGetType1Reward();
			win.com_Type1.btn_Go.onClick.Release();
		});
	}

	private void TryGetType1Reward()
	{
		if (base.isShowing && base.contentPane is UIActivityPopupWindow)
		{
			BaseTaskData baseTaskData = ActivityDataType1?.taskDataDict.ElementAt(0).Value;
			if (baseTaskData != null && !baseTaskData.TaskRunning && !baseTaskData._FinishStatus)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(ActivityDataType1.activityConfig.Id, baseTaskData).OnFinishedOnly.AddOnce(RefreshRewardStatus);
			}
		}
	}

	private void RefreshRewardStatus()
	{
		if (base.isShowing)
		{
			BaseTaskData baseTaskData = ActivityDataType1?.taskDataDict.ElementAt(0).Value;
			if (baseTaskData != null && base.contentPane is UIActivityPopupWindow uIActivityPopupWindow && uIActivityPopupWindow.type.selectedIndex == ActivityDataType1.activityConfig.UiTab)
			{
				uIActivityPopupWindow.com_Type1.btn_Item.grayed = baseTaskData._FinishStatus;
				uIActivityPopupWindow.com_Type1.status.selectedIndex = (baseTaskData._FinishStatus ? 1 : 0);
			}
		}
	}

	private async UniTask ShowActivity_Type2(int activityId)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIActivityPopupWindow win = gComponent as UIActivityPopupWindow;
		if (win != null)
		{
			win.type.selectedIndex = CurActivityInfo.UiTab;
			win.com_Type2.loader_BG.url = CurActivityInfo.ActivityImage;
			DateTime beginTime = 1740801600.StampToDateTime();
			win.com_Type2.status.selectedIndex = (TimeHelper.ValidityTime(beginTime, CurActivityInfo.EndTime.ToDateTime()) ? 1 : 0);
			if ((CurActivityInfo.EndTime.ToDateTime() - MonoSingletonProvider<NetManager>.inst.ServerTime).Days < 1)
			{
				win.com_Type2.txt_Countdown.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, CurActivityInfo.EndTime.ToDateTime());
				win.com_Type2.txt_Countdown.visible = true;
			}
			else
			{
				win.com_Type2.txt_Countdown.visible = false;
			}
			win.com_Type2.btn_GoCrowdfunding.onClick.Set((EventCallback0)delegate
			{
				win.com_Type2.btn_GoCrowdfunding.onClick.Retain();
				SimpleSingletonProvider<WebServerManager>.inst.PostSkipLinkRecord(LogToServerType.MODIAN);
				Application.OpenURL("https://zhongchou.modian.com/item/137932.html");
				win.com_Type2.btn_GoCrowdfunding.onClick.Release();
			});
			win.com_Type2.com_Adv.InitComponent();
			win.com_Type2.com_Adv.Show();
		}
	}

	private void AddEvent_Type2()
	{
		if (base.contentPane is UIActivityPopupWindow uIActivityPopupWindow && CurActivityInfo != null && CurActivityInfo.UiTab == 2)
		{
			uIActivityPopupWindow.com_Type2.com_Adv.AddEvent();
		}
	}

	private void RemoveEvent_Type2()
	{
		if (base.contentPane is UIActivityPopupWindow uIActivityPopupWindow && CurActivityInfo != null && CurActivityInfo.UiTab == 2)
		{
			uIActivityPopupWindow.com_Type2.com_Adv.RemoveEvent();
		}
	}

	private async UniTask ShowActivity_Type3(int activityId)
	{
		TaskActivityData activityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		if (activityData == null)
		{
			return;
		}
		DateTime endTime = activityData.activityConfig.EndTime.ToDateTime();
		DateTime dateTime = activityData.activityConfig.BeginTime.ToDateTime();
		DateTime nowTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (!(nowTime < dateTime) && !(nowTime > endTime))
		{
			await TryShow();
			if (base.contentPane is UIActivityPopupWindow uIActivityPopupWindow)
			{
				uIActivityPopupWindow.type.selectedIndex = CurActivityInfo.UiTab;
				TimeSpan timeSpan = endTime - nowTime;
				uIActivityPopupWindow.com_type3.loader_BG.url = activityData.activityConfig.ActivityImage;
				uIActivityPopupWindow.com_type3.txt_time.text = $"剩余时间：{timeSpan.Days}天{timeSpan.Hours}时";
			}
		}
	}
}
