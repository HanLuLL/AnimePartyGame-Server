using party.protocol;

namespace Core.Net;

public class ChinaCreateOrderC2SRPC
{
	public virtual RPCAsyncResult ChinaCreateOrderC2SCall(ChinaCreateOrderC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5339, ProtolcalType.Ahead);
	}
}
