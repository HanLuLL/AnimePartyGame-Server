using System.Collections.Generic;
using Tools;

namespace GameLogic;

public class SevenTaskData : BaseTaskData
{
	public TaskDay7Configure _Config;

	private int targetDay;

	public SevenTaskData(TaskDay7Configure config, int day)
	{
		_Config = config;
		targetDay = day;
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

	public override int GetWay()
	{
		return _Config.Way;
	}

	protected override int GetProgress()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1((int)_Config.ConditionType);
	}

	protected override bool GetStatus()
	{
		TaskLogic task = SimpleSingletonProvider<GameLogicManager>.inst.task;
		if (task.sevenDayData.SystemProgress < targetDay)
		{
			return true;
		}
		return task.taskFinishIds.Contains(_Config.Id);
	}

	public override int GetOrderWeight()
	{
		return 0;
	}

	protected override int GetTarget()
	{
		return 0;
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
