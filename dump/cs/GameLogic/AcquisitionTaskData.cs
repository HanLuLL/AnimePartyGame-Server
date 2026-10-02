using System.Collections.Generic;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class AcquisitionTaskData : BaseTaskData
{
	public AcquisitionTaskConfigure _Config;

	private int _ActivityId;

	public AcquisitionTaskData(int activityId, AcquisitionTaskConfigure config)
	{
		_ActivityId = activityId;
		_Config = config;
		rewards = new List<KeyValuePair<int, int>>(_Config.Reward.Count);
		foreach (KeyValuePair<int, int> item in _Config.Reward)
		{
			rewards.Add(new KeyValuePair<int, int>(item.Key, item.Value));
		}
	}

	protected override int ConfigID()
	{
		return _Config.Id;
	}

	protected override bool Running()
	{
		return base._Progress < _Config.Params[0];
	}

	public override int GetWay()
	{
		return _Config.Way;
	}

	protected override int GetTarget()
	{
		return 0;
	}

	protected override int GetProgress()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.acquisition.GetProgress(_ActivityId, _Config.Id, (int)_Config.ConditionType);
	}

	protected override bool GetStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.acquisition.GetStatus(_ActivityId, _Config.Id);
	}

	public override int GetOrderWeight()
	{
		return _Config.OrderWeight;
	}

	public override string GetTaskDesc()
	{
		if (_Config.ConditionType == ConditionType.TotalPlayerCountInvitedLv)
		{
			if (_Config.Params.Count < 2)
			{
				Debug.LogError($"任务类型{_Config.ConditionType} ID:{_Config.Id}的任务配置的参数个数{_Config.Params.Count} 不满足需求");
				return "";
			}
			return string.Format(_Config.DescId.GetLocal(UIStringType.Acquisition), _Config.Params[0], _Config.Params[1]);
		}
		return string.Format(_Config.DescId.GetLocal(UIStringType.Acquisition), (_Config.Params.Count > 0) ? ((object)_Config.Params[0]) : "");
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
