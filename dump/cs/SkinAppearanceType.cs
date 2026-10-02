using Google.Protobuf.Reflection;
using UnityEngine;

public enum SkinAppearanceType
{
	[InspectorName("无")]
	[OriginalName("SkinAppearanceType_None")]
	None,
	[InspectorName("蓝色")]
	[OriginalName("SkinAppearanceType_Sapphire")]
	Sapphire,
	[InspectorName("紫色")]
	[OriginalName("SkinAppearanceType_Amethyst")]
	Amethyst,
	[InspectorName("彩色")]
	[OriginalName("SkinAppearanceType_Ultimate")]
	Ultimate,
	[InspectorName("特典")]
	[OriginalName("SkinAppearanceType_Emerald")]
	Emerald,
	[InspectorName("铂金")]
	[OriginalName("SkinAppearanceType_Platinum")]
	Platinum
}
