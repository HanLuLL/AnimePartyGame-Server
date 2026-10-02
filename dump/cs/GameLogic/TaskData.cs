using System.Collections.Generic;
using Tools;

namespace GameLogic;

public class TaskData : BaseTaskData
{
	public TaskBeginnerConfigure _Config;

	public TaskData(TaskBeginnerConfigure config)
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
		if (_Config.ConditionType == ConditionType.CampaignLevelPass)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_2((int)_Config.ConditionType, _Config.Ref)?.Param ?? 0;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1((int)_Config.ConditionType);
	}

	protected override bool GetStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.taskFinishIds.Contains(_Config.Id);
	}

	public override int GetOrderWeight()
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
