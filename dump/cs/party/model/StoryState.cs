using Google.Protobuf.Reflection;

namespace party.model;

public enum StoryState
{
	[OriginalName("None")]
	None,
	[OriginalName("GameBeginState")]
	GameBeginState,
	[OriginalName("BeforeVoteState")]
	BeforeVoteState,
	[OriginalName("VoteFinishState")]
	VoteFinishState,
	[OriginalName("GameEndState")]
	GameEndState,
	[OriginalName("MissionCompleteState")]
	MissionCompleteState
}
