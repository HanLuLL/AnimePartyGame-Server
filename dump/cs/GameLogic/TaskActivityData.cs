using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class TaskActivityData : ActivityBaseData
{
	public readonly Dictionary<int, BaseTaskData> taskDataDict;

	public readonly List<int> taskFinishIds;

	public readonly Dictionary<int, int> activityAchieveInfo;

	public readonly Dictionary<int, ConditionData> activityAchieveInfo2;

	public TaskActivityData(int _activityId)
		: base(_activityId)
	{
		taskDataDict = new Dictionary<int, BaseTaskData>(activityConfig.TaskIDs.Count);
		taskFinishIds = new List<int>(taskDataDict.Count);
		activityAchieveInfo = new Dictionary<int, int>();
		activityAchieveInfo2 = new Dictionary<int, ConditionData>();
		if (activityConfig.TaskIDs.Count != 0)
		{
			foreach (int taskID in activityConfig.TaskIDs)
			{
				if (StaticConfigure.Activity.TaskDict.TryGetValue(taskID, out var value))
				{
					taskDataDict.TryAdd(value.Id, new ActivityTaskData(activityConfig.Id, value));
				}
			}
			return;
		}
		if (activityConfig.MissionIDs.Count == 0)
		{
			return;
		}
		foreach (int missionID in activityConfig.MissionIDs)
		{
			MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(missionID);
			if (missionData != null)
			{
				taskDataDict.TryAdd(missionID, missionData);
			}
		}
	}

	public override string GetDurationText()
	{
		return TimeHelper.GetDurationText(activityConfig.BeginTime, activityConfig.EndTime, OnlyDuration: true);
	}

	public void UpdateTaskInfo(ActivityInfo info)
	{
		taskFinishIds.Clear();
		foreach (int taskRewardI in info.TaskRewardIs)
		{
			taskFinishIds.Add(taskRewardI);
		}
		UpdateActivityAchieve(info.Condition, Noop: true);
		UpdatePlayerAchieve_2(info.Condition1);
	}

	public void UpdateActivityAchieve(MapField<int, int> Condition, bool Noop)
	{
		if (Noop)
		{
			activityAchieveInfo.Clear();
		}
		foreach (KeyValuePair<int, int> item in Condition)
		{
			activityAchieveInfo[item.Key] = item.Value;
		}
	}

	public void UpdatePlayerAchieve_2(MapField<int, ConditionData> Condition)
	{
		activityAchieveInfo2.Clear();
		foreach (KeyValuePair<int, ConditionData> item in Condition)
		{
			activityAchieveInfo2.TryAdd(item.Key, item.Value);
		}
	}

	public void UpdatePlayerAchieve_2(ConditionData Condition)
	{
		if (Condition != null)
		{
			if (activityAchieveInfo2.ContainsKey(Condition.CondType))
			{
				activityAchieveInfo2[Condition.CondType] = Condition;
			}
			else
			{
				activityAchieveInfo2.TryAdd(Condition.CondType, Condition);
			}
		}
	}

	public ConditionParams GetAchieveInfo_2(int _type, int _param)
	{
		if (activityAchieveInfo2.TryGetValue(_type, out var value))
		{
			foreach (ConditionParams item in value.Params)
			{
				if (item.Param1 == _param)
				{
					return item;
				}
			}
		}
		return null;
	}

	public List<int> GetTaskId(bool overView)
	{
		List<int> list = new List<int>();
		foreach (var (key, baseTaskData2) in taskDataDict)
		{
			if (!baseTaskData2.ValidityTime())
			{
				continue;
			}
			if (activityConfig.TaskIDs.Count != 0)
			{
				if (StaticConfigure.Activity.TaskDict.TryGetValue(key, out var value))
				{
					BaseTaskData value2;
					if (overView || value.FrontID == 0)
					{
						list.Add(baseTaskData2._Id);
					}
					else if (!taskDataDict.TryGetValue(value.FrontID, out value2))
					{
						Debug.LogError($"未找到活动任务{value.Id}的前置活动：{value.FrontID}");
					}
					else if (value2._FinishStatus)
					{
						list.Add(baseTaskData2._Id);
					}
				}
			}
			else
			{
				if (!(baseTaskData2 is MissionData missionData))
				{
					continue;
				}
				if (overView || missionData.config.FrontID == null || missionData.config.FrontID.Count == 0)
				{
					list.Add(baseTaskData2._Id);
					continue;
				}
				bool flag = true;
				foreach (int item in missionData.config.FrontID)
				{
					if (!taskDataDict.TryGetValue(item, out var value3))
					{
						Debug.LogError($"未找到活动任务{missionData.config.Id}的前置活动：{item}");
					}
					else if (!value3._FinishStatus)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(baseTaskData2._Id);
				}
			}
		}
		list.Sort((int x, int y) => CompareTo(taskDataDict[x], taskDataDict[y]));
		return list;
	}

	protected int CompareTo(BaseTaskData x, BaseTaskData y)
	{
		int num = x._FinishStatus.CompareTo(y._FinishStatus);
		if (num != 0)
		{
			return num;
		}
		int num2 = x.TaskRunning.CompareTo(y.TaskRunning);
		if (num2 != 0)
		{
			return num2;
		}
		return x.GetOrderWeight().CompareTo(y.GetOrderWeight());
	}

	public bool GetStatus(int _taskId)
	{
		return taskFinishIds.Contains(_taskId);
	}

	public override bool GetActivityStatus()
	{
		foreach (KeyValuePair<int, BaseTaskData> item in taskDataDict)
		{
			if (!item.Value.ValidityTime() || item.Value._FinishStatus)
			{
				continue;
			}
			if (item.Value is MissionData missionData)
			{
				if (missionData.TaskRunning)
				{
					continue;
				}
				bool flag = true;
				MissionDataConfigure config = missionData.config;
				while (config != null)
				{
					RepeatedField<int> frontID = config.FrontID;
					if (frontID == null || frontID.Count <= 0)
					{
						break;
					}
					foreach (int item2 in config.FrontID)
					{
						MissionData missionData2 = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(item2);
						if (missionData2 != null)
						{
							if (!missionData2._FinishStatus)
							{
								flag = false;
								break;
							}
							config = missionData2.config;
						}
					}
					if (!flag)
					{
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			else if (!item.Value.TaskRunning)
			{
				return true;
			}
		}
		UIPanelType panelType = activityConfig.PanelType;
		if (panelType == UIPanelType.ActivitySevenDaySignIn || panelType == UIPanelType.SignInModuleOne || panelType == UIPanelType.SignInModuleTwo)
		{
			SignInLogic signIn = SimpleSingletonProvider<GameLogicManager>.inst.signIn;
			int signInByActivityId = signIn.GetSignInByActivityId(activityConfig.Id);
			if (signIn.CanSignIn(signInByActivityId))
			{
				return signIn.GetCanSignInToday(signInByActivityId);
			}
			return false;
		}
		return false;
	}
}
