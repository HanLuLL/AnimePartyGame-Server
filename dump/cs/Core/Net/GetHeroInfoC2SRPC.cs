using party.protocol;

namespace Core.Net;

public class GetHeroInfoC2SRPC
{
	public virtual RPCAsyncResult GetHeroInfoC2SCall(GetHeroInfoC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5327, ProtolcalType.Ahead);
	}
}
