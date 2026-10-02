using party.protocol;

namespace Core.Net;

public class ClientClickConfirmTaskC2SRPC
{
	public virtual RPCAsyncResult ClientClickConfirmTaskC2SCall(ClientClickConfirmTaskC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5247, ProtolcalType.Ahead);
	}
}
