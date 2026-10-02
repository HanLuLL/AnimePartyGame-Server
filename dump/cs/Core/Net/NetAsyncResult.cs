using Tools;

namespace Core.Net;

public class NetAsyncResult : CustomAsyncResult
{
	public bool isSuccess { get; set; }

	public NetAsyncResult()
	{
		isSuccess = false;
	}
}
