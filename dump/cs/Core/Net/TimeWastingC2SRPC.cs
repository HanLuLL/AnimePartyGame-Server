using party.protocol;

namespace Core.Net;

public class TimeWastingC2SRPC
{
	public virtual RPCAsyncResult TimeWastingC2SCall(TimeWastingC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5307, ProtolcalType.Ahead);
	}
}
