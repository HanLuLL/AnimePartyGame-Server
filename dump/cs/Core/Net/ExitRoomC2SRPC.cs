using party.protocol;

namespace Core.Net;

public class ExitRoomC2SRPC
{
	public virtual RPCAsyncResult ExitRoomC2SCall(ExitRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5011, ProtolcalType.Ahead);
	}
}
