using party.protocol;

namespace Core.Net;

public class UseTreasureAutoTransformC2SRPC
{
	public virtual RPCAsyncResult UseTreasureAutoTransformC2SCall(UseTreasureAutoTransformC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5123, ProtolcalType.Ahead);
	}
}
