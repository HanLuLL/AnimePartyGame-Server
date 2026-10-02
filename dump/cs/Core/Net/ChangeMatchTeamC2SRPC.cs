using party.protocol;

namespace Core.Net;

public class ChangeMatchTeamC2SRPC
{
	public virtual RPCAsyncResult ChangeMatchTeamC2SCall(ChangeMatchTeamC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5331, ProtolcalType.Ahead);
	}
}
