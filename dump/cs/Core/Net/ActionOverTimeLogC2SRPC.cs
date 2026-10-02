using party.protocol;

namespace Core.Net;

public class ActionOverTimeLogC2SRPC
{
	public virtual RPCAsyncResult ActionOverTimeLogC2SCall(ActionOverTimeLogC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5365, ProtolcalType.Ahead);
	}
}
