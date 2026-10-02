using UnityEngine;

namespace GameLogic;

public abstract class ActivityBaseData
{
	public ActivityInfoConfigure activityConfig;

	public ActivityBaseData(int _activityId)
	{
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(_activityId, out activityConfig))
		{
			Debug.LogError($"无法通过活动ID{_activityId}取得Activity.InfoDict的数据");
		}
	}

	public virtual bool GetActivityStatus()
	{
		return false;
	}

	public virtual string GetDurationText()
	{
		return null;
	}

	public virtual bool IsActivityComplete()
	{
		return false;
	}

	public T child<T>() where T : ActivityBaseData
	{
		return this as T;
	}
}
