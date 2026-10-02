using party.protocol;

namespace Core.Net;

public class ImpeachGuildMasterC2SRPC
{
	public virtual RPCAsyncResult ImpeachGuildMasterC2SCall(ImpeachGuildMasterC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9025, ProtolcalType.Ahead);
	}
}
