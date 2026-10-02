using Google.Protobuf.Reflection;
using UnityEngine;

public enum GoodsRefreshType
{
	[InspectorName("无")]
	[OriginalName("GoodsRefreshType_None")]
	None,
	[InspectorName("每日刷新")]
	[OriginalName("GoodsRefreshType_Daily")]
	Daily,
	[InspectorName("每周刷新")]
	[OriginalName("GoodsRefreshType_Weekly")]
	Weekly,
	[InspectorName("每月刷新")]
	[OriginalName("GoodsRefreshType_Monthly")]
	Monthly
}
