using party.protocol;

namespace Core.Net;

public class BattlePassTaskRewardC2SRPC
{
	public virtual RPCAsyncResult BattlePassTaskRewardC2SCall(BattlePassTaskRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5199, ProtolcalType.Ahead);
	}
}
