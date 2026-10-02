using party.protocol;

namespace Core.Net;

public class GetChatMsgC2SRPC
{
	public virtual RPCAsyncResult GetChatMsgC2SCall(GetChatMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5205, ProtolcalType.Ahead);
	}
}
