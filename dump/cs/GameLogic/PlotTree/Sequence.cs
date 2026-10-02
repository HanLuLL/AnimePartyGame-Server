using System;

namespace GameLogic.PlotTree;

[Serializable]
public class Sequence : PlotComposite
{
	public Sequence()
	{
		Title = "列表";
	}

	public Sequence(string name)
	{
		Title = name;
	}

	public override PlotState Tick()
	{
		if (NodeState == PlotState.None)
		{
			CurrentIndex = 0;
		}
		if (ChildNodes.Count == 0)
		{
			NodeState = PlotState.Failure;
			return PlotState.Failure;
		}
		PlotState plotState = ChildNodes[CurrentIndex].Tick();
		if (plotState == PlotState.Success)
		{
			CurrentIndex++;
			if (CurrentIndex >= ChildNodes.Count)
			{
				CurrentIndex = 0;
				NodeState = PlotState.Success;
			}
			else
			{
				NodeState = PlotState.Running;
			}
		}
		else
		{
			NodeState = plotState;
		}
		return NodeState;
	}
}
