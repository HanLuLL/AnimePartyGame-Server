using System.Collections.Generic;
using Tools;
using UI;

namespace GameLogic;

public class WeekTaskData : BaseTaskData
{
	public TaskWeeklyConfigure _Config;

	public WeekTaskData(TaskWeeklyConfigure config)
	{
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
		return base._Progress < _Config.Param;
	}

	protected override int GetProgress()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.GetWeekAchieveInfo((int)_Config.ConditionType);
	}

	protected override bool GetStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.weekTaskFinishIds.Contains(_Config.Id);
	}

	public override int GetWay()
	{
		return 0;
	}

	protected override int GetTarget()
	{
		return 0;
	}

	public override int GetOrderWeight()
	{
		return 0;
	}

	public override TaskRefreshType GetTaskRefreshType()
	{
		return TaskRefreshType.Weekly;
	}

	public override string GetTaskRefreshTypeLocal()
	{
		return 102.GetLocal(UIStringType.Mission);
	}
}
