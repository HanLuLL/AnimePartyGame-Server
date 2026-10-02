using party.protocol;

namespace Core.Net;

public class TriggerDivinationC2SRPC
{
	public virtual RPCAsyncResult TriggerDivinationC2SCall(TriggerDivinationC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5069, ProtolcalType.Ahead);
	}
}
