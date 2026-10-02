using party.protocol;

namespace Core.Net;

public class TaskRewardC2SRPC
{
	public virtual RPCAsyncResult TaskRewardC2SCall(TaskRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5119, ProtolcalType.Ahead);
	}
}
