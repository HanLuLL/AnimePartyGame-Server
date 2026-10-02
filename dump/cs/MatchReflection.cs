using System;
using Google.Protobuf.Reflection;

public static class MatchReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MatchReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtNYXRjaC5wcm90bxoKRW51bS5wcm90byIuChJNYXRjaEluZm9Db25maWd1" + "cmUSCgoCaWQYASABKA8SDAoEbWFwcxgCIAMoESJXChNNYXRjaFBhcm1zQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEg8KB2JhblRpbWUYAiABKA8SEQoJcmVhZHlU" + "aW1lGAMgASgPEhAKCGFma0xpbWl0GAQgASgPIpsBChhNYXRjaENyZWRpdFRp" + "ZXJDb25maWd1cmUSCgoCaWQYASABKA8SEAoIbWluU2NvcmUYAiABKA8SEAoI" + "bWF4U2NvcmUYAyABKA8SEAoIdGllck5hbWUYBCABKA8SFgoObWF0Y2hDZFNl" + "Y29uZHMYBSABKA8SEwoLcmV3YXJkUmF0aW8YBiABKA8SEAoIdGllckRlc2MY" + "ByABKA8ifgoaTWF0Y2hDcmVkaXRBY3Rpb25Db25maWd1cmUSCgoCaWQYASAB" + "KA8SKwoQY3JlZGl0QWN0aW9uVHlwZRgCIAEoDjIRLkNyZWRpdEFjdGlvblR5" + "cGUSEwoLc2NvcmVDaGFuZ2UYAyABKA8SEgoKYWN0aW9uTmFtZRgEIAEoDyLW" + "BQoOTWF0Y2hDb25maWd1cmUSIgoFSW5mb3MYASADKAsyEy5NYXRjaEluZm9D" + "b25maWd1cmUSLwoISW5mb0RpY3QYAiADKAsyHS5NYXRjaENvbmZpZ3VyZS5J" + "bmZvRGljdEVudHJ5EiQKBlBhcm1zcxgDIAMoCzIULk1hdGNoUGFybXNDb25m" + "aWd1cmUSMQoJUGFybXNEaWN0GAQgAygLMh4uTWF0Y2hDb25maWd1cmUuUGFy" + "bXNEaWN0RW50cnkSLgoLQ3JlZGl0VGllcnMYBSADKAsyGS5NYXRjaENyZWRp" + "dFRpZXJDb25maWd1cmUSOwoOQ3JlZGl0VGllckRpY3QYBiADKAsyIy5NYXRj" + "aENvbmZpZ3VyZS5DcmVkaXRUaWVyRGljdEVudHJ5EjIKDUNyZWRpdEFjdGlv" + "bnMYByADKAsyGy5NYXRjaENyZWRpdEFjdGlvbkNvbmZpZ3VyZRI/ChBDcmVk" + "aXRBY3Rpb25EaWN0GAggAygLMiUuTWF0Y2hDb25maWd1cmUuQ3JlZGl0QWN0" + "aW9uRGljdEVudHJ5GkQKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiIK" + "BXZhbHVlGAIgASgLMhMuTWF0Y2hJbmZvQ29uZmlndXJlOgI4ARpGCg5QYXJt" + "c0RpY3RFbnRyeRILCgNrZXkYASABKA8SIwoFdmFsdWUYAiABKAsyFC5NYXRj" + "aFBhcm1zQ29uZmlndXJlOgI4ARpQChNDcmVkaXRUaWVyRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIoCgV2YWx1ZRgCIAEoCzIZLk1hdGNoQ3JlZGl0VGllckNv" + "bmZpZ3VyZToCOAEaVAoVQ3JlZGl0QWN0aW9uRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxIqCgV2YWx1ZRgCIAEoCzIbLk1hdGNoQ3JlZGl0QWN0aW9uQ29uZmln" + "dXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(MatchInfoConfigure), MatchInfoConfigure.Parser, new string[2] { "Id", "Maps" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MatchParmsConfigure), MatchParmsConfigure.Parser, new string[4] { "Id", "BanTime", "ReadyTime", "AfkLimit" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MatchCreditTierConfigure), MatchCreditTierConfigure.Parser, new string[7] { "Id", "MinScore", "MaxScore", "TierName", "MatchCdSeconds", "RewardRatio", "TierDesc" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MatchCreditActionConfigure), MatchCreditActionConfigure.Parser, new string[4] { "Id", "CreditActionType", "ScoreChange", "ActionName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MatchConfigure), MatchConfigure.Parser, new string[8] { "Infos", "InfoDict", "Parmss", "ParmsDict", "CreditTiers", "CreditTierDict", "CreditActions", "CreditActionDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
