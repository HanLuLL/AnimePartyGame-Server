using party.protocol;

namespace Core.Net;

public class FriendApplyOpC2SRPC
{
	public virtual RPCAsyncResult FriendApplyOpC2SCall(FriendApplyOpC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5169, ProtolcalType.Ahead);
	}
}
