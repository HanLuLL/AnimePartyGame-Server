using party.protocol;

namespace Core.Net;

public class EventThrowDiceC2SRPC
{
	public virtual RPCAsyncResult EventThrowDiceC2SCall(EventThrowDiceC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5051, ProtolcalType.Ahead);
	}
}
