using Google.Protobuf.Reflection;
using UnityEngine;

public enum ChoosingTimeType
{
	[InspectorName("无")]
	[OriginalName("ChoosingTimeType_None")]
	None,
	[InspectorName("短")]
	[OriginalName("ChoosingTimeType_Shot")]
	Shot,
	[InspectorName("中")]
	[OriginalName("ChoosingTimeType_Middle")]
	Middle,
	[InspectorName("长")]
	[OriginalName("ChoosingTimeType_Long")]
	Long
}
