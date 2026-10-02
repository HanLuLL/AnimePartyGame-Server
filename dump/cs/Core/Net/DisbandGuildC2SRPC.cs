using party.protocol;

namespace Core.Net;

public class DisbandGuildC2SRPC
{
	public virtual RPCAsyncResult DisbandGuildC2SCall(DisbandGuildC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9029, ProtolcalType.Ahead);
	}
}
