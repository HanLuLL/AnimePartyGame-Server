using System.Collections.Generic;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class ActivityTaskData : BaseTaskData
{
	public ActivityTaskConfigure _Config;

	private int _ActivityId;

	public ActivityTaskData(int activityId, ActivityTaskConfigure config)
	{
		_ActivityId = activityId;
		_Config = config;
		rewards = new List<KeyValuePair<int, int>>(_Config.Reward.Count);
		foreach (KeyValuePair<int, int> item in _Config.Reward)
		{
			rewards.Add(new KeyValuePair<int, int>(item.Key, item.Value));
		}
		BeginTime = _Config.BeginTime;
		EndTime = _Config.EndTime;
	}

	protected override int ConfigID()
	{
		return _Config.Id;
	}

	protected override bool Running()
	{
		return base._Progress < _Config.Param;
	}

	public override int GetWay()
	{
		return _Config.Way;
	}

	protected override int GetProgress()
	{
		TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(_ActivityId);
		if (taskActivityData == null)
		{
			Debug.LogError("活动Id: {activityId} 非任务类型活动，无法取出任务进度");
			return 0;
		}
		ConditionType conditionType = _Config.ConditionType;
		if (conditionType == ConditionType.TotalMapPlayCount || conditionType == ConditionType.TotalCharacterWin || conditionType == ConditionType.TotalCharacterDone || conditionType == ConditionType.TotalMapModePlayCount || conditionType == ConditionType.OwnRole || conditionType == ConditionType.TotalWinGameCountPve || conditionType == ConditionType.SingleMaxProgress15WinPve || conditionType == ConditionType.SingleMaxNoDieWinPve || conditionType == ConditionType.SingleMaxQuickWinNightmare || conditionType == ConditionType.SingleMaxQuickWinInsane || conditionType == ConditionType.TotalGet3GoldRelicInsaneTimes || conditionType == ConditionType.FavorLv5 || conditionType == ConditionType.TotalPvprankTimes || conditionType == ConditionType.TotalPvewinDifficulty1Times || conditionType == ConditionType.TotalPvewinDifficulty2OrHarderTimes || conditionType == ConditionType.UseTraitorCardInsane || conditionType == ConditionType.TotalPvewinDifficulty4Times || conditionType == ConditionType.TotalPvewinDifficulty5Times || conditionType == ConditionType.TotalMapWinTimes || conditionType == ConditionType.TotalKillUnitCount || conditionType == ConditionType.SpecifyMapWinGameCount)
		{
			return taskActivityData.GetAchieveInfo_2((int)_Config.ConditionType, _Config.Ref)?.Param ?? 0;
		}
		return taskActivityData.activityAchieveInfo.GetValueOrDefault((int)_Config.ConditionType, 0);
	}

	protected override bool GetStatus()
	{
		TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(_ActivityId);
		if (taskActivityData == null)
		{
			Debug.LogError("活动Id: {activityId} 非任务类型活动，无法取出任务进度");
			return false;
		}
		return taskActivityData.GetStatus(_Config.Id);
	}

	public override int GetOrderWeight()
	{
		return _Config.OrderWeight;
	}

	public override string GetTaskTitle()
	{
		return _Config.NameID.GetLocal(UIStringType.Activity);
	}

	protected override int GetTarget()
	{
		return _Config.Param;
	}

	public override string GetTaskDesc()
	{
		ConditionType conditionType = _Config.ConditionType;
		if (conditionType == ConditionType.TotalMapPlayCount || conditionType == ConditionType.SingleMaxQuickWinNightmare || conditionType == ConditionType.SingleMaxQuickWinInsane || conditionType == ConditionType.TotalGet3GoldRelicInsaneTimes || conditionType == ConditionType.TotalPvewinDifficulty1Times || conditionType == ConditionType.TotalPvewinDifficulty2OrHarderTimes || conditionType == ConditionType.TotalPvewinDifficulty4Times || conditionType == ConditionType.TotalPvewinDifficulty5Times || conditionType == ConditionType.TotalMapWinTimes || conditionType == ConditionType.SpecifyMapWinGameCount)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Param, _Config.Ref.GetMapDataConfigure().MapName.GetLocal(UIStringType.Map));
		}
		if (_Config.ConditionType == ConditionType.TotalMapModePlayCount)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Param, _Config.Ref.GetGameModeInfoConfigure().NameID.GetLocal(UIStringType.GameMode));
		}
		if (_Config.ConditionType == ConditionType.FavorLv5)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), CharacterHandle.GetCharacterName(_Config.Ref));
		}
		conditionType = _Config.ConditionType;
		if (conditionType == ConditionType.TotalKillUnitCount || conditionType == ConditionType.TotalCharacterWin)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Param, CharacterHandle.GetCharacterName(_Config.Ref));
		}
		if (_Config.ConditionType == ConditionType.TotalPvprankTimes)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Param, _Config.Ref);
		}
		if (_Config.ConditionType == ConditionType.UseTraitorCardInsane)
		{
			return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Ref);
		}
		return string.Format(_Config.DescId.GetLocal(UIStringType.Activity), _Config.Param);
	}

	public override TaskRefreshType GetTaskRefreshType()
	{
		return TaskRefreshType.None;
	}

	public override string GetTaskRefreshTypeLocal()
	{
		return null;
	}
}
