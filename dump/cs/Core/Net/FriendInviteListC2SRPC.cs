using party.protocol;

namespace Core.Net;

public class FriendInviteListC2SRPC
{
	public virtual RPCAsyncResult FriendInviteListC2SCall(FriendInviteListC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5175, ProtolcalType.Ahead);
	}
}
