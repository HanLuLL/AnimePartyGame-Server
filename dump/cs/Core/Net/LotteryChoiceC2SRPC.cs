using party.protocol;

namespace Core.Net;

public class LotteryChoiceC2SRPC
{
	public virtual RPCAsyncResult LotteryChoiceC2SCall(LotteryChoiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5041, ProtolcalType.Ahead);
	}
}
