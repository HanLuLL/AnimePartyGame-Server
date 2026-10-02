namespace SinglePlayer.GamePlay.Build;

public sealed class BuildingExecutionItem
{
	public BuildingBase Building { get; }

	public BuildingExecutionType ExecutionType { get; }

	public int TriggerCount { get; }

	public BuildingExecutionItem(BuildingBase building, BuildingExecutionType executionType, int triggerCount = 1)
	{
		Building = building;
		ExecutionType = executionType;
		TriggerCount = triggerCount;
	}
}
