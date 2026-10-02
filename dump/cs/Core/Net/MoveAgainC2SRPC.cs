using party.protocol;

namespace Core.Net;

public class MoveAgainC2SRPC
{
	public virtual RPCAsyncResult MoveAgainC2SCall(MoveAgainC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5043, ProtolcalType.Ahead);
	}
}
