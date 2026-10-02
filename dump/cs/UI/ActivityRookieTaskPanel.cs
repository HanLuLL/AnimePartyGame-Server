using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityRookieTaskPanel : BasePanel<UIActivityRookieTaskPanel>
{
	private RookieGroupTaskData _curRookieTaskData;

	private int _activityId;

	public ActivityRookieTaskPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityRookieTaskPanel.CreateInstance();
		base.Create();
		base.ui.list_Task.itemRenderer = RendererTask;
	}

	protected override void InitData(params object[] objs)
	{
		base.InitData();
		if (objs != null && objs.Length > 0 && objs[0] is RepeatedField<int> { Count: >0 } repeatedField)
		{
			_activityId = repeatedField[0];
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.loadbg.MallScreen();
	}

	public override void Refresh()
	{
		base.Refresh();
		ResetCurRookieTask();
		RefreshTaskData();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_finalReward.onClick.Add(ReceiveFinalReward);
		base.ui.btn_Detail.onClick.Add(ShowRewardDetail);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_finalReward.onClick.Remove(ReceiveFinalReward);
		base.ui.btn_Detail.onClick.Remove(ShowRewardDetail);
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
		ResetCurRookieTask();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void ReceiveFinalReward()
	{
		MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(_curRookieTaskData.FinalTaskId);
		TryReceiveReward(missionData, base.ui.btn_finalReward, SimpleSingletonProvider<GameLogicManager>.inst.task.rookieTaskData.UpdateRookieTask);
	}

	private void TryReceiveReward(MissionData missionData, GButton btn, Action callback = null)
	{
		if (missionData == null || missionData.TaskRunning)
		{
			return;
		}
		btn.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.task.RequestActivityMissionRewardC2S(missionData._Id).OnFinishedOnly.AddOnce(delegate
		{
			callback?.Invoke();
			RefreshTaskData();
			if (_activityId != 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(_activityId);
			}
			btn.onClick.Release();
		});
	}

	private void ShowRewardDetail()
	{
		SimpleSingletonProvider<UIManager>.inst.RookieTaskInfo.ShowRewardDetail().Forget();
	}

	private void OnCurFinalMissionStateChange(MissionData missionData)
	{
		if (missionData._Id == _curRookieTaskData.FinalTaskId)
		{
			RefreshFinalTask(missionData);
		}
	}

	private void RefreshTaskData()
	{
		RookieGroupTaskData curTaskGroup = SimpleSingletonProvider<GameLogicManager>.inst.task.rookieTaskData.GetCurTaskGroup();
		if (curTaskGroup != null)
		{
			MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(curTaskGroup.FinalTaskId);
			if (_curRookieTaskData == null || _curRookieTaskData.FinalTaskId != curTaskGroup.FinalTaskId)
			{
				RemoveCurRookieTaskStateChange();
				missionData.StateChange.AddListener(OnCurFinalMissionStateChange);
			}
			_curRookieTaskData = curTaskGroup;
			base.ui.list_Task.numItems = _curRookieTaskData.TaskIds.Count;
			base.ui.txt_title.SetVar("level", (SimpleSingletonProvider<GameLogicManager>.inst.task.rookieTaskData.GetCurTaskIndex() + 1).ToString()).FlushVars();
			RefreshFinalTask(missionData);
		}
	}

	private void ResetCurRookieTask()
	{
		RemoveCurRookieTaskStateChange();
		_curRookieTaskData = null;
	}

	private void RemoveCurRookieTaskStateChange()
	{
		if (_curRookieTaskData != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(_curRookieTaskData.FinalTaskId).StateChange.RemoveListener(OnCurFinalMissionStateChange);
		}
	}

	private void RefreshFinalTask(MissionData missionData)
	{
		if (missionData == null)
		{
			return;
		}
		using (List<KeyValuePair<int, int>>.Enumerator enumerator = missionData.rewards.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.Current;
				ItemInfoConfigure itemInfoConfigure = current.Key.GetItemInfoConfigure();
				base.ui.loader_finalRewardIocn.url = itemInfoConfigure.Icon;
				base.ui.txt_finalRewardName.text = ((current.Value > 1) ? $"{itemInfoConfigure.NameID.GetLocal(UIStringType.Item)}x{current.Value}" : itemInfoConfigure.NameID.GetLocal(UIStringType.Item));
			}
		}
		base.ui.btn_finalReward.enabled = !missionData.TaskRunning;
	}

	private void RendererTask(int index, GObject item)
	{
		UIRookieTask_Com_AchieveLabel label = item as UIRookieTask_Com_AchieveLabel;
		if (label == null)
		{
			return;
		}
		int missionId = _curRookieTaskData.TaskIds[index];
		MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(missionId);
		if (missionData == null)
		{
			return;
		}
		using (List<KeyValuePair<int, int>>.Enumerator enumerator = missionData.rewards.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.Current;
				CommonUIManager.RendererLitItem((UICom_LitItem)label.rewardItem, current.Key, current.Value);
			}
		}
		label.Status.selectedIndex = (missionData._FinishStatus ? 2 : ((!missionData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = (missionData._FinishStatus ? 2 : ((!missionData.TaskRunning) ? 1 : 0));
		label.btn_GoWay.Status.selectedIndex = ((!missionData.TaskRunning && !missionData._FinishStatus) ? 1 : 0);
		label.txt_taskDesc.text = missionData.GetTaskDesc();
		RefreshBar(label.bar_task, missionData.config.ParamProgress, missionData.AchieveProgress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			TryReceiveReward(missionData, label.btn_taskStatus);
		});
		label.btn_GoWay.visible = missionData.config.Way != 0 && !missionData._FinishStatus;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(label.btn_GoWay, missionData);
		});
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private async void GoTargetPanel(GButton btn, MissionData missionData)
	{
		if (missionData.config.Way != 0)
		{
			btn.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(missionData.config.Way);
			btn.onClick.Release();
		}
	}
}
