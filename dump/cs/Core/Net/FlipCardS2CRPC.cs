using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FlipCardS2CRPC
{
	public delegate UniTask OnFlipCardS2CServerDelegate(FlipCardS2C model, int errId, bool isDispatch);

	public OnFlipCardS2CServerDelegate OnFlipCardS2CServerCallBackAsync;

	internal virtual async UniTask PushFlipCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFlipCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FlipCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FlipCardS2C model = param.ReadObject<FlipCardS2C>();
		await OnFlipCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
