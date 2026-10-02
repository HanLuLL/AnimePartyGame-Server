using party.protocol;

namespace Core.Net;

public class SelectMechanismC2SRPC
{
	public virtual RPCAsyncResult SelectMechanismC2SCall(SelectMechanismC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5259, ProtolcalType.Ahead);
	}
}
