using party.protocol;

namespace Core.Net;

public class TriggerEventC2SRPC
{
	public virtual RPCAsyncResult TriggerEventC2SCall(TriggerEventC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5053, ProtolcalType.Ahead);
	}
}
