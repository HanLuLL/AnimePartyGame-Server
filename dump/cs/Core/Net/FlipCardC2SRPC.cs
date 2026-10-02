using party.protocol;

namespace Core.Net;

public class FlipCardC2SRPC
{
	public virtual RPCAsyncResult FlipCardC2SCall(FlipCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5387, ProtolcalType.Ahead);
	}
}
