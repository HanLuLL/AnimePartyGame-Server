using party.protocol;

namespace Core.Net;

public class SearchPlayerC2SRPC
{
	public virtual RPCAsyncResult SearchPlayerC2SCall(SearchPlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5185, ProtolcalType.Ahead);
	}
}
