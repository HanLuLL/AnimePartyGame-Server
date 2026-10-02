using party.protocol;

namespace Core.Net;

public class ChatMapMarkersC2SRPC
{
	public virtual RPCAsyncResult ChatMapMarkersC2SCall(ChatMapMarkersC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5237, ProtolcalType.Ahead);
	}
}
