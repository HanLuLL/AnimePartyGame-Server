using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LightGiftS2CRPC
{
	public delegate UniTask OnLightGiftS2CServerDelegate(LightGiftS2C model, int errId, bool isDispatch);

	public OnLightGiftS2CServerDelegate OnLightGiftS2CServerCallBackAsync;

	internal virtual async UniTask PushLightGiftS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLightGiftS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LightGiftS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LightGiftS2C model = param.ReadObject<LightGiftS2C>();
		await OnLightGiftS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
