using Google.Protobuf.Reflection;
using UnityEngine;

public enum ChatType
{
	[InspectorName("无")]
	[OriginalName("ChatType_None")]
	None,
	[InspectorName("房间")]
	[OriginalName("ChatType_Room")]
	Room,
	[InspectorName("游戏内")]
	[OriginalName("ChatType_InGame")]
	InGame,
	[InspectorName("结算")]
	[OriginalName("ChatType_Result")]
	Result
}
