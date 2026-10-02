using System.Collections.Generic;
using System.Linq;

namespace GameLogic;

public class DiceActivityData : TaskActivityData
{
	public DiceActivityInfoConfigure DiceActivityInfoConfig;

	private const int MissionStart = 6050000;

	private const int MissionEnd = 6059999;

	private const int RewardStart = 7050000;

	private const int RewardEnd = 7059999;

	public List<DiceActivityDataConfigureItem> DiceGrids = new List<DiceActivityDataConfigureItem>();

	public bool IsMission(int taskId)
	{
		if (taskId >= 6050000)
		{
			return taskId <= 6059999;
		}
		return false;
	}

	public bool IsReward(int taskId)
	{
		if (taskId >= 7050000)
		{
			return taskId <= 7059999;
		}
		return false;
	}

	public List<MissionData> GetSortedDiceMissionList()
	{
		List<MissionData> list = new List<MissionData>();
		foreach (KeyValuePair<int, BaseTaskData> item2 in taskDataDict)
		{
			if (IsMission(item2.Key) && item2.Value is MissionData item)
			{
				list.Add(item);
			}
		}
		list.Sort((MissionData x, MissionData y) => CompareTo(x, y));
		return list;
	}

	public List<MissionData> GetSortedRewardMissionList()
	{
		List<MissionData> list = new List<MissionData>();
		foreach (KeyValuePair<int, BaseTaskData> item2 in taskDataDict)
		{
			if (IsReward(item2.Key) && item2.Value is MissionData item)
			{
				list.Add(item);
			}
		}
		list.Sort((MissionData x, MissionData y) => x._Id - y._Id);
		return list;
	}

	public DiceActivityData(int _activityId)
		: base(_activityId)
	{
		StaticConfigure.DiceActivity.InfoDict.TryGetValue(_activityId, out DiceActivityInfoConfig);
		int mapID = DiceActivityInfoConfig.MapID;
		StaticConfigure.DiceActivity.DataDict.TryGetValue(mapID, out var value);
		DiceGrids = value.DiceActivityDataConfigureItems.ToList();
	}
}
