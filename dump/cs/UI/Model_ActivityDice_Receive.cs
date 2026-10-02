using System.Collections.Generic;
using Core.Net;
using GameLogic;
using Tools;

namespace UI;

public class Model_ActivityDice_Receive : BaseModel<UIActivityDice_Button_Receive>
{
	private List<MissionData> missionDatas = new List<MissionData>();

	private bool isAddMissionDataListener;

	public Model_ActivityDice_Receive(UIActivityDice_Button_Receive _com)
		: base(_com)
	{
	}

	public void ChangeMissionDatas(List<MissionData> _datas)
	{
		if (isAddMissionDataListener)
		{
			foreach (MissionData missionData in missionDatas)
			{
				missionData.StateChange?.RemoveListener(OnMissionDataChange);
			}
		}
		missionDatas.Clear();
		missionDatas.AddRange(_datas);
		if (!isAddMissionDataListener)
		{
			return;
		}
		foreach (MissionData missionData2 in missionDatas)
		{
			missionData2.StateChange?.AddListener(OnMissionDataChange);
		}
	}

	public void AddMissionDataListener()
	{
		if (isAddMissionDataListener)
		{
			return;
		}
		isAddMissionDataListener = true;
		foreach (MissionData missionData in missionDatas)
		{
			missionData.StateChange?.AddListener(OnMissionDataChange);
		}
	}

	public void RemoveMissionDataListener()
	{
		if (!isAddMissionDataListener)
		{
			return;
		}
		isAddMissionDataListener = false;
		foreach (MissionData missionData in missionDatas)
		{
			missionData.StateChange?.RemoveListener(OnMissionDataChange);
		}
	}

	private void OnMissionDataChange(MissionData data)
	{
		Refresh();
	}

	public override void AddEvent()
	{
		AddMissionDataListener();
		com.onClick.Add(OnButtonDown);
	}

	public override void RemoveEvent()
	{
		RemoveMissionDataListener();
		com.onClick.Remove(OnButtonDown);
	}

	private void OnButtonDown()
	{
		List<RPCAsyncResult> list = new List<RPCAsyncResult>();
		int callbackCount = 0;
		com.onClick.Retain();
		foreach (MissionData missionData in missionDatas)
		{
			int num = ((!missionData.TaskRunning) ? 1 : 0);
			num = (missionData._FinishStatus ? 2 : num);
			if (num == 1)
			{
				int id = missionData._Id;
				RPCAsyncResult item = SimpleSingletonProvider<GameLogicManager>.inst.task.RequestActivityMissionRewardC2S(id);
				list.Add(item);
			}
		}
		int targetCallBackCount = list.Count;
		if (targetCallBackCount == 0)
		{
			com.onClick.Release();
		}
		list.ForEach(delegate(RPCAsyncResult result)
		{
			result.OnFinished.AddOnce(delegate
			{
				callbackCount++;
				if (callbackCount == targetCallBackCount)
				{
					com.onClick.Release();
				}
			});
		});
		com.Status.selectedIndex = 0;
	}

	public override void Refresh()
	{
		base.Refresh();
		bool flag = false;
		foreach (MissionData missionData in missionDatas)
		{
			int num = ((!missionData.TaskRunning) ? 1 : 0);
			num = (missionData._FinishStatus ? 2 : num);
			if (num == 1)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			com.Status.selectedIndex = 1;
		}
		else
		{
			com.Status.selectedIndex = 0;
		}
	}
}
