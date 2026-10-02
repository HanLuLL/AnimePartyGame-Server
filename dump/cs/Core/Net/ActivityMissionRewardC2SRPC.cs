using party.protocol;

namespace Core.Net;

public class ActivityMissionRewardC2SRPC
{
	public virtual RPCAsyncResult ActivityMissionRewardC2SCall(ActivityMissionRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5321, ProtolcalType.Ahead);
	}
}
