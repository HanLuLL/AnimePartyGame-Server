using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TriggerDivinationS2CRPC
{
	public delegate UniTask OnTriggerDivinationS2CServerDelegate(TriggerDivinationS2C model, int errId, bool isDispatch);

	public OnTriggerDivinationS2CServerDelegate OnTriggerDivinationS2CServerCallBackAsync;

	internal virtual async UniTask PushTriggerDivinationS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTriggerDivinationS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TriggerDivinationS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TriggerDivinationS2C model = param.ReadObject<TriggerDivinationS2C>();
		await OnTriggerDivinationS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
