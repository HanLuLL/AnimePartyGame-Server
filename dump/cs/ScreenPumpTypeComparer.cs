using System.Collections.Generic;

public class ScreenPumpTypeComparer : IEqualityComparer<ScreenPumpType>
{
	public bool Equals(ScreenPumpType lhs, ScreenPumpType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ScreenPumpType obj)
	{
		return (int)obj;
	}
}
