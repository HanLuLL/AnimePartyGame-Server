using party.protocol;

namespace Core.Net;

public class LiveGiftPackageC2SRPC
{
	public virtual RPCAsyncResult LiveGiftPackageC2SCall(LiveGiftPackageC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5361, ProtolcalType.Ahead);
	}
}
