using party.protocol;

namespace Core.Net;

public class GuildMissionRewardC2SRPC
{
	public virtual RPCAsyncResult GuildMissionRewardC2SCall(GuildMissionRewardC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 9031, ProtolcalType.Ahead);
	}
}
