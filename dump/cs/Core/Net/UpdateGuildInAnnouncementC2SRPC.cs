using party.protocol;

namespace Core.Net;

public class UpdateGuildInAnnouncementC2SRPC
{
	public virtual RPCAsyncResult UpdateGuildInAnnouncementC2SCall(UpdateGuildInAnnouncementC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9017, ProtolcalType.Ahead);
	}
}
