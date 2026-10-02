using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class DelayProgressMapEventS2CRPC
{
	public delegate UniTask OnDelayProgressMapEventS2CServerDelegate(DelayProgressMapEventS2C model, int errId, bool isDispatch);

	public OnDelayProgressMapEventS2CServerDelegate OnDelayProgressMapEventS2CServerCallBackAsync;

	internal virtual async UniTask PushDelayProgressMapEventS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnDelayProgressMapEventS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议DelayProgressMapEventS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		DelayProgressMapEventS2C model = param.ReadObject<DelayProgressMapEventS2C>();
		await OnDelayProgressMapEventS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
