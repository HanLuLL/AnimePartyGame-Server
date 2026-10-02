using party.protocol;

namespace Core.Net;

public class SetShowPlayerC2SRPC
{
	public virtual RPCAsyncResult SetShowPlayerC2SCall(SetShowPlayerC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5151, ProtolcalType.Ahead);
	}
}
