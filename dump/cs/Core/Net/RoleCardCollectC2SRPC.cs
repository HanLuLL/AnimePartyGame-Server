using party.protocol;

namespace Core.Net;

public class RoleCardCollectC2SRPC
{
	public virtual RPCAsyncResult RoleCardCollectC2SCall(RoleCardCollectC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5265, ProtolcalType.Ahead);
	}
}
