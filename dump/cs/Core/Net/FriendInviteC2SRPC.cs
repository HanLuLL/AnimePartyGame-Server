using party.protocol;

namespace Core.Net;

public class FriendInviteC2SRPC
{
	public virtual RPCAsyncResult FriendInviteC2SCall(FriendInviteC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5173, ProtolcalType.Ahead);
	}
}
