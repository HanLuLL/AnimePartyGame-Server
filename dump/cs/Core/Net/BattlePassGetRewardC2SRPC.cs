using party.protocol;

namespace Core.Net;

public class BattlePassGetRewardC2SRPC
{
	public virtual RPCAsyncResult BattlePassGetRewardC2SCall(BattlePassGetRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5197, ProtolcalType.Ahead);
	}
}
