using party.protocol;

namespace Core.Net;

public class RoomReadyC2SRPC
{
	public virtual RPCAsyncResult RoomReadyC2SCall(RoomReadyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5131, ProtolcalType.Ahead);
	}
}
