using party.protocol;

namespace Core.Net;

public class RookieGachaRewardC2SRPC
{
	public virtual RPCAsyncResult RookieGachaRewardC2SCall(RookieGachaRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5359, ProtolcalType.Ahead);
	}
}
