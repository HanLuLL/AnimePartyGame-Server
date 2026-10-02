using party.protocol;

namespace Core.Net;

public class PlayerShopBuyC2SRPC
{
	public virtual RPCAsyncResult PlayerShopBuyC2SCall(PlayerShopBuyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5097, ProtolcalType.Ahead);
	}
}
