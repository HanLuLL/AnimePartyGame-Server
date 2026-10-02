using party.protocol;

namespace Core.Net;

public class ThrowDiceC2SRPC
{
	public virtual RPCAsyncResult ThrowDiceC2SCall(ThrowDiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5021, ProtolcalType.Ahead);
	}
}
