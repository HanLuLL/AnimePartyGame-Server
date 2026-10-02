using party.protocol;

namespace Core.Net;

public class BombThrowDiceC2SRPC
{
	public virtual RPCAsyncResult BombThrowDiceC2SCall(BombThrowDiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5059, ProtolcalType.Ahead);
	}
}
