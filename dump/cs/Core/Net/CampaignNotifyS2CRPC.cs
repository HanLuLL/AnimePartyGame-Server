using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CampaignNotifyS2CRPC
{
	public delegate UniTask OnCampaignNotifyS2CServerDelegate(CampaignNotifyS2C model, int errId, bool isDispatch);

	public OnCampaignNotifyS2CServerDelegate OnCampaignNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushCampaignNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCampaignNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CampaignNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CampaignNotifyS2C model = param.ReadObject<CampaignNotifyS2C>();
		await OnCampaignNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
