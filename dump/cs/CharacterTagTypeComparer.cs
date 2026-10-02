using System.Collections.Generic;

public class CharacterTagTypeComparer : IEqualityComparer<CharacterTagType>
{
	public bool Equals(CharacterTagType lhs, CharacterTagType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(CharacterTagType obj)
	{
		return (int)obj;
	}
}
