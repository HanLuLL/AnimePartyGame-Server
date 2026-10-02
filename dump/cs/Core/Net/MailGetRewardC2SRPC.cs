using party.protocol;

namespace Core.Net;

public class MailGetRewardC2SRPC
{
	public virtual RPCAsyncResult MailGetRewardC2SCall(MailGetRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5141, ProtolcalType.Ahead);
	}
}
