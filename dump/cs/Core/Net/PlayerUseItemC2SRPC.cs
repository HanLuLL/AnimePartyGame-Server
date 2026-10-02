using party.protocol;

namespace Core.Net;

public class PlayerUseItemC2SRPC
{
	public virtual RPCAsyncResult PlayerUseItemC2SCall(PlayerUseItemC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5099, ProtolcalType.Ahead);
	}
}
