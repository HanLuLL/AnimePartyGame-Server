using party.protocol;

namespace Core.Net;

public class GachaRecordC2SRPC
{
	public virtual RPCAsyncResult GachaRecordC2SCall(GachaRecordC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5145, ProtolcalType.Ahead);
	}
}
