using party.protocol;

namespace Core.Net;

public class GetGuildsInfoC2SRPC
{
	public virtual RPCAsyncResult GetGuildsInfoC2SCall(GetGuildsInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9041, ProtolcalType.Ahead);
	}
}
