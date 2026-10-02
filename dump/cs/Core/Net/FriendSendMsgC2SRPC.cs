using party.protocol;

namespace Core.Net;

public class FriendSendMsgC2SRPC
{
	public virtual RPCAsyncResult FriendSendMsgC2SCall(FriendSendMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5203, ProtolcalType.Ahead);
	}
}
