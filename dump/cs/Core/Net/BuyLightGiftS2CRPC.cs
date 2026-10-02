using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BuyLightGiftS2CRPC
{
	public delegate UniTask OnBuyLightGiftS2CServerDelegate(BuyLightGiftS2C model, int errId, bool isDispatch);

	public OnBuyLightGiftS2CServerDelegate OnBuyLightGiftS2CServerCallBackAsync;

	internal virtual async UniTask PushBuyLightGiftS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBuyLightGiftS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BuyLightGiftS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BuyLightGiftS2C model = param.ReadObject<BuyLightGiftS2C>();
		await OnBuyLightGiftS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
