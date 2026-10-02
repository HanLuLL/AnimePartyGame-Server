using party.protocol;

namespace Core.Net;

public class LightGiftC2SRPC
{
	public virtual RPCAsyncResult LightGiftC2SCall(LightGiftC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5253, ProtolcalType.Ahead);
	}
}
