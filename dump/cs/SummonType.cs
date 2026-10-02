using Google.Protobuf.Reflection;
using UnityEngine;

public enum SummonType
{
	[InspectorName("无")]
	[OriginalName("SummonType_None")]
	None,
	[InspectorName("陷阱")]
	[OriginalName("SummonType_Trap")]
	Trap,
	[InspectorName("路障")]
	[OriginalName("SummonType_Barricade")]
	Barricade,
	[InspectorName("金币")]
	[OriginalName("SummonType_Gold")]
	Gold,
	[InspectorName("拾取物")]
	[OriginalName("SummonType_Pickup")]
	Pickup,
	[InspectorName("可控单位")]
	[OriginalName("SummonType_ControllableUnit")]
	ControllableUnit
}
