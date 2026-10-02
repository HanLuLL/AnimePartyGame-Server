using Google.Protobuf.Reflection;
using UnityEngine;

public enum BuffRoundCountType
{
	[InspectorName("无")]
	[OriginalName("BuffRoundCountType_None")]
	None,
	[InspectorName("轮次")]
	[OriginalName("BuffRoundCountType_Round")]
	Round,
	[InspectorName("回合")]
	[OriginalName("BuffRoundCountType_Action")]
	Action,
	[InspectorName("当前行动回合")]
	[OriginalName("BuffRoundCountType_CurrentAction")]
	CurrentAction
}
