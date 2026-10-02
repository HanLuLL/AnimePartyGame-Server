using party.protocol;

namespace Core.Net;

public class JoinRoomC2SRPC
{
	public virtual RPCAsyncResult JoinRoomC2SCall(JoinRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5009, ProtolcalType.Ahead);
	}
}
