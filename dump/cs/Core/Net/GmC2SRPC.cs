using party.protocol;

namespace Core.Net;

public class GmC2SRPC
{
	public virtual RPCAsyncResult GmC2SCall(GmC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5091, ProtolcalType.Ahead);
	}
}
