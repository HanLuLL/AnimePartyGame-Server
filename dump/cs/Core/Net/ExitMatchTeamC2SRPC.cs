using party.protocol;

namespace Core.Net;

public class ExitMatchTeamC2SRPC
{
	public virtual RPCAsyncResult ExitMatchTeamC2SCall(ExitMatchTeamC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5335, ProtolcalType.Ahead);
	}
}
