using party.protocol;

namespace Core.Net;

public class LaborActDiceC2SRPC
{
	public virtual RPCAsyncResult LaborActDiceC2SCall(LaborActDiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5363, ProtolcalType.Ahead);
	}
}
