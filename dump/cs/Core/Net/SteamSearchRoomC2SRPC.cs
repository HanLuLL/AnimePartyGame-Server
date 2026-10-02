using party.protocol;

namespace Core.Net;

public class SteamSearchRoomC2SRPC
{
	public virtual RPCAsyncResult SteamSearchRoomC2SCall(SteamSearchRoomC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5109, ProtolcalType.Ahead);
	}
}
