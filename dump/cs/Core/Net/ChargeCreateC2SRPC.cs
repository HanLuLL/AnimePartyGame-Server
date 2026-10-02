using party.protocol;

namespace Core.Net;

public class ChargeCreateC2SRPC
{
	public virtual RPCAsyncResult ChargeCreateC2SCall(ChargeCreateC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5133, ProtolcalType.Ahead);
	}
}
