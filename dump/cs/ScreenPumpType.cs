using Google.Protobuf.Reflection;
using UnityEngine;

public enum ScreenPumpType
{
	[InspectorName("无")]
	[OriginalName("ScreenPumpType_None")]
	None,
	[InspectorName("小")]
	[OriginalName("ScreenPumpType_Slight")]
	Slight,
	[InspectorName("中")]
	[OriginalName("ScreenPumpType_Middle")]
	Middle,
	[InspectorName("大")]
	[OriginalName("ScreenPumpType_Heavy")]
	Heavy
}
