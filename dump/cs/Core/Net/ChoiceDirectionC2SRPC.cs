using party.protocol;

namespace Core.Net;

public class ChoiceDirectionC2SRPC
{
	public virtual RPCAsyncResult ChoiceDirectionC2SCall(ChoiceDirectionC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5061, ProtolcalType.Ahead);
	}
}
