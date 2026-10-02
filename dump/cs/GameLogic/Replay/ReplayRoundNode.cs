using System.Collections.Generic;

namespace GameLogic.Replay;

public class ReplayRoundNode
{
	public int Round;

	public int FrameIndex;

	public int PlaybackStartIndex;

	public List<ReplayTurnNode> Turns = new List<ReplayTurnNode>();
}
