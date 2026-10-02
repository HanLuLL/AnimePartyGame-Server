using party.protocol;

namespace Core.Net;

public class AccuseC2SRPC
{
	public virtual RPCAsyncResult AccuseC2SCall(AccuseC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5227, ProtolcalType.Ahead);
	}
}
