using Google.Protobuf.Reflection;
using UnityEngine;

public enum TaskRefreshType
{
	[InspectorName("无")]
	[OriginalName("TaskRefreshType_None")]
	None,
	[InspectorName("每日刷新")]
	[OriginalName("TaskRefreshType_Daily")]
	Daily,
	[InspectorName("每周刷新")]
	[OriginalName("TaskRefreshType_Weekly")]
	Weekly,
	[InspectorName("每月刷新")]
	[OriginalName("TaskRefreshType_Monthly")]
	Monthly
}
