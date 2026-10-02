using party.protocol;

namespace Core.Net;

public class SetFashionC2SRPC
{
	public virtual RPCAsyncResult SetFashionC2SCall(SetFashionC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5103, ProtolcalType.Ahead);
	}
}
