using Google.Protobuf.Reflection;
using UnityEngine;

public enum PVEExtraModeType
{
	[InspectorName("无")]
	[OriginalName("PVEExtraModeType_None")]
	None,
	[InspectorName("词条")]
	[OriginalName("PVEExtraModeType_Mutator")]
	Mutator,
	[InspectorName("线索")]
	[OriginalName("PVEExtraModeType_Clue")]
	Clue
}
