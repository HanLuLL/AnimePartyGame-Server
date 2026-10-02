using party.protocol;

namespace Core.Net;

public class SelectEventC2SRPC
{
	public virtual RPCAsyncResult SelectEventC2SCall(SelectEventC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5317, ProtolcalType.Ahead);
	}
}
