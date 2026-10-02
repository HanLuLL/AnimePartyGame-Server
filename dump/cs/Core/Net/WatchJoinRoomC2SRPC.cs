using party.protocol;

namespace Core.Net;

public class WatchJoinRoomC2SRPC
{
	public virtual RPCAsyncResult WatchJoinRoomC2SCall(WatchJoinRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5191, ProtolcalType.Ahead);
	}
}
