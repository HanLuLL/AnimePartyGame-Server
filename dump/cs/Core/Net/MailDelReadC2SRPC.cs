using party.protocol;

namespace Core.Net;

public class MailDelReadC2SRPC
{
	public virtual RPCAsyncResult MailDelReadC2SCall(MailDelReadC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5143, ProtolcalType.Ahead);
	}
}
