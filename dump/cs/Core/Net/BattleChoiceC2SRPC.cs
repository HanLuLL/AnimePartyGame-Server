using party.protocol;

namespace Core.Net;

public class BattleChoiceC2SRPC
{
	public virtual RPCAsyncResult BattleChoiceC2SCall(BattleChoiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5039, ProtolcalType.Ahead);
	}
}
