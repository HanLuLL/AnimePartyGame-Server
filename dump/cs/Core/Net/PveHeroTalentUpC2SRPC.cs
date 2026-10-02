using party.protocol;

namespace Core.Net;

public class PveHeroTalentUpC2SRPC
{
	public virtual RPCAsyncResult PveHeroTalentUpC2SCall(PveHeroTalentUpC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5315, ProtolcalType.Ahead);
	}
}
