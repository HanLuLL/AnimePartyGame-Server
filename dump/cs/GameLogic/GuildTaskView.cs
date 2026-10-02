using party.model;

namespace GameLogic;

public sealed class GuildTaskView
{
	public GuildMissionConfigure Configure { get; }

	public TaskDSO Task { get; }

	public GuildTaskView(GuildMissionConfigure configure, TaskDSO task)
	{
		Configure = configure;
		Task = task;
	}
}
