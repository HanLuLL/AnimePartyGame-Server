using Tools;

namespace GameLogic.Replay;

public class ReplaySignal
{
	public readonly Signal<ReplayTurnNode> turnNodeReached = new Signal<ReplayTurnNode>();

	public readonly Signal<ReplayState, ReplayState> stateChanged = new Signal<ReplayState, ReplayState>();

	public readonly Signal speedChanged = new Signal();
}
