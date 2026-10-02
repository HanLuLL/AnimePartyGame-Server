using party.protocol;

namespace Core.Net;

public class TriggerDestinyC2SRPC
{
	public virtual RPCAsyncResult TriggerDestinyC2SCall(TriggerDestinyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5071, ProtolcalType.Ahead);
	}
}
