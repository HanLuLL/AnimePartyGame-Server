using party.protocol;

namespace Core.Net;

public class VendorBuyCardC2SRPC
{
	public virtual RPCAsyncResult VendorBuyCardC2SCall(VendorBuyCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5323, ProtolcalType.Ahead);
	}
}
