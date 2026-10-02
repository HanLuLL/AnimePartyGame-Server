using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AbroadCreateOrderS2CRPC
{
	public delegate UniTask OnAbroadCreateOrderS2CServerDelegate(AbroadCreateOrderS2C model, int errId, bool isDispatch);

	public OnAbroadCreateOrderS2CServerDelegate OnAbroadCreateOrderS2CServerCallBackAsync;

	internal virtual async UniTask PushAbroadCreateOrderS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAbroadCreateOrderS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AbroadCreateOrderS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AbroadCreateOrderS2C model = param.ReadObject<AbroadCreateOrderS2C>();
		await OnAbroadCreateOrderS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
