using party.protocol;

namespace Core.Net;

public class PlayerChatC2SRPC
{
	public virtual RPCAsyncResult PlayerChatC2SCall(PlayerChatC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5347, ProtolcalType.Ahead);
	}
}
