using System.Collections.Generic;

public class UIPanelTypeComparer : IEqualityComparer<UIPanelType>
{
	public bool Equals(UIPanelType lhs, UIPanelType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(UIPanelType obj)
	{
		return (int)obj;
	}
}
