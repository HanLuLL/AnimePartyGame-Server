using party.protocol;

namespace Core.Net;

public class GetReturnInfoC2SRPC
{
	public virtual RPCAsyncResult GetReturnInfoC2SCall(GetReturnInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5379, ProtolcalType.Ahead);
	}
}
