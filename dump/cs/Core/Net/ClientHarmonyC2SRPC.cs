using party.protocol;

namespace Core.Net;

public class ClientHarmonyC2SRPC
{
	public virtual RPCAsyncResult ClientHarmonyC2SCall(ClientHarmonyC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5369, ProtolcalType.Ahead);
	}
}
