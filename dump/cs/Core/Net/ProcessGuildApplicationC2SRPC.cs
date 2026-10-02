using party.protocol;

namespace Core.Net;

public class ProcessGuildApplicationC2SRPC
{
	public virtual RPCAsyncResult ProcessGuildApplicationC2SCall(ProcessGuildApplicationC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9007, ProtolcalType.Ahead);
	}
}
