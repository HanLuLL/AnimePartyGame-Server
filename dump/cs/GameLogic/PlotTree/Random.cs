using System;
using UnityEngine;

namespace GameLogic.PlotTree;

[Serializable]
public class Random : PlotComposite
{
	public Random()
	{
		Title = "随机分支";
	}

	public override PlotState Tick()
	{
		if (ChildNodes.Count == 0)
		{
			NodeState = PlotState.Failure;
			return PlotState.Failure;
		}
		if (NodeState != PlotState.Running)
		{
			CurrentIndex = UnityEngine.Random.Range(0, ChildNodes.Count);
		}
		NodeState = ChildNodes[CurrentIndex].Tick();
		return NodeState;
	}
}
