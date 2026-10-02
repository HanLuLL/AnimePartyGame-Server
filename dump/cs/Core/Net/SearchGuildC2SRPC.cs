using party.protocol;

namespace Core.Net;

public class SearchGuildC2SRPC
{
	public virtual RPCAsyncResult SearchGuildC2SCall(SearchGuildC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9003, ProtolcalType.Ahead);
	}
}
