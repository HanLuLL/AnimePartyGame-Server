using party.protocol;

namespace Core.Net;

public class AbandonCardC2SRPC
{
	public virtual RPCAsyncResult AbandonCardC2SCall(AbandonCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5075, ProtolcalType.Ahead);
	}
}
