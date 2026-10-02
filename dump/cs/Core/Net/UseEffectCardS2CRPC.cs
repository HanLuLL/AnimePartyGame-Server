using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UseEffectCardS2CRPC
{
	public delegate UniTask OnUseEffectCardS2CServerDelegate(UseEffectCardS2C model, int errId, bool isDispatch);

	public OnUseEffectCardS2CServerDelegate OnUseEffectCardS2CServerCallBackAsync;

	internal virtual async UniTask PushUseEffectCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUseEffectCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UseEffectCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UseEffectCardS2C model = param.ReadObject<UseEffectCardS2C>();
		await OnUseEffectCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
