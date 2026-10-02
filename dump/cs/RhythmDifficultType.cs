using Google.Protobuf.Reflection;
using UnityEngine;

public enum RhythmDifficultType
{
	[InspectorName("无")]
	[OriginalName("RhythmDifficultType_None")]
	None,
	[InspectorName("简单")]
	[OriginalName("RhythmDifficultType_Easy")]
	Easy,
	[InspectorName("困难")]
	[OriginalName("RhythmDifficultType_Hard")]
	Hard
}
