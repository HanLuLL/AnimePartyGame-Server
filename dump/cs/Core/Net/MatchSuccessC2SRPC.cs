using party.protocol;

namespace Core.Net;

public class MatchSuccessC2SRPC
{
	public virtual RPCAsyncResult MatchSuccessC2SCall(MatchSuccessC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5225, ProtolcalType.Ahead);
	}
}
