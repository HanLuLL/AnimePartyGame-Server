using Google.Protobuf.Reflection;
using UnityEngine;

public enum LanguageType
{
	[InspectorName("无")]
	[OriginalName("LanguageType_None")]
	None = 0,
	[InspectorName("英")]
	[OriginalName("LanguageType_English")]
	English = 10,
	[InspectorName("日")]
	[OriginalName("LanguageType_Japanese")]
	Japanese = 22,
	[InspectorName("韩")]
	[OriginalName("LanguageType_Korean")]
	Korean = 23,
	[InspectorName("简中")]
	[OriginalName("LanguageType_SimplifiedChinese")]
	SimplifiedChinese = 40,
	[InspectorName("繁中")]
	[OriginalName("LanguageType_TraditionalChinese")]
	TraditionalChinese = 41
}
