using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIActivityComeback_Task_Com : GComponent
{
	private TaskActivityData _activityData;

	private List<int> _taskIds;

	public GLoader hero_loader;

	public GLoader loader_Title;

	public GTextField txt_Task_Time;

	public GList list_Tasks;

	public const string URL = "ui://hconmwfcy9qn9";

	public bool HasRedPoint { get; private set; }

	public void Init(int activityId)
	{
		_activityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		if (_activityData == null)
		{
			Debug.LogError($"[ActivityComebackPanel] 活动Id: {activityId} 数据为空");
		}
		else
		{
			list_Tasks.itemRenderer = RendererTask;
		}
	}

	public void OnShow()
	{
		RefreshTaskList();
		RefreshTaskTime();
		UpdateRedStatus();
		RepeatedField<string> titleImages = _activityData.activityConfig.TitleImages;
		loader_Title.url = GameSettings.GetDataForLanguage(titleImages[1], titleImages[2], titleImages[0], titleImages[3]);
		hero_loader.url = (GameSettings.angelMode ? _activityData.activityConfig.ActivityImageSFW : _activityData.activityConfig.ActivityImage);
	}

	public void RefreshRedPoints()
	{
		UpdateRedStatus();
	}

	private void RefreshTaskTime()
	{
		if (txt_Task_Time != null)
		{
			ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
			if (returnInfo == null || returnInfo.TriggerTime == 0L || returnInfo.EndTime == 0L)
			{
				txt_Task_Time.visible = false;
				return;
			}
			txt_Task_Time.text = TimeHelper.GetDurationText(returnInfo.TriggerTime, returnInfo.EndTime);
			txt_Task_Time.visible = true;
		}
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.AddListener(OnActivityStatusChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.RemoveListener(OnActivityStatusChanged);
	}

	public void ClearData()
	{
		_activityData = null;
		_taskIds = null;
	}

	public override void Dispose()
	{
		_activityData = null;
		_taskIds = null;
		base.Dispose();
	}

	private void RendererTask(int index, GObject item)
	{
		UIActivityComeback_Task_Com_Label label = item as UIActivityComeback_Task_Com_Label;
		if (label == null || _taskIds == null || index >= _taskIds.Count)
		{
			return;
		}
		BaseTaskData taskData = _activityData.taskDataDict[_taskIds[index]];
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)label.btn_Item, keyValuePair.Key, keyValuePair.Value);
		bool flag = IsTaskRunning(taskData);
		bool canClaim = CanClaimTask(taskData);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!flag) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!flag) ? 1 : 0);
		label.txt_RefreshType.visible = taskData.GetTaskRefreshType() != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		label.txt_taskTitle.text = taskData.GetTaskDesc();
		label.bar_task.max = taskData.TaskTarget;
		label.bar_task.min = 0.0;
		label.bar_task.value = Mathf.Min(taskData._Progress, taskData.TaskTarget);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (canClaim)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(_activityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshTaskList();
					UpdateRedStatus();
					SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(_activityData.activityConfig.Id);
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.group_Go.visible = !taskData._FinishStatus && flag && taskData.GetWay() != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
		});
	}

	private void RefreshTaskList()
	{
		if (_activityData != null)
		{
			_taskIds = _activityData.GetTaskId(overView: false);
			list_Tasks.numItems = _taskIds?.Count ?? 0;
		}
	}

	private void UpdateRedStatus()
	{
		bool hasRedPoint = false;
		if (_activityData != null)
		{
			foreach (var (_, baseTaskData2) in _activityData.taskDataDict)
			{
				if (baseTaskData2.ValidityTime() && !baseTaskData2._FinishStatus && CanClaimTask(baseTaskData2))
				{
					hasRedPoint = true;
					break;
				}
			}
		}
		HasRedPoint = hasRedPoint;
	}

	private bool CanClaimTask(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress < missionData.TaskTarget)
			{
				return missionData.Status == 2;
			}
			return true;
		}
		return !taskData.TaskRunning;
	}

	private bool IsTaskRunning(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress >= missionData.TaskTarget)
			{
				return false;
			}
			return missionData.Status != 2;
		}
		return taskData.TaskRunning;
	}

	private async void GoTargetPanel(BaseTaskData taskData)
	{
		int way = taskData.GetWay();
		if (way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
		}
	}

	private void OnActivityStatusChanged(int activityId)
	{
		if (_activityData != null && _activityData.activityConfig.Id == activityId)
		{
			RefreshTaskList();
			UpdateRedStatus();
		}
	}

	public static UIActivityComeback_Task_Com CreateInstance()
	{
		return (UIActivityComeback_Task_Com)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Task_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hero_loader = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(1);
		txt_Task_Time = (GTextField)GetChildAt(5);
		list_Tasks = (GList)GetChildAt(6);
	}
}
