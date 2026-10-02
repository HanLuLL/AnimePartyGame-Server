using party.protocol;

namespace Core.Net;

public class SearchRoomC2SRPC
{
	public virtual RPCAsyncResult SearchRoomC2SCall(SearchRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5089, ProtolcalType.Ahead);
	}
}
