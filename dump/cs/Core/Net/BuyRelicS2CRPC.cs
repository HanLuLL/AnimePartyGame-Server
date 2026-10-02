using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BuyRelicS2CRPC
{
	public delegate UniTask OnBuyRelicS2CServerDelegate(BuyRelicS2C model, int errId, bool isDispatch);

	public OnBuyRelicS2CServerDelegate OnBuyRelicS2CServerCallBackAsync;

	internal virtual async UniTask PushBuyRelicS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBuyRelicS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BuyRelicS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BuyRelicS2C model = param.ReadObject<BuyRelicS2C>();
		await OnBuyRelicS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
