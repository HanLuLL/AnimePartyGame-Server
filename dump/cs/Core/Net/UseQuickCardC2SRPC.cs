using party.protocol;

namespace Core.Net;

public class UseQuickCardC2SRPC
{
	public virtual RPCAsyncResult UseQuickCardC2SCall(UseQuickCardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5073, ProtolcalType.Ahead);
	}
}
