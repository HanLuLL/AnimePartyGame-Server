using party.protocol;

namespace Core.Net;

public class MatchTeamReadyC2SRPC
{
	public virtual RPCAsyncResult MatchTeamReadyC2SCall(MatchTeamReadyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5345, ProtolcalType.Ahead);
	}
}
