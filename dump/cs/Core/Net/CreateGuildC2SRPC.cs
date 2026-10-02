using party.protocol;

namespace Core.Net;

public class CreateGuildC2SRPC
{
	public virtual RPCAsyncResult CreateGuildC2SCall(CreateGuildC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9001, ProtolcalType.Ahead);
	}
}
