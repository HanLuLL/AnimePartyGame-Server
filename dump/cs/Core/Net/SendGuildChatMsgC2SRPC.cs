using party.protocol;

namespace Core.Net;

public class SendGuildChatMsgC2SRPC
{
	public virtual RPCAsyncResult SendGuildChatMsgC2SCall(SendGuildChatMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9035, ProtolcalType.Ahead);
	}
}
