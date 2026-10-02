using party.protocol;

namespace Core.Net;

public class StopOrContinueC2SRPC
{
	public virtual RPCAsyncResult StopOrContinueC2SCall(StopOrContinueC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5077, ProtolcalType.Ahead);
	}
}
