using party.protocol;

namespace Core.Net;

public class FlipCardProgressRewardC2SRPC
{
	public virtual RPCAsyncResult FlipCardProgressRewardC2SCall(FlipCardProgressRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5389, ProtolcalType.Ahead);
	}
}
