using party.protocol;

namespace Core.Net;

public class TriggerHospitalC2SRPC
{
	public virtual RPCAsyncResult TriggerHospitalC2SCall(TriggerHospitalC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5093, ProtolcalType.Ahead);
	}
}
