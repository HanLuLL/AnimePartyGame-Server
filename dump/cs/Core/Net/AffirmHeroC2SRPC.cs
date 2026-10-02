using party.protocol;

namespace Core.Net;

public class AffirmHeroC2SRPC
{
	public virtual RPCAsyncResult AffirmHeroC2SCall(AffirmHeroC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5087, ProtolcalType.Ahead);
	}
}
