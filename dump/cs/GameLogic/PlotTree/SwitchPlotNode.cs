using System;
using UnityEngine;

namespace GameLogic.PlotTree;

[Serializable]
public class SwitchPlotNode : PlotActionNode
{
	public StoryData StoryData;

	public override PlotState Tick()
	{
		if (StoryData == null || StoryData.Root == null)
		{
			Debug.LogError("节点：" + Guid);
			return PlotState.Failure;
		}
		NodeState = StoryData.Root.Tick();
		return NodeState;
	}
}
