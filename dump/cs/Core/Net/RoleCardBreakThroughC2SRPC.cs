using party.protocol;

namespace Core.Net;

public class RoleCardBreakThroughC2SRPC
{
	public virtual RPCAsyncResult RoleCardBreakThroughC2SCall(RoleCardBreakThroughC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5115, ProtolcalType.Ahead);
	}
}
