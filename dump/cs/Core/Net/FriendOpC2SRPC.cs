using party.protocol;

namespace Core.Net;

public class FriendOpC2SRPC
{
	public virtual RPCAsyncResult FriendOpC2SCall(FriendOpC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5171, ProtolcalType.Ahead);
	}
}
