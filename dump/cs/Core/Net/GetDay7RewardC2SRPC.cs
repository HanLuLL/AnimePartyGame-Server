using party.protocol;

namespace Core.Net;

public class GetDay7RewardC2SRPC
{
	public virtual RPCAsyncResult GetDay7RewardC2SCall(GetDay7RewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5157, ProtolcalType.Ahead);
	}
}
