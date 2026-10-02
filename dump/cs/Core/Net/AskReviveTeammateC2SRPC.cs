using party.protocol;

namespace Core.Net;

public class AskReviveTeammateC2SRPC
{
	public virtual RPCAsyncResult AskReviveTeammateC2SCall(AskReviveTeammateC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5233, ProtolcalType.Ahead);
	}
}
