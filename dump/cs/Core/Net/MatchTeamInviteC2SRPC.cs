using party.protocol;

namespace Core.Net;

public class MatchTeamInviteC2SRPC
{
	public virtual RPCAsyncResult MatchTeamInviteC2SCall(MatchTeamInviteC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5341, ProtolcalType.Ahead);
	}
}
