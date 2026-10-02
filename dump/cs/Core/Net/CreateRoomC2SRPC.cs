using party.protocol;

namespace Core.Net;

public class CreateRoomC2SRPC
{
	public virtual RPCAsyncResult CreateRoomC2SCall(CreateRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5005, ProtolcalType.Ahead);
	}
}
