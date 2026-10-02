using party.protocol;

namespace Core.Net;

public class AbroadCreateOrderC2SRPC
{
	public virtual RPCAsyncResult AbroadCreateOrderC2SCall(AbroadCreateOrderC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5239, ProtolcalType.Ahead);
	}
}
