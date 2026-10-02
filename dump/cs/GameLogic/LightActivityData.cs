using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class LightActivityData : ActivityBaseData
{
	public int consumePropId;

	public int consumeNumber;

	public readonly Dictionary<int, int> LightDict = new Dictionary<int, int>();

	public readonly Dictionary<int, int> RewardDict = new Dictionary<int, int>();

	public ActivityLightingConfigure LightInfoConfig;

	private RepeatedField<ActivityScratchoffPoolConfigureItem> _PoolData;

	public int LightIndex
	{
		get
		{
			foreach (KeyValuePair<int, int> item in LightDict)
			{
				if (!RewardDict.ContainsKey(item.Key))
				{
					return item.Value;
				}
			}
			return -1;
		}
	}

	public RepeatedField<ActivityScratchoffPoolConfigureItem> PoolData
	{
		get
		{
			if (_PoolData == null)
			{
				if (!StaticConfigure.Activity.ScratchoffPoolDict.TryGetValue(LightInfoConfig.ScratchoffPoolId, out var value))
				{
					Debug.LogError($"当前卡池ID:{LightInfoConfig.ScratchoffPoolId} 无法取得点亮活动配置卡池数据");
					return null;
				}
				_PoolData = value.ActivityScratchoffPoolConfigureItems;
			}
			return _PoolData;
		}
	}

	public LightActivityData(int activityId)
		: base(activityId)
	{
		InitConfig();
	}

	private void InitConfig()
	{
		if (!StaticConfigure.Activity.LightingDict.TryGetValue(activityConfig.Id, out LightInfoConfig))
		{
			Debug.LogError($"无法通过活动ID{activityConfig.Id}取得Activity.LightingDict的数据");
			return;
		}
		KeyValuePair<int, int> keyValuePair = LightInfoConfig.Spends.ElementAt(0);
		consumePropId = keyValuePair.Key;
		consumeNumber = keyValuePair.Value;
	}

	public void UpdateGift(LightGift data)
	{
		foreach (KeyValuePair<int, int> item in data.LightGift_)
		{
			LightDict[item.Value] = item.Key;
		}
		foreach (KeyValuePair<int, int> reward in data.Rewards)
		{
			RewardDict[reward.Value] = reward.Key;
		}
	}

	public bool IsLighted(int configIndex)
	{
		return LightDict.ContainsKey(configIndex);
	}

	public bool IsFinished(int configIndex)
	{
		return RewardDict.ContainsKey(configIndex);
	}

	public void UpdateLightData(int ConfIndex, int Index)
	{
		LightDict[ConfIndex] = Index;
	}

	public ActivityScratchoffPoolConfigureItem GetLightData(int index)
	{
		foreach (KeyValuePair<int, int> item in LightDict)
		{
			if (item.Value != index)
			{
				continue;
			}
			foreach (ActivityScratchoffPoolConfigureItem poolDatum in PoolData)
			{
				if (poolDatum.Index == item.Key)
				{
					return poolDatum;
				}
			}
		}
		return null;
	}

	public override bool GetActivityStatus()
	{
		return false;
	}

	public override string GetDurationText()
	{
		return TimeHelper.GetDurationText(activityConfig.BeginTime, activityConfig.EndTime, OnlyDuration: true);
	}
}
