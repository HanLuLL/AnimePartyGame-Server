using party.protocol;

namespace Core.Net;

public class GambleThrowDicC2SRPC
{
	public virtual RPCAsyncResult GambleThrowDicC2SCall(GambleThrowDicC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5083, ProtolcalType.Ahead);
	}
}
