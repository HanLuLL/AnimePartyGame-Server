using party.protocol;

namespace Core.Net;

public class WatchRefreshRoomStateC2SRPC
{
	public virtual RPCAsyncResult WatchRefreshRoomStateC2SCall(WatchRefreshRoomStateC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5193, ProtolcalType.Ahead);
	}
}
