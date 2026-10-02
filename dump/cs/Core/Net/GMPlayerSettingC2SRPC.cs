using party.protocol;

namespace Core.Net;

public class GMPlayerSettingC2SRPC
{
	public virtual RPCAsyncResult GMPlayerSettingC2SCall(GMPlayerSettingC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5267, ProtolcalType.Ahead);
	}
}
