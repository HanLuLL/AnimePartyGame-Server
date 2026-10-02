using party.protocol;

namespace Core.Net;

public class SyncSingleGameDataC2SRPC
{
	public virtual RPCAsyncResult SyncSingleGameDataC2SCall(SyncSingleGameDataC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5349, ProtolcalType.Ahead);
	}
}
