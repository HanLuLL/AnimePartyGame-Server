using party.protocol;

namespace Core.Net;

public class OpsChangeSlotC2SRPC
{
	public virtual RPCAsyncResult OpsChangeSlotC2SCall(OpsChangeSlotC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5357, ProtolcalType.Ahead);
	}
}
