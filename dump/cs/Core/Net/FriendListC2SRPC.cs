using party.protocol;

namespace Core.Net;

public class FriendListC2SRPC
{
	public virtual RPCAsyncResult FriendListC2SCall(FriendListC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5163, ProtolcalType.Ahead);
	}
}
