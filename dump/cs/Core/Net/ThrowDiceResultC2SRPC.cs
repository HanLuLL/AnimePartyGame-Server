using party.protocol;

namespace Core.Net;

public class ThrowDiceResultC2SRPC
{
	public virtual RPCAsyncResult ThrowDiceResultC2SCall(ThrowDiceResultC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5067, ProtolcalType.Ahead);
	}
}
