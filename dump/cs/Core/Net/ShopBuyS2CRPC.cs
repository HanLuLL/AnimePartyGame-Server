using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ShopBuyS2CRPC
{
	public delegate UniTask OnShopBuyS2CServerDelegate(ShopBuyS2C model, int errId, bool isDispatch);

	public OnShopBuyS2CServerDelegate OnShopBuyS2CServerCallBackAsync;

	internal virtual async UniTask PushShopBuyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnShopBuyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ShopBuyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ShopBuyS2C model = param.ReadObject<ShopBuyS2C>();
		await OnShopBuyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
