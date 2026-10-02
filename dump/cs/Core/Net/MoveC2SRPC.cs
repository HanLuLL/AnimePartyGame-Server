using party.protocol;

namespace Core.Net;

public class MoveC2SRPC
{
	public virtual RPCAsyncResult MoveC2SCall(MoveC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5027, ProtolcalType.Ahead);
	}
}
