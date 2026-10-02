using System;

namespace GameLogic.PlotTree;

[Serializable]
public abstract class PlotTreeBase
{
	public string Guid;

	public string Title;

	[NonSerialized]
	public PlotState NodeState;

	public abstract PlotState Tick();

	public void ChangeFailState()
	{
		NodeState = PlotState.Failure;
		if (this is PlotComposite plotComposite)
		{
			plotComposite.ChildNodes.ForEach(delegate(PlotTreeBase node)
			{
				node.ChangeFailState();
			});
		}
	}
}
