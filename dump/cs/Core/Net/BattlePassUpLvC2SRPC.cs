using party.protocol;

namespace Core.Net;

public class BattlePassUpLvC2SRPC
{
	public virtual RPCAsyncResult BattlePassUpLvC2SCall(BattlePassUpLvC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5201, ProtolcalType.Ahead);
	}
}
