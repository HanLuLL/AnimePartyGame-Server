using Google.Protobuf.Reflection;
using UnityEngine;

public enum PerformTransform
{
	[InspectorName("无")]
	[OriginalName("PerformTransform_None")]
	None,
	[InspectorName("拉伸消失")]
	[OriginalName("PerformTransform_StretchDisappear")]
	StretchDisappear,
	[InspectorName("插值出现")]
	[OriginalName("PerformTransform_LerpShow")]
	LerpShow,
	[InspectorName("压扁消失")]
	[OriginalName("PerformTransform_FlattenDisappear")]
	FlattenDisappear,
	[InspectorName("直接出现")]
	[OriginalName("PerformTransform_Show")]
	Show
}
