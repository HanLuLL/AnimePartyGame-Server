using party.protocol;

namespace Core.Net;

public class GetGuildInfoC2SRPC
{
	public virtual RPCAsyncResult GetGuildInfoC2SCall(GetGuildInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9013, ProtolcalType.Ahead);
	}
}
