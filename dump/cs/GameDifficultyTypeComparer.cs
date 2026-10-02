using System.Collections.Generic;

public class GameDifficultyTypeComparer : IEqualityComparer<GameDifficultyType>
{
	public bool Equals(GameDifficultyType lhs, GameDifficultyType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GameDifficultyType obj)
	{
		return (int)obj;
	}
}
