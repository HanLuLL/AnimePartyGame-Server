using Google.Protobuf.Reflection;
using UnityEngine;

public enum Day7GiftPackageType
{
	[InspectorName("无")]
	[OriginalName("Day7GiftPackageType_None")]
	None,
	[InspectorName("PVP七日礼包")]
	[OriginalName("Day7GiftPackageType_PVP")]
	Pvp,
	[InspectorName("PVE七日礼包")]
	[OriginalName("Day7GiftPackageType_PVE")]
	Pve
}
