using party.protocol;

namespace Core.Net;

public class RoomShortChatC2SRPC
{
	public virtual RPCAsyncResult RoomShortChatC2SCall(RoomShortChatC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5149, ProtolcalType.Ahead);
	}
}
