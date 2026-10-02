using Google.Protobuf.Reflection;
using UnityEngine;

public enum RelicKeyWordType
{
	[InspectorName("无")]
	[OriginalName("RelicKeyWordType_None")]
	None,
	[InspectorName("治愈")]
	[OriginalName("RelicKeyWordType_Cure")]
	Cure,
	[InspectorName("工资")]
	[OriginalName("RelicKeyWordType_Salary")]
	Salary,
	[InspectorName("标记")]
	[OriginalName("RelicKeyWordType_Mark")]
	Mark,
	[InspectorName("充能")]
	[OriginalName("RelicKeyWordType_Energy")]
	Energy
}
