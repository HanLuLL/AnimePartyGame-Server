using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TriggerEventS2CRPC
{
	public delegate UniTask OnTriggerEventS2CServerDelegate(TriggerEventS2C model, int errId, bool isDispatch);

	public OnTriggerEventS2CServerDelegate OnTriggerEventS2CServerCallBackAsync;

	internal virtual async UniTask PushTriggerEventS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTriggerEventS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TriggerEventS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TriggerEventS2C model = param.ReadObject<TriggerEventS2C>();
		await OnTriggerEventS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
