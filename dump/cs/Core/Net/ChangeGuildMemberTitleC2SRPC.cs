using party.protocol;

namespace Core.Net;

public class ChangeGuildMemberTitleC2SRPC
{
	public virtual RPCAsyncResult ChangeGuildMemberTitleC2SCall(ChangeGuildMemberTitleC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9021, ProtolcalType.Ahead);
	}
}
