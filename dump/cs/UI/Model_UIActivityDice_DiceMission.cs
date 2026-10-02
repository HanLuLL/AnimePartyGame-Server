using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class Model_UIActivityDice_DiceMission : BaseModel<UIActivityDice_Com_DiceMission>
{
	private MissionData missionData;

	private bool isAddMissionDataListener;

	public Action ResortMissionList;

	public Model_UIActivityDice_DiceMission(UIActivityDice_Com_DiceMission _com)
		: base(_com)
	{
	}

	public void ChangeMissionData(MissionData _data)
	{
		if (_data != missionData)
		{
			if (isAddMissionDataListener)
			{
				missionData?.StateChange?.RemoveListener(OnMissionDataChange);
				missionData = _data;
				missionData?.StateChange?.AddListener(OnMissionDataChange);
			}
			else
			{
				missionData = _data;
			}
		}
	}

	private void OnMissionDataChange(MissionData data)
	{
		Refresh();
		ResortMissionList?.Invoke();
	}

	public override void AddEvent()
	{
		com.rewardItem.onClick.Add(OnLitItemClick);
		com.btn_GoWay.onClick.Add(OnGoWayClick);
		com.btn_taskStatus.onClick.Add(OnTaskStatusClick);
		AddMissionDataListener();
	}

	public override void RemoveEvent()
	{
		com.rewardItem.onClick.Remove(OnLitItemClick);
		com.btn_GoWay.onClick.Remove(OnGoWayClick);
		com.btn_taskStatus.onClick.Remove(OnTaskStatusClick);
		missionData?.StateChange?.RemoveListener(OnMissionDataChange);
		RemoveMissionDataListener();
	}

	public void AddMissionDataListener()
	{
		if (!isAddMissionDataListener)
		{
			isAddMissionDataListener = true;
			missionData?.StateChange?.AddListener(OnMissionDataChange);
		}
	}

	public void RemoveMissionDataListener()
	{
		if (isAddMissionDataListener)
		{
			isAddMissionDataListener = false;
			missionData?.StateChange?.RemoveListener(OnMissionDataChange);
		}
	}

	private void OnLitItemClick()
	{
		KeyValuePair<int, int> keyValuePair = missionData.rewards[0];
		int key = keyValuePair.Key;
		GButton rewardItem = com.rewardItem;
		int value = keyValuePair.Value;
		rewardItem.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(key, value, _Usable: true).Forget();
		rewardItem.onClick.Release();
	}

	private async void OnGoWayClick()
	{
		int way = missionData.config.Way;
		if (way != 0)
		{
			com.btn_GoWay.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
			com.btn_GoWay.onClick.Release();
		}
	}

	private void OnTaskStatusClick()
	{
		if (!missionData.TaskRunning)
		{
			com.btn_taskStatus.onClick.Retain();
			int id = missionData._Id;
			SimpleSingletonProvider<GameLogicManager>.inst.task.RequestActivityMissionRewardC2S(id).OnFinishedOnly.AddOnce(delegate
			{
				com.btn_taskStatus.onClick.Release();
			});
		}
	}

	public override void Refresh()
	{
		KeyValuePair<int, int> keyValuePair = missionData.rewards[0];
		int key = keyValuePair.Key;
		int value = keyValuePair.Value;
		UICom_LitItem obj = (UICom_LitItem)com.rewardItem;
		ItemInfoConfigure itemInfoConfigure = key.GetItemInfoConfigure();
		obj.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		obj.loader_Icon.url = itemInfoConfigure.ShowIcon;
		obj.txt_itemNum.text = value.ToString();
		obj.isShowNum.selectedIndex = 0;
		int num = ((!missionData.TaskRunning) ? 1 : 0);
		num = (missionData._FinishStatus ? 2 : num);
		int selectedIndex = ((num == 0) ? 1 : 0);
		bool visible = missionData.config.Way != 0 && num != 2;
		com.Status.selectedIndex = num;
		com.btn_GoWay.Status.selectedIndex = selectedIndex;
		com.btn_GoWay.visible = visible;
		com.txt_taskDesc.text = missionData.GetTaskDesc();
		com.btn_taskStatus.Status.selectedIndex = num;
		int paramProgress = missionData.config.ParamProgress;
		int achieveProgress = missionData.AchieveProgress;
		com.bar_task.max = paramProgress;
		com.bar_task.min = 0.0;
		com.bar_task.value = Mathf.Min(achieveProgress, paramProgress);
	}
}
