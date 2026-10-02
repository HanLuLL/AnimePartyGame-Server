using Google.Protobuf.Collections;
using UI;
using party.model;

namespace GameLogic;

public class CampaignTaskData
{
	public VictoryType VictoryType;

	public int Star;

	public MapField<int, int> Kill_Record;

	private readonly CampaignLevelConfigure CampaignLevel;

	public CampaignTaskData(VictoryCondition victoryCondition, CampaignLevelConfigure campaignLevel)
	{
		CampaignLevel = campaignLevel;
		UpdateCampaignTask(victoryCondition);
	}

	public void UpdateCampaignTask(VictoryCondition victoryCondition)
	{
		VictoryType = (VictoryType)victoryCondition.VictoryType;
		Star = victoryCondition.Star;
		Kill_Record = victoryCondition.KillRecord;
	}

	public string GetTaskDesc()
	{
		if (CampaignLevel == null)
		{
			return "";
		}
		if (VictoryType == VictoryType.Star)
		{
			return CampaignLevel.VictoryDescription.GetLocal(UIStringType.Campaign) + $"[color=#66FF00]{Star}[/color]/{CampaignLevel.VictoryParams[0]}";
		}
		if (VictoryType == VictoryType.KillMonster)
		{
			Kill_Record.TryGetValue(CampaignLevel.VictoryParams[1], out var value);
			string characterName = CharacterHandle.GetCharacterName(CampaignLevel.VictoryParams[1]);
			return string.Format(CampaignLevel.VictoryDescription.GetLocal(UIStringType.Campaign), characterName) + $"[color=#66FF00]{value}[/color]/{CampaignLevel.VictoryParams[0]}";
		}
		return "";
	}
}
