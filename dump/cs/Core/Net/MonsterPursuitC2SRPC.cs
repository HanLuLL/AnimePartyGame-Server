using party.protocol;

namespace Core.Net;

public class MonsterPursuitC2SRPC
{
	public virtual RPCAsyncResult MonsterPursuitC2SCall(MonsterPursuitC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5213, ProtolcalType.Ahead);
	}
}
