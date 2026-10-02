namespace GameLogic;

public class ActivityVoteData
{
	public int RefreshTime;

	public int CampId;

	public int CampScore;

	public void RefreshData(int refreshTime, int campId, int campScorePercent)
	{
		RefreshTime = refreshTime;
		CampId = campId;
		CampScore = campScorePercent;
	}
}
