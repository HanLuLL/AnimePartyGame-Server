using party.protocol;

namespace Core.Net;

public class RoleCardUpLvC2SRPC
{
	public virtual RPCAsyncResult RoleCardUpLvC2SCall(RoleCardUpLvC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5113, ProtolcalType.Ahead);
	}
}
