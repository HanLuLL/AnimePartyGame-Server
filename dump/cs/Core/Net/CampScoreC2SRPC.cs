using party.protocol;

namespace Core.Net;

public class CampScoreC2SRPC
{
	public virtual RPCAsyncResult CampScoreC2SCall(CampScoreC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5319, ProtolcalType.Ahead);
	}
}
