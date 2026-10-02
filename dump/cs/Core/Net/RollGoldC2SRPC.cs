using party.protocol;

namespace Core.Net;

public class RollGoldC2SRPC
{
	public virtual RPCAsyncResult RollGoldC2SCall(RollGoldC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5049, ProtolcalType.Ahead);
	}
}
