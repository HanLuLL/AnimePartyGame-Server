using Tools;
using party.model;

namespace GameLogic;

public class MatchSignal
{
	public readonly Signal StartMatch = new Signal();

	public readonly Signal<MatchTeamInfo.Types.State> CancelMatch = new Signal<MatchTeamInfo.Types.State>();

	public readonly Signal TeamChange = new Signal();

	public readonly Signal<long> UpdateReady = new Signal<long>();
}
