using System.Collections.Generic;
using SinglePlayer.GamePlay;

namespace SinglePlayer;

public class SinglePlayerLevelSelectInfo
{
	public int ChapterId;

	public int LevelId;

	public HashSet<int> SelectedCardPacks = new HashSet<int>();

	public bool IsLoadOnGameStart;

	public void SetRestartGameInfo(bool isNextLevel)
	{
		GameData model = Game.GetModel<GameData>();
		ChapterId = model.ChapterId;
		LevelId = model.LevelId;
		isNextLevel &= !model.IsFinalLevel();
		if (isNextLevel)
		{
			LevelId++;
		}
		SelectedCardPacks.Clear();
		foreach (int selectedCardPack in model.SelectedCardPacks)
		{
			SelectedCardPacks.Add(selectedCardPack);
		}
		IsLoadOnGameStart = true;
	}
}
