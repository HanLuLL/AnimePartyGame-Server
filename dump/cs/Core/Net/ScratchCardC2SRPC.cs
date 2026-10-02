using party.protocol;

namespace Core.Net;

public class ScratchCardC2SRPC
{
	public virtual RPCAsyncResult ScratchCardC2SCall(ScratchCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5187, ProtolcalType.Ahead);
	}
}
