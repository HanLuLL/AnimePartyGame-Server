using party.protocol;

namespace Core.Net;

public class AcquisitionC2SRPC
{
	public virtual RPCAsyncResult AcquisitionC2SCall(AcquisitionC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5255, ProtolcalType.Ahead);
	}
}
