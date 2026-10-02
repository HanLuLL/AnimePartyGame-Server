using party.protocol;

namespace Core.Net;

public class ApplyChangeSlotC2SRPC
{
	public virtual RPCAsyncResult ApplyChangeSlotC2SCall(ApplyChangeSlotC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5355, ProtolcalType.Ahead);
	}
}
