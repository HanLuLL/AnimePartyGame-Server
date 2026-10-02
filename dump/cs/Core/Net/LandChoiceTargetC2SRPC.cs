using party.protocol;

namespace Core.Net;

public class LandChoiceTargetC2SRPC
{
	public virtual RPCAsyncResult LandChoiceTargetC2SCall(LandChoiceTargetC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5063, ProtolcalType.Ahead);
	}
}
