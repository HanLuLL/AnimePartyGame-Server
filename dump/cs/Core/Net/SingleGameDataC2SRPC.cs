using party.protocol;

namespace Core.Net;

public class SingleGameDataC2SRPC
{
	public virtual RPCAsyncResult SingleGameDataC2SCall(SingleGameDataC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5351, ProtolcalType.Ahead);
	}
}
