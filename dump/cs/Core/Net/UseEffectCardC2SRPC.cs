using party.protocol;

namespace Core.Net;

public class UseEffectCardC2SRPC
{
	public virtual RPCAsyncResult UseEffectCardC2SCall(UseEffectCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5055, ProtolcalType.Ahead);
	}
}
