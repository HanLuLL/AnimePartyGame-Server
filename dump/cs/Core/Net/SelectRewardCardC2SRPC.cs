using party.protocol;

namespace Core.Net;

public class SelectRewardCardC2SRPC
{
	public virtual RPCAsyncResult SelectRewardCardC2SCall(SelectRewardCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5377, ProtolcalType.Ahead);
	}
}
