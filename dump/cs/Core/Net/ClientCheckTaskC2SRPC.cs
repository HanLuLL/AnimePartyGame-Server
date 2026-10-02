using party.protocol;

namespace Core.Net;

public class ClientCheckTaskC2SRPC
{
	public virtual RPCAsyncResult ClientCheckTaskC2SCall(ClientCheckTaskC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5217, ProtolcalType.Ahead);
	}
}
