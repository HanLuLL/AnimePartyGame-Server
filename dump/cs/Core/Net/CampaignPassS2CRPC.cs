using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CampaignPassS2CRPC
{
	public delegate UniTask OnCampaignPassS2CServerDelegate(CampaignPassS2C model, int errId, bool isDispatch);

	public OnCampaignPassS2CServerDelegate OnCampaignPassS2CServerCallBackAsync;

	internal virtual async UniTask PushCampaignPassS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCampaignPassS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CampaignPassS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CampaignPassS2C model = param.ReadObject<CampaignPassS2C>();
		await OnCampaignPassS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
