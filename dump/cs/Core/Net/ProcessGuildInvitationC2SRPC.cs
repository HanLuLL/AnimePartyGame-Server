using party.protocol;

namespace Core.Net;

public class ProcessGuildInvitationC2SRPC
{
	public virtual RPCAsyncResult ProcessGuildInvitationC2SCall(ProcessGuildInvitationC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9011, ProtolcalType.Ahead);
	}
}
