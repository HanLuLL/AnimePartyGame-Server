using party.protocol;

namespace Core.Net;

public class GetSignInRewardC2SRPC
{
	public virtual RPCAsyncResult GetSignInRewardC2SCall(GetSignInRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5235, ProtolcalType.Ahead);
	}
}
