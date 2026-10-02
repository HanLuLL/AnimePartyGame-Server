using party.protocol;

namespace Core.Net;

public class VoteC2SRPC
{
	public virtual RPCAsyncResult VoteC2SCall(VoteC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5309, ProtolcalType.Ahead);
	}
}
