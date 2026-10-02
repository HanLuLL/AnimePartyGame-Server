using party.protocol;

namespace Core.Net;

public class QueryRoomC2SRPC
{
	public virtual RPCAsyncResult QueryRoomC2SCall(QueryRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5013, ProtolcalType.Ahead);
	}
}
