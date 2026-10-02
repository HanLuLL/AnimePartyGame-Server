using Tools;

namespace GameLogic;

public class BattlePassTaskData : BaseTaskData
{
	private readonly int battlePassId;

	public BattlePassTaskConfigureItem taskConfig;

	public BattlePassTaskData(int _battlePassId, BattlePassTaskConfigureItem _taskConfig)
	{
		battlePassId = _battlePassId;
		taskConfig = _taskConfig;
	}

	protected override int ConfigID()
	{
		return taskConfig.Id;
	}

	protected override bool Running()
	{
		return base._Progress < taskConfig.Param;
	}

	public override int GetWay()
	{
		return 0;
	}

	protected override int GetTarget()
	{
		return taskConfig.Param;
	}

	protected override int GetProgress()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetProgress(battlePassId, taskConfig.Id, (int)taskConfig.ConditionType);
	}

	protected override bool GetStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetStatus(battlePassId, taskConfig.Id);
	}

	public override int GetOrderWeight()
	{
		return taskConfig.OrderWeight;
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
