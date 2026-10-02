using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NotifyStoryS2CRPC
{
	public delegate UniTask OnNotifyStoryS2CServerDelegate(NotifyStoryS2C model, int errId, bool isDispatch);

	public OnNotifyStoryS2CServerDelegate OnNotifyStoryS2CServerCallBackAsync;

	internal virtual async UniTask PushNotifyStoryS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNotifyStoryS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NotifyStoryS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NotifyStoryS2C model = param.ReadObject<NotifyStoryS2C>();
		await OnNotifyStoryS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
