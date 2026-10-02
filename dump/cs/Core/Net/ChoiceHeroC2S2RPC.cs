using party.protocol;

namespace Core.Net;

public class ChoiceHeroC2S2RPC
{
	public virtual RPCAsyncResult ChoiceHeroC2S2Call(ChoiceHeroC2S2 req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5085, ProtolcalType.Ahead);
	}
}
