using party.protocol;

namespace Core.Net;

public class BattleUseCardC2SRPC
{
	public virtual RPCAsyncResult BattleUseCardC2SCall(BattleUseCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5035, ProtolcalType.Ahead);
	}
}
