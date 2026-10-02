using party.protocol;

namespace Core.Net;

public class UseTreasureC2SRPC
{
	public virtual RPCAsyncResult UseTreasureC2SCall(UseTreasureC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5101, ProtolcalType.Ahead);
	}
}
