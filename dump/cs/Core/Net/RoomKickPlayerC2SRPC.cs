using party.protocol;

namespace Core.Net;

public class RoomKickPlayerC2SRPC
{
	public virtual RPCAsyncResult RoomKickPlayerC2SCall(RoomKickPlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5127, ProtolcalType.Ahead);
	}
}
