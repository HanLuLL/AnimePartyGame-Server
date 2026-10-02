using party.protocol;

namespace Core.Net;

public class RoleCardChoiceResC2SRPC
{
	public virtual RPCAsyncResult RoleCardChoiceResC2SCall(RoleCardChoiceResC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5117, ProtolcalType.Ahead);
	}
}
