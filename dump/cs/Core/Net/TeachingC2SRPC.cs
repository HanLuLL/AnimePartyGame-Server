using party.protocol;

namespace Core.Net;

public class TeachingC2SRPC
{
	public virtual RPCAsyncResult TeachingC2SCall(TeachingC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5121, ProtolcalType.Ahead);
	}
}
