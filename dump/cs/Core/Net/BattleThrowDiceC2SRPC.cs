using party.protocol;

namespace Core.Net;

public class BattleThrowDiceC2SRPC
{
	public virtual RPCAsyncResult BattleThrowDiceC2SCall(BattleThrowDiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5037, ProtolcalType.Ahead);
	}
}
