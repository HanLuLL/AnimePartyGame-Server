using party.protocol;

namespace Core.Net;

public class NotifyStoryC2SRPC
{
	public virtual RPCAsyncResult NotifyStoryC2SCall(NotifyStoryC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5313, ProtolcalType.Ahead);
	}
}
