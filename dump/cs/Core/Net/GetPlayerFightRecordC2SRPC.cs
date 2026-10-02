using party.protocol;

namespace Core.Net;

public class GetPlayerFightRecordC2SRPC
{
	public virtual RPCAsyncResult GetPlayerFightRecordC2SCall(GetPlayerFightRecordC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5155, ProtolcalType.Ahead);
	}
}
