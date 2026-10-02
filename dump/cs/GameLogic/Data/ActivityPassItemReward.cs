using System.Collections.Generic;
using Google.Protobuf.Collections;
using UI;

namespace GameLogic.Data;

public class ActivityPassItemReward
{
	public enum RewardType
	{
		Undone,
		Completed,
		Claimed
	}

	public List<ActivityPassItemRewardData> Rewards;

	public MissionData MissionData;

	public bool IsPremium;

	public ActivityPassItemData PassItemData { get; }

	public ActivityPassItemReward(MapField<int, int> itemData, ActivityPassItemData _data)
	{
		PassItemData = _data;
		Rewards = new List<ActivityPassItemRewardData>(itemData.Count);
		foreach (KeyValuePair<int, int> itemDatum in itemData)
		{
			Rewards.Add(new ActivityPassItemRewardData(itemDatum.Key.GetItemInfoConfigure(), itemDatum.Value, PassItemData));
		}
	}

	public void RefreshInfo(MissionData _missionData, bool _isPremium)
	{
		MissionData = _missionData;
		IsPremium = _isPremium;
	}

	public RewardType GetRewardType()
	{
		switch ((MissionData.TaskState)MissionData.Status)
		{
		case MissionData.TaskState.TaskStateCompleted:
			if (!IsPremium)
			{
				return RewardType.Claimed;
			}
			return RewardType.Completed;
		case MissionData.TaskState.TaskStateClaimed:
			return RewardType.Claimed;
		case MissionData.TaskState.TaskStateOneCompleted:
			if (IsPremium)
			{
				if (PassItemData.ActivityPassData.PassGear <= 1)
				{
					return RewardType.Undone;
				}
				return RewardType.Completed;
			}
			return RewardType.Completed;
		case MissionData.TaskState.TaskStateOneClaimed:
			if (!IsPremium)
			{
				return RewardType.Claimed;
			}
			return RewardType.Undone;
		default:
			return RewardType.Undone;
		}
	}
}
