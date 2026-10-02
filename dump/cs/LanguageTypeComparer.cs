using System.Collections.Generic;

public class LanguageTypeComparer : IEqualityComparer<LanguageType>
{
	public bool Equals(LanguageType lhs, LanguageType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(LanguageType obj)
	{
		return (int)obj;
	}
}
