using party.protocol;

namespace Core.Net;

public class TransferGuildMasterC2SRPC
{
	public virtual RPCAsyncResult TransferGuildMasterC2SCall(TransferGuildMasterC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9019, ProtolcalType.Ahead);
	}
}
