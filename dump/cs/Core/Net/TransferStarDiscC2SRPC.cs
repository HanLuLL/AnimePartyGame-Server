using party.protocol;

namespace Core.Net;

public class TransferStarDiscC2SRPC
{
	public virtual RPCAsyncResult TransferStarDiscC2SCall(TransferStarDiscC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5325, ProtolcalType.Ahead);
	}
}
