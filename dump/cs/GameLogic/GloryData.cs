using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;

namespace GameLogic;

public class GloryData
{
	private readonly List<GloryTaskData> tasksData;

	public GloryData()
	{
		RepeatedField<PlayerRewardConfigure> rewards = StaticConfigure.Player.Rewards;
		tasksData = new List<GloryTaskData>(rewards.Count);
		for (int i = 0; i < rewards.Count; i++)
		{
			tasksData.Add(new GloryTaskData(rewards[i]));
		}
	}

	public List<GloryTaskData> GetTasksData()
	{
		if (tasksData == null || tasksData.Count == 0)
		{
			return null;
		}
		tasksData.Sort((GloryTaskData x, GloryTaskData y) => SimpleSingletonProvider<GameLogicManager>.inst.task.CompareTo(x, y));
		return tasksData;
	}
}
