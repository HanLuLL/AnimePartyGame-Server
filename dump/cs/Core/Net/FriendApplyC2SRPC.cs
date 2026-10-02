using party.protocol;

namespace Core.Net;

public class FriendApplyC2SRPC
{
	public virtual RPCAsyncResult FriendApplyC2SCall(FriendApplyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5165, ProtolcalType.Ahead);
	}
}
