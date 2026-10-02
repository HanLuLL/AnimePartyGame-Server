using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ScratchCardS2CRPC
{
	public delegate UniTask OnScratchCardS2CServerDelegate(ScratchCardS2C model, int errId, bool isDispatch);

	public OnScratchCardS2CServerDelegate OnScratchCardS2CServerCallBackAsync;

	internal virtual async UniTask PushScratchCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnScratchCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ScratchCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ScratchCardS2C model = param.ReadObject<ScratchCardS2C>();
		await OnScratchCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
