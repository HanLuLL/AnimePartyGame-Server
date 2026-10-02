using System.Collections.Generic;
using UnityEngine;

namespace GameLogic;

public class RookieGroupTaskData
{
	public List<int> TaskIds;

	public int FinalTaskId;

	public RookieGroupTaskData(List<int> _taskIds, int _finalTaskId)
	{
		TaskIds = _taskIds;
		FinalTaskId = _finalTaskId;
	}

	public MissionDataConfigure GetFinalTaskConfig()
	{
		StaticConfigure.Mission.DataDict.TryGetValue(FinalTaskId, out var value);
		return value;
	}

	public List<MissionDataConfigure> GetGroupTaskConfigs()
	{
		if (TaskIds == null || TaskIds.Count == 0)
		{
			return null;
		}
		List<MissionDataConfigure> list = new List<MissionDataConfigure>(TaskIds.Count);
		foreach (int taskId in TaskIds)
		{
			if (StaticConfigure.Mission.DataDict.TryGetValue(taskId, out var value))
			{
				list.Add(value);
			}
			else
			{
				Debug.LogError($"数据异常! 不存在({taskId}) Mission配置");
			}
		}
		return list;
	}
}
