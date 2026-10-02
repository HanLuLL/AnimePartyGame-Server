using party.protocol;

namespace Core.Net;

public class BuyRelicC2SRPC
{
	public virtual RPCAsyncResult BuyRelicC2SCall(BuyRelicC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5249, ProtolcalType.Ahead);
	}
}
