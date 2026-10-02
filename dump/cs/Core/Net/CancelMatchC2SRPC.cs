using party.protocol;

namespace Core.Net;

public class CancelMatchC2SRPC
{
	public virtual RPCAsyncResult CancelMatchC2SCall(CancelMatchC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5223, ProtolcalType.Ahead);
	}
}
