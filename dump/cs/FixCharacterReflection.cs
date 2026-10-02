using System;
using Google.Protobuf.Reflection;

public static class FixCharacterReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixCharacterReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChJGaXhDaGFyYWN0ZXIucHJvdG8iJwoZRml4Q2hhcmFjdGVySW5mb0NvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDyJICiBGaXhDaGFyYWN0ZXJTaGllbGRTa2lsbENv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIYChBwdmVQYXNzaXZlU2tpbGxzGAIgAygP" + "IqEDChVGaXhDaGFyYWN0ZXJDb25maWd1cmUSKQoFSW5mb3MYASADKAsyGi5G" + "aXhDaGFyYWN0ZXJJbmZvQ29uZmlndXJlEjYKCEluZm9EaWN0GAIgAygLMiQu" + "Rml4Q2hhcmFjdGVyQ29uZmlndXJlLkluZm9EaWN0RW50cnkSNwoMU2hpZWxk" + "U2tpbGxzGAMgAygLMiEuRml4Q2hhcmFjdGVyU2hpZWxkU2tpbGxDb25maWd1" + "cmUSRAoPU2hpZWxkU2tpbGxEaWN0GAQgAygLMisuRml4Q2hhcmFjdGVyQ29u" + "ZmlndXJlLlNoaWVsZFNraWxsRGljdEVudHJ5GksKDUluZm9EaWN0RW50cnkS" + "CwoDa2V5GAEgASgPEikKBXZhbHVlGAIgASgLMhouRml4Q2hhcmFjdGVySW5m" + "b0NvbmZpZ3VyZToCOAEaWQoUU2hpZWxkU2tpbGxEaWN0RW50cnkSCwoDa2V5" + "GAEgASgPEjAKBXZhbHVlGAIgASgLMiEuRml4Q2hhcmFjdGVyU2hpZWxkU2tp" + "bGxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(FixCharacterInfoConfigure), FixCharacterInfoConfigure.Parser, new string[1] { "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixCharacterShieldSkillConfigure), FixCharacterShieldSkillConfigure.Parser, new string[2] { "Id", "PvePassiveSkills" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixCharacterConfigure), FixCharacterConfigure.Parser, new string[4] { "Infos", "InfoDict", "ShieldSkills", "ShieldSkillDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
