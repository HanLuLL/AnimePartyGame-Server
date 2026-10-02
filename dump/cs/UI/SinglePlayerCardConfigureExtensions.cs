using System;
using SinglePlayer.GamePlay.Card;

namespace UI;

public static class SinglePlayerCardConfigureExtensions
{
	public static int GetQualityIndex(this SinglePlayerCardConfigure configure)
	{
		return (CardRarity)configure.Rarity switch
		{
			CardRarity.GREEN => 1, 
			CardRarity.BLUE => 2, 
			CardRarity.PURPLE => 3, 
			CardRarity.GOLDEN => 4, 
			CardRarity.RED => 5, 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	public static int GetQualityIndex(this SinglePlayerRelicConfigure configure)
	{
		return (CardRarity)configure.Rarity switch
		{
			CardRarity.GREEN => 1, 
			CardRarity.BLUE => 2, 
			CardRarity.PURPLE => 3, 
			CardRarity.GOLDEN => 4, 
			CardRarity.RED => 5, 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}
}
