using party.protocol;

namespace Core.Net;

public class DevChargeC2SRPC
{
	public virtual RPCAsyncResult DevChargeC2SCall(DevChargeC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5231, ProtolcalType.Ahead);
	}
}
