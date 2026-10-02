using System.Collections.Generic;
using Tools;

namespace GameLogic;

public class AchieveData : BaseTaskData
{
	public AchieveGlobalConfigure _Config;

	public AchieveData(AchieveGlobalConfigure config)
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
		return 0;
	}

	protected override int GetTarget()
	{
		return _Config.Param;
	}

	protected override int GetProgress()
	{
		ConditionType conditionType = _Config.ConditionType;
		if (conditionType == ConditionType.TotalCharacterDone || conditionType == ConditionType.TotalCharacterWin || conditionType == ConditionType.TotalWinGameCountPve || conditionType == ConditionType.SingleMaxProgress15WinPve || conditionType == ConditionType.SingleMaxNoDieWinPve || conditionType == ConditionType.SingleMaxQuickWinNightmare || conditionType == ConditionType.SingleMaxQuickWinInsane)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_2((int)_Config.ConditionType, _Config.Ref)?.Param ?? 0;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1((int)_Config.ConditionType);
	}

	protected override bool GetStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.task.achieveFinishIds.Contains(_Config.Id);
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
