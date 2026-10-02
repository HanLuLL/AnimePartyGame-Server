using Google.Protobuf.Reflection;
using UnityEngine;

public enum UIType
{
	[InspectorName("无")]
	[OriginalName("UIType_None")]
	None,
	[InspectorName("界面")]
	[OriginalName("UIType_Panel")]
	Panel,
	[InspectorName("窗口")]
	[OriginalName("UIType_Window")]
	Window
}
