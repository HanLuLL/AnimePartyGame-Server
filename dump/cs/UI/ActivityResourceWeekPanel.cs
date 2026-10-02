using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityResourceWeekPanel : BasePanel<UIActivityResourceWeekPanel>
{
	private ActivityInfoConfigure _currentActivityConfig;

	private List<int> curActivityWeekTaskIds;

	private readonly List<TaskActivityData> activityList = new List<TaskActivityData>();

	private TaskActivityData curActivityWeekData;

	private void InitComponentsForWeek()
	{
		base.ui.com_panel.list_ActivityWeekTask.itemRenderer = RendererActivityTask;
	}

	private void AddEventForWeek()
	{
		base.ui.com_panel.btn_TaskToggle.onClick.Add(TaskToggle_Week);
	}

	private void RemoveEventForWeek()
	{
		base.ui.com_panel.btn_TaskToggle.onClick.Remove(TaskToggle_Week);
	}

	public ActivityResourceWeekPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityResourceWeekPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		int num = ((objs != null && objs.Length != 0) ? ((RepeatedField<int>)objs[0])[0] : ((_currentActivityConfig == null) ? StaticConfigure.Activity.Infos[0].Id : _currentActivityConfig.Id));
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(num, out _currentActivityConfig))
		{
			Debug.LogError($"获取活动id:{num}错误!");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RefreshActivityWeek();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(_currentActivityConfig.CurrencyBar);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponentsForWeek();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		AddEventForWeek();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		RemoveEventForWeek();
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void TaskToggle_Week()
	{
		base.ui.com_panel.btn_TaskToggle.onClick.Retain();
		RefreshActivityTask();
		base.ui.com_panel.btn_TaskToggle.onClick.Release();
	}

	private void RefreshActivityWeek()
	{
		curActivityWeekData = null;
		activityList.Clear();
		foreach (ActivityInfoConfigure info in StaticConfigure.Activity.Infos)
		{
			if (info.UiTab == 1 && info.UiType == UIType.Panel && SimpleSingletonProvider<GameLogicManager>.inst.activity.AdjustActivity(info))
			{
				TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(info.Id);
				activityList.Add(taskActivityData);
			}
		}
		if (activityList.Count > 0)
		{
			RefreshActivityDetail(activityList[0]);
		}
	}

	private void RefreshActivityDetail(TaskActivityData _data)
	{
		curActivityWeekData = _data;
		base.ui.com_panel.loader_Activity.url = curActivityWeekData.activityConfig.ActivityImage;
		base.ui.com_panel.txt_Title.text = curActivityWeekData.activityConfig.TitleID.GetLocal(UIStringType.Activity);
		base.ui.com_panel.txt_Desc.text = curActivityWeekData.activityConfig.DescriptionID.GetLocal(UIStringType.Activity);
		base.ui.com_panel.btn_TaskToggle.selected = true;
		base.ui.com_panel.btn_TaskToggle.visible = false;
		RefreshActicityTime();
		RefreshActivityTask();
	}

	private void RefreshActicityTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if ((object)curActivityWeekData.activityConfig.EndTime != null)
		{
			DateTime dateTime = curActivityWeekData.activityConfig.EndTime.ToDateTime();
			if (serverTime > dateTime)
			{
				base.ui.com_panel.showTime.selectedIndex = 0;
				return;
			}
			base.ui.com_panel.showTime.selectedIndex = 1;
			base.ui.com_panel.txt_timeTip.text = TimeHelper.RefreshTimeText(1033, 1034, serverTime, dateTime);
		}
		else
		{
			base.ui.com_panel.showTime.selectedIndex = 0;
		}
	}

	private void RefreshActivityTask()
	{
		base.ui.com_panel.list_ActivityWeekTask.numItems = 0;
		if (curActivityWeekData != null)
		{
			bool selected = base.ui.com_panel.btn_TaskToggle.selected;
			curActivityWeekTaskIds = curActivityWeekData.GetTaskId(selected);
			base.ui.com_panel.list_ActivityWeekTask.numItems = curActivityWeekTaskIds.Count;
			base.ui.com_panel.list_ActivityWeekTask.scrollPane.percY = 0f;
		}
	}

	private void RendererActivityTask(int index, GObject item)
	{
		if (curActivityWeekTaskIds == null)
		{
			return;
		}
		UIActivityResourceWeek_Com_Label label = item as UIActivityResourceWeek_Com_Label;
		if (label == null)
		{
			return;
		}
		BaseTaskData taskData = curActivityWeekData.taskDataDict[curActivityWeekTaskIds[index]];
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_taskDesc.text = taskData.GetTaskDesc();
		label.txt_taskTitle.text = taskData.GetTaskTitle();
		label.txt_timeTip.text = taskData.GetTime();
		label.txt_RefreshType.visible = taskData.GetTaskRefreshType() != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		RefreshBar(label.bar_task, taskData.TaskTarget, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curActivityWeekData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshActivityTask();
					SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(curActivityWeekData.activityConfig.Id);
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private async void GoTargetPanel(BaseTaskData taskData)
	{
		int way = taskData.GetWay();
		if (way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
		}
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private void RendererRewardItem(GList list, List<KeyValuePair<int, int>> rewards)
	{
		list.itemRenderer = delegate(int i, GObject o)
		{
			KeyValuePair<int, int> keyValuePair = rewards[i];
			CommonUIManager.RendererLitItem((UICom_LitItem)o, keyValuePair.Key, keyValuePair.Value);
		};
		list.numItems = rewards.Count;
		list.scrollPane.touchEffect = rewards.Count > 3;
	}
}
