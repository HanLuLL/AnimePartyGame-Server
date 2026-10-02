using party.protocol;

namespace Core.Net;

public class QuickJoinRoomC2SRPC
{
	public virtual RPCAsyncResult QuickJoinRoomC2SCall(QuickJoinRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5125, ProtolcalType.Ahead);
	}
}
