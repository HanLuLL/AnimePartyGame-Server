using party.protocol;

namespace Core.Net;

public class StartGameC2SRPC
{
	public virtual RPCAsyncResult StartGameC2SCall(StartGameC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5019, ProtolcalType.Ahead);
	}
}
