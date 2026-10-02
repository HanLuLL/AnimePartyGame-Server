using party.protocol;

namespace Core.Net;

public class AgeVerifyC2SRPC
{
	public virtual RPCAsyncResult AgeVerifyC2SCall(AgeVerifyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5243, ProtolcalType.Ahead);
	}
}
