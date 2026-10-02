using party.protocol;

namespace Core.Net;

public class HeartbeatC2SRPC
{
	public virtual RPCAsyncResult HeartbeatC2SCall(HeartbeatC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5003, ProtolcalType.Ahead);
	}
}
