using party.protocol;

namespace Core.Net;

public class VoteSelectC2SRPC
{
	public virtual RPCAsyncResult VoteSelectC2SCall(VoteSelectC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5311, ProtolcalType.Ahead);
	}
}
