using Google.Protobuf.Reflection;
using UnityEngine;

public enum WelfareType
{
	[InspectorName("无")]
	[OriginalName("WelfareType_None")]
	None,
	[InspectorName("新手任务")]
	[OriginalName("WelfareType_Beginner")]
	Beginner,
	[InspectorName("周任务")]
	[OriginalName("WelfareType_Weekly")]
	Weekly,
	[InspectorName("成就")]
	[OriginalName("WelfareType_Achieve")]
	Achieve,
	[InspectorName("七天任务")]
	[OriginalName("WelfareType_Beginner7Days")]
	Beginner7Days,
	[InspectorName("荣耀之路")]
	[OriginalName("WelfareType_Glory")]
	Glory,
	[InspectorName("拉新")]
	[OriginalName("WelfareType_Acquisition")]
	Acquisition
}
