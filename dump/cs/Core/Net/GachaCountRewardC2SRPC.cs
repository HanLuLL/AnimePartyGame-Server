using party.protocol;

namespace Core.Net;

public class GachaCountRewardC2SRPC
{
	public virtual RPCAsyncResult GachaCountRewardC2SCall(GachaCountRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5261, ProtolcalType.Ahead);
	}
}
