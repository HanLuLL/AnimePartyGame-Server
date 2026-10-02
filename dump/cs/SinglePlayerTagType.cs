using Google.Protobuf.Reflection;
using UnityEngine;

public enum SinglePlayerTagType
{
	[InspectorName("无")]
	[OriginalName("SinglePlayerTagType_None")]
	None,
	[InspectorName("繁荣")]
	[OriginalName("SinglePlayerTagType_Prosperity")]
	Prosperity,
	[InspectorName("晋升")]
	[OriginalName("SinglePlayerTagType_Promotion")]
	Promotion,
	[InspectorName("骰子")]
	[OriginalName("SinglePlayerTagType_Dice")]
	Dice,
	[InspectorName("海盗")]
	[OriginalName("SinglePlayerTagType_Pirate")]
	Pirate
}
