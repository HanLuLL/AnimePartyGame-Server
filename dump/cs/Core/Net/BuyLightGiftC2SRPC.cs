using party.protocol;

namespace Core.Net;

public class BuyLightGiftC2SRPC
{
	public virtual RPCAsyncResult BuyLightGiftC2SCall(BuyLightGiftC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5251, ProtolcalType.Ahead);
	}
}
