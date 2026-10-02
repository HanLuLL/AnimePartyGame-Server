using party.protocol;

namespace Core.Net;

public class GiftCdkC2SRPC
{
	public virtual RPCAsyncResult GiftCdkC2SCall(GiftCdkC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5137, ProtolcalType.Ahead);
	}
}
