using party.protocol;

namespace Core.Net;

public class GetShowPlayerC2SRPC
{
	public virtual RPCAsyncResult GetShowPlayerC2SCall(GetShowPlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5153, ProtolcalType.Ahead);
	}
}
