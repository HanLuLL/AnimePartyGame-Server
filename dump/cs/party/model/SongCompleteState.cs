using Google.Protobuf.Reflection;

namespace party.model;

public enum SongCompleteState
{
	[OriginalName("NoneComplete")]
	NoneComplete,
	[OriginalName("Complete")]
	Complete,
	[OriginalName("AllCombo")]
	AllCombo,
	[OriginalName("AllPerfect")]
	AllPerfect
}
