using party.protocol;

namespace Core.Net;

public class MatchTeamChatC2SRPC
{
	public virtual RPCAsyncResult MatchTeamChatC2SCall(MatchTeamChatC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5343, ProtolcalType.Ahead);
	}
}
