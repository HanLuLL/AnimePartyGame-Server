using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class UIActivityMGWT_Com_Task : GComponent
{
	private TaskActivityData _activityData;

	private List<int> _taskIds;

	public Controller language;

	public GLoader hero_loader;

	public GLoader loader_Title;

	public GTextField txt_Task_Time;

	public GList list_Tasks;

	public Transition Cut_in;

	public const string URL = "ui://wdl8l4hslwm16";

	public void Init(int activityId)
	{
		ActivityLogic activity = SimpleSingletonProvider<GameLogicManager>.inst.activity;
		if (activity == null)
		{
			Debug.LogError("[ActivityMGWTPanel] ActivityLogic 未初始化");
			return;
		}
		_activityData = activity.GetTaskActivityData(activityId);
		if (_activityData == null)
		{
			Debug.LogError($"[ActivityMGWTPanel] 活动Id:{activityId}任务数据为空");
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
		RefreshTaskImages();
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal?.activityStatus.AddListener(OnActivityStatusChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal?.activityStatus.RemoveListener(OnActivityStatusChanged);
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

	private void RefreshTaskTime()
	{
		if (txt_Task_Time != null)
		{
			ActivityInfoConfigure obj = _activityData?.activityConfig;
			Timestamp timestamp = obj?.BeginTime;
			Timestamp timestamp2 = obj?.EndTime;
			if (timestamp == null || timestamp2 == null || timestamp.Seconds == 0L || timestamp2.Seconds == 0L)
			{
				txt_Task_Time.visible = false;
				return;
			}
			string durationText = TimeHelper.GetDurationText(timestamp, timestamp2, OnlyDuration: true);
			txt_Task_Time.text = durationText;
			txt_Task_Time.visible = !string.IsNullOrEmpty(durationText);
		}
	}

	private void RefreshTaskImages()
	{
		ActivityInfoConfigure activityInfoConfigure = _activityData?.activityConfig;
		if (hero_loader != null)
		{
			string text = ((!GameSettings.angelMode) ? activityInfoConfigure?.ActivityImage : activityInfoConfigure?.ActivityImageSFW);
			hero_loader.url = (string.IsNullOrEmpty(text) ? string.Empty : text);
		}
		if (loader_Title != null)
		{
			RepeatedField<string> repeatedField = activityInfoConfigure?.TitleImages;
			if (repeatedField == null || repeatedField.Count < 4)
			{
				loader_Title.url = string.Empty;
				return;
			}
			string dataForLanguage = GameSettings.GetDataForLanguage(repeatedField[1], repeatedField[2], repeatedField[0], repeatedField[3]);
			loader_Title.url = (string.IsNullOrEmpty(dataForLanguage) ? string.Empty : dataForLanguage);
		}
	}

	private void RefreshTaskList()
	{
		if (_activityData != null && list_Tasks != null)
		{
			_taskIds = _activityData.GetTaskId(overView: false);
			list_Tasks.numItems = _taskIds?.Count ?? 0;
		}
	}

	private void RendererTask(int index, GObject item)
	{
		UIActivityMGWT_Task_Com_Label label = item as UIActivityMGWT_Task_Com_Label;
		if (label == null || _activityData == null || _taskIds == null || index < 0 || index >= _taskIds.Count || !_activityData.taskDataDict.TryGetValue(_taskIds[index], out var taskData))
		{
			return;
		}
		if (taskData.rewards != null && taskData.rewards.Count > 0)
		{
			KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
			label.btn_Item.visible = true;
			CommonUIManager.RendererLitItem((UICom_LitItem)label.btn_Item, keyValuePair.Key, keyValuePair.Value);
		}
		else
		{
			label.btn_Item.visible = false;
		}
		bool flag = IsTaskRunning(taskData);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!flag) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!flag) ? 1 : 0);
		label.bar_task.max = taskData.TaskTarget;
		label.bar_task.min = 0.0;
		label.bar_task.value = Mathf.Min(taskData._Progress, taskData.TaskTarget);
		label.txt_taskTitle.text = taskData.GetTaskDesc();
		TaskRefreshType taskRefreshType = taskData.GetTaskRefreshType();
		label.txt_RefreshType.visible = taskRefreshType != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		int activityId = _activityData.activityConfig.Id;
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData._FinishStatus)
			{
				if (IsTaskRunning(taskData))
				{
					GoTargetPanel(taskData);
				}
				else if (CanClaimTask(taskData))
				{
					ActivityLogic activity = SimpleSingletonProvider<GameLogicManager>.inst.activity;
					if (activity != null)
					{
						label.btn_taskStatus.onClick.Retain();
						activity.OnRequestTaskReward(activityId, taskData).OnFinishedOnly.AddOnce(delegate
						{
							RefreshTaskList();
							activity.signal.activityStatus.Dispatch(activityId);
							label.btn_taskStatus.onClick.Release();
						});
					}
				}
			}
		});
	}

	private static bool CanClaimTask(BaseTaskData taskData)
	{
		if (taskData == null || taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress < missionData.TaskTarget && missionData.Status != 2)
			{
				return missionData.Status == 4;
			}
			return true;
		}
		return !taskData.TaskRunning;
	}

	private static bool IsTaskRunning(BaseTaskData taskData)
	{
		if (taskData == null || taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress >= missionData.TaskTarget)
			{
				return false;
			}
			if (missionData.Status != 2)
			{
				return missionData.Status != 4;
			}
			return false;
		}
		return taskData.TaskRunning;
	}

	private async void GoTargetPanel(BaseTaskData taskData)
	{
		if (IsTaskRunning(taskData))
		{
			int way = taskData.GetWay();
			if (way != 0)
			{
				await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
			}
		}
	}

	private void OnActivityStatusChanged(int activityId)
	{
		if (_activityData?.activityConfig != null && _activityData.activityConfig.Id == activityId)
		{
			RefreshTaskList();
		}
	}

	public static UIActivityMGWT_Com_Task CreateInstance()
	{
		return (UIActivityMGWT_Com_Task)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Com_Task");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		hero_loader = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(1);
		txt_Task_Time = (GTextField)GetChildAt(5);
		list_Tasks = (GList)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}
