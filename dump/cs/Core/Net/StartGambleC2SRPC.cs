using party.protocol;

namespace Core.Net;

public class StartGambleC2SRPC
{
	public virtual RPCAsyncResult StartGambleC2SCall(StartGambleC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5081, ProtolcalType.Ahead);
	}
}
