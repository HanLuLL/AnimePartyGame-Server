using party.protocol;

namespace Core.Net;

public class ReturnGiftClaimC2SRPC
{
	public virtual RPCAsyncResult ReturnGiftClaimC2SCall(ReturnGiftClaimC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5381, ProtolcalType.Ahead);
	}
}
