using party.protocol;

namespace Core.Net;

public class ClientDataUploadC2SRPC
{
	public virtual RPCAsyncResult ClientDataUploadC2SCall(ClientDataUploadC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5161, ProtolcalType.Ahead);
	}
}
