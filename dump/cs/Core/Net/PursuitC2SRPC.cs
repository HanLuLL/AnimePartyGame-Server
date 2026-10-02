using party.protocol;

namespace Core.Net;

public class PursuitC2SRPC
{
	public virtual RPCAsyncResult PursuitC2SCall(PursuitC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5033, ProtolcalType.Ahead);
	}
}
