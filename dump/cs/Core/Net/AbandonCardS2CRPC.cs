using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AbandonCardS2CRPC
{
	public delegate UniTask OnAbandonCardS2CServerDelegate(AbandonCardS2C model, int errId, bool isDispatch);

	public OnAbandonCardS2CServerDelegate OnAbandonCardS2CServerCallBackAsync;

	internal virtual async UniTask PushAbandonCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAbandonCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AbandonCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AbandonCardS2C model = param.ReadObject<AbandonCardS2C>();
		await OnAbandonCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
