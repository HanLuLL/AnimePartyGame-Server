using party.protocol;

namespace Core.Net;

public class ChangeRoomC2SRPC
{
	public virtual RPCAsyncResult ChangeRoomC2SCall(ChangeRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5023, ProtolcalType.Ahead);
	}
}
