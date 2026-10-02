using party.protocol;

namespace Core.Net;

public class NextScratchCardPoolC2SRPC
{
	public virtual RPCAsyncResult NextScratchCardPoolC2SCall(NextScratchCardPoolC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5189, ProtolcalType.Ahead);
	}
}
