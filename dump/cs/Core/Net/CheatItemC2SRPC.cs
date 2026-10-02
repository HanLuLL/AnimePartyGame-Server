using party.protocol;

namespace Core.Net;

public class CheatItemC2SRPC
{
	public virtual RPCAsyncResult CheatItemC2SCall(CheatItemC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5111, ProtolcalType.Ahead);
	}
}
