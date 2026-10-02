using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class ScratchOffActivityData : TaskActivityData
{
	public readonly ActivityScratchoffConfigure ScratchOffConfig;

	public readonly int consumePropId;

	public readonly int consumeNumber;

	private int _CurPoolId;

	public readonly Dictionary<int, int> record = new Dictionary<int, int>();

	private int _totalScratchedCount;

	public int curPoolId
	{
		get
		{
			return _CurPoolId;
		}
		set
		{
			if (_CurPoolId != value)
			{
				record.Clear();
				_CurPoolId = value;
			}
		}
	}

	public int CurPage => ScratchOffConfig.ScratchoffPoolIds.IndexOf(curPoolId) + 1;

	public int MaxPage => ScratchOffConfig.ScratchoffPoolIds.Count;

	public RepeatedField<ActivityScratchoffPoolConfigureItem> PoolData
	{
		get
		{
			if (!StaticConfigure.Activity.ScratchoffPoolDict.TryGetValue(curPoolId, out var value))
			{
				Debug.LogError($"当前卡池ID:{curPoolId} 无法取得刮刮乐配置卡池数据");
				return null;
			}
			return value.ActivityScratchoffPoolConfigureItems;
		}
	}

	public int TotalScratchCardCount
	{
		get
		{
			MapField<int, ActivityScratchoffPoolConfigure> scratchoffPoolDict = StaticConfigure.Activity.ScratchoffPoolDict;
			int num = 0;
			foreach (int scratchoffPoolId in ScratchOffConfig.ScratchoffPoolIds)
			{
				if (scratchoffPoolDict.TryGetValue(scratchoffPoolId, out var value))
				{
					num += value.ActivityScratchoffPoolConfigureItems.Count;
				}
			}
			return num;
		}
	}

	public ScratchOffActivityData(int _activityId)
		: base(_activityId)
	{
		if (!StaticConfigure.Activity.ScratchoffDict.TryGetValue(activityConfig.Id, out ScratchOffConfig))
		{
			Debug.LogError($"无法通过活动ID{activityConfig.Id}取得Activity.ScratchoffDict的数据");
			return;
		}
		KeyValuePair<int, int> keyValuePair = ScratchOffConfig.Spends.ElementAt(0);
		consumePropId = keyValuePair.Key;
		consumeNumber = keyValuePair.Value;
		UpdateDataByServer(null);
	}

	public override string GetDurationText()
	{
		return TimeHelper.GetDurationText(ScratchOffConfig.BeginTime, ScratchOffConfig.EndTime, OnlyDuration: true);
	}

	public ActivityScratchoffPoolConfigureItem GetRecordConfig(int index)
	{
		if (record.TryGetValue(index, out var value))
		{
			for (int i = 0; i < PoolData.Count; i++)
			{
				if (PoolData[i].Index == value)
				{
					return PoolData[i];
				}
			}
			return null;
		}
		return null;
	}

	public void UpdateDataByServer(ScratchCardRecord scratchOffData)
	{
		record.Clear();
		_totalScratchedCount = 0;
		if (scratchOffData == null)
		{
			_CurPoolId = ScratchOffConfig.ScratchoffPoolIds[0];
			return;
		}
		_CurPoolId = ((scratchOffData.CurrentPoolId == 0) ? ScratchOffConfig.ScratchoffPoolIds[0] : scratchOffData.CurrentPoolId);
		foreach (KeyValuePair<int, ScratchCardPool> item in scratchOffData.Pool)
		{
			_totalScratchedCount += item.Value.Record.Count;
		}
		if (!scratchOffData.Pool.TryGetValue(_CurPoolId, out var value))
		{
			return;
		}
		foreach (KeyValuePair<int, int> item2 in value.Record)
		{
			record.TryAdd(item2.Key, item2.Value);
		}
	}

	public void UpdateRecord(int index, int configIndex)
	{
		if (!record.ContainsKey(index))
		{
			record.TryAdd(index, configIndex);
			_totalScratchedCount++;
		}
		else
		{
			record[index] = configIndex;
		}
	}

	public bool ScratchOffDataLicense()
	{
		if (CurPage == MaxPage && record.Count == PoolData.Count)
		{
			return false;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(consumePropId) >= consumeNumber;
	}

	public bool NextPageLicense()
	{
		if (CurPage != MaxPage && record.Count == PoolData.Count)
		{
			return true;
		}
		return false;
	}

	public bool IsFinishItem(int itemID)
	{
		foreach (KeyValuePair<int, int> item in record)
		{
			if (item.Value == itemID)
			{
				return true;
			}
		}
		return false;
	}

	public override bool GetActivityStatus()
	{
		if (!TimeHelper.ValidityTime(ScratchOffConfig.BeginTime, ScratchOffConfig.EndTime))
		{
			return false;
		}
		if (ScratchOffDataLicense())
		{
			return true;
		}
		return base.GetActivityStatus();
	}

	public bool IsAllTaskRewardClaimed()
	{
		if (taskDataDict == null || taskDataDict.Count == 0)
		{
			return true;
		}
		foreach (KeyValuePair<int, BaseTaskData> item in taskDataDict)
		{
			if (item.Value != null && item.Value.ValidityTime() && !item.Value._FinishStatus)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsAllScratchOffRewardObtained()
	{
		if (_totalScratchedCount >= TotalScratchCardCount)
		{
			return TotalScratchCardCount > 0;
		}
		return false;
	}

	public override bool IsActivityComplete()
	{
		if (IsAllTaskRewardClaimed())
		{
			return IsAllScratchOffRewardObtained();
		}
		return false;
	}
}
