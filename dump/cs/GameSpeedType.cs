using Google.Protobuf.Reflection;
using UnityEngine;

public enum GameSpeedType
{
	[InspectorName("无")]
	[OriginalName("GameSpeedType_None")]
	None,
	[InspectorName("普通")]
	[OriginalName("GameSpeedType_Normal")]
	Normal,
	[InspectorName("快速")]
	[OriginalName("GameSpeedType_Fast")]
	Fast
}
