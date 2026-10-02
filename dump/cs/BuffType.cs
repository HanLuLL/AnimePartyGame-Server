using Google.Protobuf.Reflection;
using UnityEngine;

public enum BuffType
{
	[InspectorName("无")]
	[OriginalName("BuffType_None")]
	None,
	[InspectorName("正常")]
	[OriginalName("BuffType_Normal")]
	Normal,
	[InspectorName("标记")]
	[OriginalName("BuffType_Mark")]
	Mark,
	[InspectorName("关联")]
	[OriginalName("BuffType_Relate")]
	Relate,
	[InspectorName("战斗")]
	[OriginalName("BuffType_Battle")]
	Battle,
	[InspectorName("事件")]
	[OriginalName("BuffType_Event")]
	Event
}
