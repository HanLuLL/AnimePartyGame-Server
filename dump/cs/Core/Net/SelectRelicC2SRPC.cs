using party.protocol;

namespace Core.Net;

public class SelectRelicC2SRPC
{
	public virtual RPCAsyncResult SelectRelicC2SCall(SelectRelicC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5211, ProtolcalType.Ahead);
	}
}
