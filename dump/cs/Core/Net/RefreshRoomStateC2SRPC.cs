using party.protocol;

namespace Core.Net;

public class RefreshRoomStateC2SRPC
{
	public virtual RPCAsyncResult RefreshRoomStateC2SCall(RefreshRoomStateC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5015, ProtolcalType.Ahead);
	}
}
