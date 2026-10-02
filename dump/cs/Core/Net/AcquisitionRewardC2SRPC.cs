using party.protocol;

namespace Core.Net;

public class AcquisitionRewardC2SRPC
{
	public virtual RPCAsyncResult AcquisitionRewardC2SCall(AcquisitionRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5257, ProtolcalType.Ahead);
	}
}
