using party.protocol;

namespace Core.Net;

public class ReturnSignInClaimC2SRPC
{
	public virtual RPCAsyncResult ReturnSignInClaimC2SCall(ReturnSignInClaimC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5383, ProtolcalType.Ahead);
	}
}
