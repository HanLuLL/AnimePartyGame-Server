using party.protocol;

namespace Core.Net;

public class SetFriendNoteC2SRPC
{
	public virtual RPCAsyncResult SetFriendNoteC2SCall(SetFriendNoteC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5269, ProtolcalType.Ahead);
	}
}
