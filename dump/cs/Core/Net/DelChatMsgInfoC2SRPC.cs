using party.protocol;

namespace Core.Net;

public class DelChatMsgInfoC2SRPC
{
	public virtual RPCAsyncResult DelChatMsgInfoC2SCall(DelChatMsgInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5209, ProtolcalType.Ahead);
	}
}
