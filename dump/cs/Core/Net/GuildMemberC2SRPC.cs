using party.protocol;

namespace Core.Net;

public class GuildMemberC2SRPC
{
	public virtual RPCAsyncResult GuildMemberC2SCall(GuildMemberC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9039, ProtolcalType.Ahead);
	}
}
