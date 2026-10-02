using System.Collections.Generic;

public class UITypeComparer : IEqualityComparer<UIType>
{
	public bool Equals(UIType lhs, UIType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(UIType obj)
	{
		return (int)obj;
	}
}
