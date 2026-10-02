using party.protocol;

namespace Core.Net;

public class PraisePlayerC2SRPC
{
	public virtual RPCAsyncResult PraisePlayerC2SCall(PraisePlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5159, ProtolcalType.Ahead);
	}
}
