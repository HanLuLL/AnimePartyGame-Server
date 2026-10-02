using party.protocol;

namespace Core.Net;

public class KickGuildMemberC2SRPC
{
	public virtual RPCAsyncResult KickGuildMemberC2SCall(KickGuildMemberC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9023, ProtolcalType.Ahead);
	}
}
