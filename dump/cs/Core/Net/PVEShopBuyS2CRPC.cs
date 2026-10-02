using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PVEShopBuyS2CRPC
{
	public delegate UniTask OnPVEShopBuyS2CServerDelegate(PVEShopBuyS2C model, int errId, bool isDispatch);

	public OnPVEShopBuyS2CServerDelegate OnPVEShopBuyS2CServerCallBackAsync;

	internal virtual async UniTask PushPVEShopBuyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPVEShopBuyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PVEShopBuyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PVEShopBuyS2C model = param.ReadObject<PVEShopBuyS2C>();
		await OnPVEShopBuyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
