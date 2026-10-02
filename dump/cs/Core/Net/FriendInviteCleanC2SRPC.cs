using party.protocol;

namespace Core.Net;

public class FriendInviteCleanC2SRPC
{
	public virtual RPCAsyncResult FriendInviteCleanC2SCall(FriendInviteCleanC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5179, ProtolcalType.Ahead);
	}
}
