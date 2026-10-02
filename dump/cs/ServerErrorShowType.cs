using Google.Protobuf.Reflection;
using UnityEngine;

public enum ServerErrorShowType
{
	[InspectorName("不显示")]
	[OriginalName("ServerErrorShowType_None")]
	None,
	[InspectorName("显示Tips")]
	[OriginalName("ServerErrorShowType_ShowTips")]
	ShowTips,
	[InspectorName("显示OKCancel类型确认框")]
	[OriginalName("ServerErrorShowType_ShowOKCancelWindow")]
	ShowOkcancelWindow,
	[InspectorName("显示OK类型确认框")]
	[OriginalName("ServerErrorShowType_ShowOKWindow")]
	ShowOkwindow
}
