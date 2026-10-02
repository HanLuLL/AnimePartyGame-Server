using party.protocol;

namespace Core.Net;

public class GachaC2SRPC
{
	public virtual RPCAsyncResult GachaC2SCall(GachaC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5107, ProtolcalType.Ahead);
	}
}
