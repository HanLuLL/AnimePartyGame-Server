using System.Collections.Generic;

public class CharacterTypeComparer : IEqualityComparer<CharacterType>
{
	public bool Equals(CharacterType lhs, CharacterType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(CharacterType obj)
	{
		return (int)obj;
	}
}
