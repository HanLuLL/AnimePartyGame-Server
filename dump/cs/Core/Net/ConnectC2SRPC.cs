using party.protocol;

namespace Core.Net;

public class ConnectC2SRPC
{
	public virtual RPCAsyncResult ConnectC2SCall(ConnectC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5001, ProtolcalType.Ahead);
	}
}
