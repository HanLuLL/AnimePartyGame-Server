using System.Collections.Generic;

public class UIWindowTypeComparer : IEqualityComparer<UIWindowType>
{
	public bool Equals(UIWindowType lhs, UIWindowType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(UIWindowType obj)
	{
		return (int)obj;
	}
}
