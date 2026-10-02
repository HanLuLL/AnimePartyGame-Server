using party.protocol;

namespace Core.Net;

public class ReturnSurveyFinishC2SRPC
{
	public virtual RPCAsyncResult ReturnSurveyFinishC2SCall(ReturnSurveyFinishC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5385, ProtolcalType.Ahead);
	}
}
