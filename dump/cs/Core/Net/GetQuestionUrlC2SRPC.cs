using party.protocol;

namespace Core.Net;

public class GetQuestionUrlC2SRPC
{
	public virtual RPCAsyncResult GetQuestionUrlC2SCall(GetQuestionUrlC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5395, ProtolcalType.Ahead);
	}
}
