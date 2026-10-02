using party.protocol;

namespace Core.Net;

public class ActivityTaskRewardC2SRPC
{
	public virtual RPCAsyncResult ActivityTaskRewardC2SCall(ActivityTaskRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5147, ProtolcalType.Ahead);
	}
}
