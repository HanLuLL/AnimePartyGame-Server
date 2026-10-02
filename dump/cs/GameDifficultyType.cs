using Google.Protobuf.Reflection;
using UnityEngine;

public enum GameDifficultyType
{
	[InspectorName("简单")]
	[OriginalName("GameDifficultyType_Easy")]
	Easy,
	[InspectorName("普通")]
	[OriginalName("GameDifficultyType_Normal")]
	Normal,
	[InspectorName("噩梦")]
	[OriginalName("GameDifficultyType_Nightmare")]
	Nightmare,
	[InspectorName("疯狂")]
	[OriginalName("GameDifficultyType_Crazy")]
	Crazy,
	[InspectorName("极限")]
	[OriginalName("GameDifficultyType_Extreme")]
	Extreme
}
