namespace GameLogic;

public class GachaPoolProgress
{
	public int PoolId;

	public int Progress;

	public int FinishRewards = -1;

	public void UpdateData(int count, int gachaDataRewardCount)
	{
		Progress = count;
		UpdateRewards(gachaDataRewardCount);
	}

	public bool IsFinishRewardByProgress(int progress)
	{
		if (progress <= Progress)
		{
			return progress <= FinishRewards;
		}
		return false;
	}

	public void UpdateRewards(int modelRewardCount)
	{
		FinishRewards = modelRewardCount;
	}
}
