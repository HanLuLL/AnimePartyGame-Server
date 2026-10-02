using party.protocol;

namespace Core.Net;

public class ShopBuyC2SRPC
{
	public virtual RPCAsyncResult ShopBuyC2SCall(ShopBuyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5029, ProtolcalType.Ahead);
	}
}
