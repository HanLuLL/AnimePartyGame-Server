using party.protocol;

namespace Core.Net;

public class SendChatC2SRPC
{
	public virtual RPCAsyncResult SendChatC2SCall(SendChatC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5095, ProtolcalType.Ahead);
	}
}
