using party.protocol;

namespace Core.Net;

public class GetPlayerSimpleC2SRPC
{
	public virtual RPCAsyncResult GetPlayerSimpleC2SCall(GetPlayerSimpleC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5263, ProtolcalType.Ahead);
	}
}
