using party.protocol;

namespace Core.Net;

public class SingleCampaignC2SRPC
{
	public virtual RPCAsyncResult SingleCampaignC2SCall(SingleCampaignC2S req)
	{
		return RPCMsgManager.RPCCallStatic(req, 5229, ProtolcalType.Ahead);
	}
}
