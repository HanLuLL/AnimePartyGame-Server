using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;

namespace GameLogic;

public class SevenDayData
{
	private readonly Dictionary<int, List<SevenTaskData>> taskDict = new Dictionary<int, List<SevenTaskData>>();

	public int SystemProgress => SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1(15);

	public SevenDayData()
	{
		RepeatedField<TaskDay7Configure> day7S = StaticConfigure.Task.Day7S;
		for (int i = 0; i < day7S.Count; i++)
		{
			TaskDay7Configure taskDay7Configure = day7S[i];
			if (!taskDict.TryGetValue(taskDay7Configure.Day, out var value))
			{
				value = new List<SevenTaskData>();
				taskDict.Add(taskDay7Configure.Day, value);
			}
			SevenTaskData item = new SevenTaskData(taskDay7Configure, taskDay7Configure.Day);
			value.Add(item);
		}
	}

	public List<SevenTaskData> GetSevenDayTaskByDay(int day)
	{
		if (taskDict.TryGetValue(day, out var value))
		{
			value.Sort(CompareTo);
			return value;
		}
		return null;
	}

	private int CompareTo(SevenTaskData x, SevenTaskData y)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.CompareTo(x, y);
	}

	public int GetRunningTaskDay()
	{
		foreach (KeyValuePair<int, List<SevenTaskData>> item in taskDict)
		{
			List<SevenTaskData> value = item.Value;
			for (int i = 0; i < value.Count; i++)
			{
				if (!value[i]._FinishStatus)
				{
					return item.Key;
				}
			}
		}
		return 1;
	}

	public bool GetTaskSystemStatus()
	{
		foreach (KeyValuePair<int, List<SevenTaskData>> item in taskDict)
		{
			foreach (SevenTaskData item2 in item.Value)
			{
				if (!item2._FinishStatus && !item2.TaskRunning)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool GetTaskSystemStatusByDay(int day)
	{
		if (SystemProgress < day)
		{
			return false;
		}
		if (taskDict.TryGetValue(day, out var value))
		{
			foreach (SevenTaskData item in value)
			{
				if (!item._FinishStatus && !item.TaskRunning)
				{
					return true;
				}
			}
		}
		return false;
	}
}
