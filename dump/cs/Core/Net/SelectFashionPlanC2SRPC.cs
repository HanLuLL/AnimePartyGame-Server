using party.protocol;

namespace Core.Net;

public class SelectFashionPlanC2SRPC
{
	public virtual RPCAsyncResult SelectFashionPlanC2SCall(SelectFashionPlanC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5105, ProtolcalType.Ahead);
	}
}
