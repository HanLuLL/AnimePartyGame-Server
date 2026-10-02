using party.protocol;

namespace Core.Net;

public class CreateMatchTeamC2SRPC
{
	public virtual RPCAsyncResult CreateMatchTeamC2SCall(CreateMatchTeamC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5329, ProtolcalType.Ahead);
	}
}
