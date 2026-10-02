using party.protocol;

namespace Core.Net;

public class WatchExitRoomC2SRPC
{
	public virtual RPCAsyncResult WatchExitRoomC2SCall(WatchExitRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5195, ProtolcalType.Ahead);
	}
}
