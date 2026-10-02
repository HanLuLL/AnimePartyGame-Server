using party.protocol;

namespace Core.Net;

public class SetOnlineStatusC2SRPC
{
	public virtual RPCAsyncResult SetOnlineStatusC2SCall(SetOnlineStatusC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5271, ProtolcalType.Ahead);
	}
}
