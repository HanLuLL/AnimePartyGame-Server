using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class StopOrContinueS2CRPC
{
	public delegate UniTask OnStopOrContinueS2CServerDelegate(StopOrContinueS2C model, int errId, bool isDispatch);

	public OnStopOrContinueS2CServerDelegate OnStopOrContinueS2CServerCallBackAsync;

	internal virtual async UniTask PushStopOrContinueS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnStopOrContinueS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议StopOrContinueS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		StopOrContinueS2C model = param.ReadObject<StopOrContinueS2C>();
		await OnStopOrContinueS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
