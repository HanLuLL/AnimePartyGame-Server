using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SingleCampaignS2CRPC
{
	public delegate UniTask OnSingleCampaignS2CServerDelegate(SingleCampaignS2C model, int errId, bool isDispatch);

	public OnSingleCampaignS2CServerDelegate OnSingleCampaignS2CServerCallBackAsync;

	internal virtual async UniTask PushSingleCampaignS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSingleCampaignS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SingleCampaignS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SingleCampaignS2C model = param.ReadObject<SingleCampaignS2C>();
		await OnSingleCampaignS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
