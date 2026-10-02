using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

public interface IPVEMissionTargetConfigs
{
	int Id { get; }

	int MissionDescID { get; }

	MapField<int, RepeatedField<int>> TargetConfigs { get; }

	static string GetTitleByMissionTargetConfigs(IPVEMissionTargetConfigs targetConfigs, UIStringType uiStringType = UIStringType.PVEMission)
	{
		string local = targetConfigs.MissionDescID.GetLocal(uiStringType);
		int placeholderCount = local.GetPlaceholderCount();
		int num = 0;
		object[] array = new object[placeholderCount];
		foreach (int key in targetConfigs.TargetConfigs.Keys)
		{
			RepeatedField<int> repeatedField = targetConfigs.TargetConfigs[key];
			if (repeatedField == null || repeatedField.Count == 0)
			{
				continue;
			}
			switch ((PVEMissionTargetType)key)
			{
			case PVEMissionTargetType.KillMonster:
				if (num + repeatedField.Count - 1 <= placeholderCount)
				{
					for (int i = 1; i < repeatedField.Count; i++)
					{
						string characterName = CharacterHandle.GetCharacterName(repeatedField[i]);
						array[num++] = characterName;
					}
				}
				break;
			case PVEMissionTargetType.AccPvemission:
			case PVEMissionTargetType.AccRoleStar:
			case PVEMissionTargetType.HeroBuffCrimeCondition:
				if (num + repeatedField.Count - 1 <= placeholderCount)
				{
					array[num++] = repeatedField[0];
				}
				break;
			}
		}
		if (num != placeholderCount)
		{
			Debug.LogError($"{targetConfigs.Id}MissionTargetConfigs 参数数量错误");
			return "";
		}
		return string.Format(local, array);
	}

	static int GetTargetCountByMissionTargetConfigs(IPVEMissionTargetConfigs targetConfigs)
	{
		foreach (int key in targetConfigs.TargetConfigs.Keys)
		{
			RepeatedField<int> repeatedField = targetConfigs.TargetConfigs[key];
			if (repeatedField != null && repeatedField.Count != 0)
			{
				switch ((PVEMissionTargetType)key)
				{
				case PVEMissionTargetType.KillMonster:
					return repeatedField[0];
				case PVEMissionTargetType.AccPvemission:
				case PVEMissionTargetType.AccRoleStar:
					return repeatedField[0];
				}
			}
		}
		return 1;
	}
}
