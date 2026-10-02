using party.protocol;

namespace Core.Net;

public class RoomAbdicationC2SRPC
{
	public virtual RPCAsyncResult RoomAbdicationC2SCall(RoomAbdicationC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5129, ProtolcalType.Ahead);
	}
}
