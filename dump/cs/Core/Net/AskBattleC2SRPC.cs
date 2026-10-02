using party.protocol;

namespace Core.Net;

public class AskBattleC2SRPC
{
	public virtual RPCAsyncResult AskBattleC2SCall(AskBattleC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5047, ProtolcalType.Ahead);
	}
}
