using party.protocol;

namespace Core.Net;

public class ExitGuildC2SRPC
{
	public virtual RPCAsyncResult ExitGuildC2SCall(ExitGuildC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9027, ProtolcalType.Ahead);
	}
}
