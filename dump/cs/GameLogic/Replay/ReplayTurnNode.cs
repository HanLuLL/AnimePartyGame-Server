using party.protocol;

namespace GameLogic.Replay;

public class ReplayTurnNode
{
	public int FrameIndex;

	public int PlaybackStartIndex;

	public int TurnIndex;

	public long PlayerId;

	public int Round;

	public string DisplayName;

	public ReplaySnapshotS2C Snapshot;

	public TurnBehavior Behaviors;
}
