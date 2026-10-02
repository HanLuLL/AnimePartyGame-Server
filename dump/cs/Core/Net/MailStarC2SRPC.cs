using party.protocol;

namespace Core.Net;

public class MailStarC2SRPC
{
	public virtual RPCAsyncResult MailStarC2SCall(MailStarC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5371, ProtolcalType.Ahead);
	}
}
