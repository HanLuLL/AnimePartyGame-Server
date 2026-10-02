using System;

namespace Core.Net;

public static class NetHelper
{
	public static int GetEnvironmentTickCount()
	{
		return Environment.TickCount & 0x7FFFFFFF;
	}
}
