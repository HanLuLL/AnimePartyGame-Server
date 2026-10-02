using party.protocol;

namespace Core.Net;

public class SyncRoomC2SRPC
{
	public virtual RPCAsyncResult SyncRoomC2SCall(SyncRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5007, ProtolcalType.Ahead);
	}
}
