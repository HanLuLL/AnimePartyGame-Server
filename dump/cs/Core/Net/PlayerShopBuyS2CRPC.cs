using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PlayerShopBuyS2CRPC
{
	public delegate UniTask OnPlayerShopBuyS2CServerDelegate(PlayerShopBuyS2C model, int errId, bool isDispatch);

	public OnPlayerShopBuyS2CServerDelegate OnPlayerShopBuyS2CServerCallBackAsync;

	internal virtual async UniTask PushPlayerShopBuyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPlayerShopBuyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PlayerShopBuyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PlayerShopBuyS2C model = param.ReadObject<PlayerShopBuyS2C>();
		await OnPlayerShopBuyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
