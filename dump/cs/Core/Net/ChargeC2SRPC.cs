using party.protocol;

namespace Core.Net;

public class ChargeC2SRPC
{
	public virtual RPCAsyncResult ChargeC2SCall(ChargeC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5135, ProtolcalType.Ahead);
	}
}
