using party.protocol;

namespace Core.Net;

public class PveHeroUpLvC2SRPC
{
	public virtual RPCAsyncResult PveHeroUpLvC2SCall(PveHeroUpLvC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5219, ProtolcalType.Ahead);
	}
}
