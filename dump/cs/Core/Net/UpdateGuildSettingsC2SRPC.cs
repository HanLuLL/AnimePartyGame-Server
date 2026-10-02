using party.protocol;

namespace Core.Net;

public class UpdateGuildSettingsC2SRPC
{
	public virtual RPCAsyncResult UpdateGuildSettingsC2SCall(UpdateGuildSettingsC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9015, ProtolcalType.Ahead);
	}
}
