using party.protocol;

namespace Core.Net;

public class StartMatchC2SRPC
{
	public virtual RPCAsyncResult StartMatchC2SCall(StartMatchC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5221, ProtolcalType.Ahead);
	}
}
