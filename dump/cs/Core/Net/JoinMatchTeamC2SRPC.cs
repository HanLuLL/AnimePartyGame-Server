using party.protocol;

namespace Core.Net;

public class JoinMatchTeamC2SRPC
{
	public virtual RPCAsyncResult JoinMatchTeamC2SCall(JoinMatchTeamC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5333, ProtolcalType.Ahead);
	}
}
