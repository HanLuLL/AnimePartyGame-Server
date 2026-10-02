using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace GameLogic;

public class RookieTaskDatas
{
	public List<RookieGroupTaskData> TaskGroupDatas = new List<RookieGroupTaskData>();

	private int _curTaskGroupIndex;

	public const int RookieTaskActivityId = 599990;

	public RookieTaskDatas()
	{
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(599990, out var value))
		{
			return;
		}
		foreach (int missionID in value.MissionIDs)
		{
			if (!StaticConfigure.Mission.DataDict.TryGetValue(missionID, out var value2))
			{
				Debug.LogError($"无法找到{missionID}的mission任务");
				continue;
			}
			RepeatedField<int> frontID = value2.FrontID;
			if (frontID != null && frontID.Count > 1)
			{
				TaskGroupDatas.Add(new RookieGroupTaskData(new List<int>(value2.FrontID), missionID));
			}
		}
	}

	public RookieGroupTaskData GetCurTaskGroup()
	{
		if (TaskGroupDatas == null || TaskGroupDatas.Count == 0)
		{
			Debug.LogError($"新手任务数据异常！！！ CurTaskGroupIndex[{_curTaskGroupIndex}]");
			return null;
		}
		return TaskGroupDatas[_curTaskGroupIndex];
	}

	public void UpdateRookieTask()
	{
		int num = -1;
		for (int i = 0; i < TaskGroupDatas.Count; i++)
		{
			MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(TaskGroupDatas[i].FinalTaskId);
			if (missionData != null && missionData._FinishStatus)
			{
				num = i;
			}
		}
		if (num < 0)
		{
			_curTaskGroupIndex = 0;
		}
		else if (num >= _curTaskGroupIndex && num < TaskGroupDatas.Count - 1)
		{
			_curTaskGroupIndex = num + 1;
		}
		else
		{
			_curTaskGroupIndex = num;
		}
	}

	public int GetCurTaskIndex()
	{
		return _curTaskGroupIndex;
	}
}
