using party.protocol;

namespace Core.Net;

public class SetCardAltArtC2SRPC
{
	public virtual RPCAsyncResult SetCardAltArtC2SCall(SetCardAltArtC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5373, ProtolcalType.Ahead);
	}
}
