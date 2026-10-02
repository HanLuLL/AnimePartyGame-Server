using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TriggerDestinyS2CRPC
{
	public delegate UniTask OnTriggerDestinyS2CServerDelegate(TriggerDestinyS2C model, int errId, bool isDispatch);

	public OnTriggerDestinyS2CServerDelegate OnTriggerDestinyS2CServerCallBackAsync;

	internal virtual async UniTask PushTriggerDestinyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTriggerDestinyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TriggerDestinyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TriggerDestinyS2C model = param.ReadObject<TriggerDestinyS2C>();
		await OnTriggerDestinyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
