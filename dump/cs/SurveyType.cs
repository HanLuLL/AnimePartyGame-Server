using Google.Protobuf.Reflection;
using UnityEngine;

public enum SurveyType
{
	[InspectorName("新人问卷")]
	[OriginalName("SurveyType_Noob")]
	Noob,
	[InspectorName("回归问卷")]
	[OriginalName("SurveyType_Return")]
	Return,
	[InspectorName("版本问卷")]
	[OriginalName("SurveyType_Version")]
	Version
}
