using Google.Protobuf.Reflection;
using UnityEngine;

public enum ChannelType
{
	[InspectorName("无")]
	[OriginalName("ChannelType_None")]
	None,
	[InspectorName("Google")]
	[OriginalName("ChannelType_Google")]
	Google,
	[InspectorName("IOS")]
	[OriginalName("ChannelType_IOS")]
	Ios,
	[InspectorName("DMM")]
	[OriginalName("ChannelType_DMM")]
	Dmm,
	[InspectorName("STEAM")]
	[OriginalName("ChannelType_STEAM")]
	Steam
}
