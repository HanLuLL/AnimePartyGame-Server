using party.protocol;

namespace Core.Net;

public class ReadChatMsgC2SRPC
{
	public virtual RPCAsyncResult ReadChatMsgC2SCall(ReadChatMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5207, ProtolcalType.Ahead);
	}
}
