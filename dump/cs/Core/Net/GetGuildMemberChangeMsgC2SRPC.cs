using party.protocol;

namespace Core.Net;

public class GetGuildMemberChangeMsgC2SRPC
{
	public virtual RPCAsyncResult GetGuildMemberChangeMsgC2SCall(GetGuildMemberChangeMsgC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9033, ProtolcalType.Ahead);
	}
}
