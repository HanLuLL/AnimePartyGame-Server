using System.Collections.Generic;

public class TaskRefreshTypeComparer : IEqualityComparer<TaskRefreshType>
{
	public bool Equals(TaskRefreshType lhs, TaskRefreshType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(TaskRefreshType obj)
	{
		return (int)obj;
	}
}
