using party.protocol;

namespace Core.Net;

public class FriendApplyListC2SRPC
{
	public virtual RPCAsyncResult FriendApplyListC2SCall(FriendApplyListC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5167, ProtolcalType.Ahead);
	}
}
