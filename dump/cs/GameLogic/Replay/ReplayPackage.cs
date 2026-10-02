using System.Collections.Generic;

namespace GameLogic.Replay;

public class ReplayPackage
{
	public long RoomId;

	public int RoomServerId;

	public int MapId;

	public int MapType;

	public List<ReplayFrame> Frames = new List<ReplayFrame>();

	public List<ReplayTurnNode> TurnNodes = new List<ReplayTurnNode>();

	public List<ReplayRoundNode> Rounds = new List<ReplayRoundNode>();

	public int FrameCount => Frames?.Count ?? 0;

	public ReplayTurnNode FindTurnNodeByFrameIndex(int frameIndex)
	{
		if (TurnNodes == null)
		{
			return null;
		}
		for (int i = 0; i < TurnNodes.Count; i++)
		{
			if (TurnNodes[i].FrameIndex == frameIndex)
			{
				return TurnNodes[i];
			}
		}
		return null;
	}
}
