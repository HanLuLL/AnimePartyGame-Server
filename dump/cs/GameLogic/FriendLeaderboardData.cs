namespace GameLogic;

public struct FriendLeaderboardData
{
	public long playerId;

	public int Score;

	public string Nick;

	public int LV;

	public string HeadURL;

	public (string, bool) Label;

	public void ChangeScore(int score)
	{
		Score = score;
	}
}
