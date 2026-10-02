namespace GameLogic;

public class BingoActivityGrid
{
	public ActivityBingoFlipPoolConfigureItem Config;

	public int Id { get; private set; }

	public bool IsFlip { get; private set; }

	public BingoActivityGrid(int id, ActivityBingoFlipPoolConfigureItem config)
	{
		Id = id;
		Config = config;
	}

	public void UpdateFlipStatus(bool isFlip)
	{
		IsFlip = isFlip;
	}
}
