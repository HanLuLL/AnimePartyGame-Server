using System.Collections.Generic;
using Tools;

namespace GameLogic;

public class GloryTaskData : BaseTaskData
{
	public PlayerRewardConfigure _Config;

	public GloryTaskData(PlayerRewardConfigure config)
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
		return _Config.Level;
	}

	protected override bool Running()
	{
		return base._Progress < _Config.Level;
	}

	public override int GetWay()
	{
		return 0;
	}

	protected override int GetTarget()
	{
		return 0;
	}

	protected override int GetProgress()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
	}

	protected override bool GetStatus()
	{
		return false;
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
