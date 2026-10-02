using party.protocol;

namespace Core.Net;

public class GetActivityPassRewardC2SRPC
{
	public virtual RPCAsyncResult GetActivityPassRewardC2SCall(GetActivityPassRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5353, ProtolcalType.Ahead);
	}
}
