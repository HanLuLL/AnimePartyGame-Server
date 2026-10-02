using party.protocol;

namespace Core.Net;

public class FriendBlacksListC2SRPC
{
	public virtual RPCAsyncResult FriendBlacksListC2SCall(FriendBlacksListC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5181, ProtolcalType.Ahead);
	}
}
