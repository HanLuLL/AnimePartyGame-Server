using party.protocol;

namespace Core.Net;

public class RefreshMatchTeamInfoC2SRPC
{
	public virtual RPCAsyncResult RefreshMatchTeamInfoC2SCall(RefreshMatchTeamInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5337, ProtolcalType.Ahead);
	}
}
