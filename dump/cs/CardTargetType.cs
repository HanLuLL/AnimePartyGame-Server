using Google.Protobuf.Reflection;
using UnityEngine;

public enum CardTargetType
{
	[InspectorName("无")]
	[OriginalName("CardTargetType_None")]
	None,
	[InspectorName("自己")]
	[OriginalName("CardTargetType_Self")]
	Self,
	[InspectorName("目标")]
	[OriginalName("CardTargetType_Target")]
	Target,
	[InspectorName("地块")]
	[OriginalName("CardTargetType_Land")]
	Land
}
