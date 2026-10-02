using System.Collections.Generic;

public class GameSpeedTypeComparer : IEqualityComparer<GameSpeedType>
{
	public bool Equals(GameSpeedType lhs, GameSpeedType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GameSpeedType obj)
	{
		return (int)obj;
	}
}
