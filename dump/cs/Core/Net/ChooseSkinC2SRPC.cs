using party.protocol;

namespace Core.Net;

public class ChooseSkinC2SRPC
{
	public virtual RPCAsyncResult ChooseSkinC2SCall(ChooseSkinC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5305, ProtolcalType.Ahead);
	}
}
