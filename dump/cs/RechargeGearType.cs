using Google.Protobuf.Reflection;
using UnityEngine;

public enum RechargeGearType
{
	[InspectorName("无")]
	[OriginalName("RechargeGearType_None")]
	None,
	[InspectorName("低")]
	[OriginalName("RechargeGearType_Low")]
	Low,
	[InspectorName("中")]
	[OriginalName("RechargeGearType_Middle")]
	Middle,
	[InspectorName("高")]
	[OriginalName("RechargeGearType_High")]
	High
}
