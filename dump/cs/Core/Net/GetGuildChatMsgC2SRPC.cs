using party.protocol;

namespace Core.Net;

public class GetGuildChatMsgC2SRPC
{
	public virtual RPCAsyncResult GetGuildChatMsgC2SCall(GetGuildChatMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9037, ProtolcalType.Ahead);
	}
}
