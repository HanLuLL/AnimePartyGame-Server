using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GiftCdkS2CRPC
{
	public delegate UniTask OnGiftCdkS2CServerDelegate(GiftCdkS2C model, int errId, bool isDispatch);

	public OnGiftCdkS2CServerDelegate OnGiftCdkS2CServerCallBackAsync;

	internal virtual async UniTask PushGiftCdkS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGiftCdkS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GiftCdkS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GiftCdkS2C model = param.ReadObject<GiftCdkS2C>();
		await OnGiftCdkS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
