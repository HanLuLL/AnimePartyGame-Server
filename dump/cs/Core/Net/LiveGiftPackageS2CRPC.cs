using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LiveGiftPackageS2CRPC
{
	public delegate UniTask OnLiveGiftPackageS2CServerDelegate(LiveGiftPackageS2C model, int errId, bool isDispatch);

	public OnLiveGiftPackageS2CServerDelegate OnLiveGiftPackageS2CServerCallBackAsync;

	internal virtual async UniTask PushLiveGiftPackageS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLiveGiftPackageS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LiveGiftPackageS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LiveGiftPackageS2C model = param.ReadObject<LiveGiftPackageS2C>();
		await OnLiveGiftPackageS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
