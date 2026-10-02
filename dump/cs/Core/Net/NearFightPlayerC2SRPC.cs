using party.protocol;

namespace Core.Net;

public class NearFightPlayerC2SRPC
{
	public virtual RPCAsyncResult NearFightPlayerC2SCall(NearFightPlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5183, ProtolcalType.Ahead);
	}
}
