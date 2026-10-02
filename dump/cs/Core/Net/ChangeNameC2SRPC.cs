using party.protocol;

namespace Core.Net;

public class ChangeNameC2SRPC
{
	public virtual RPCAsyncResult ChangeNameC2SCall(ChangeNameC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5245, ProtolcalType.Ahead);
	}
}
