using party.protocol;

namespace Core.Net;

public class MailReadC2SRPC
{
	public virtual RPCAsyncResult MailReadC2SCall(MailReadC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5139, ProtolcalType.Ahead);
	}
}
