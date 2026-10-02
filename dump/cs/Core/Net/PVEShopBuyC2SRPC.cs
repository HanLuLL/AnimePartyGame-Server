using party.protocol;

namespace Core.Net;

public class PVEShopBuyC2SRPC
{
	public virtual RPCAsyncResult PVEShopBuyC2SCall(PVEShopBuyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5215, ProtolcalType.Ahead);
	}
}
