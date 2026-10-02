using party.protocol;

namespace Core.Net;

public class ApplyToGuildC2SRPC
{
	public virtual RPCAsyncResult ApplyToGuildC2SCall(ApplyToGuildC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9005, ProtolcalType.Ahead);
	}
}
