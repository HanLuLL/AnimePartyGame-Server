using party.protocol;

namespace Core.Net;

public class SendGuildInvitationC2SRPC
{
	public virtual RPCAsyncResult SendGuildInvitationC2SCall(SendGuildInvitationC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9009, ProtolcalType.Ahead);
	}
}
