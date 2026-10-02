using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class VendorBuyCardS2CRPC
{
	public delegate UniTask OnVendorBuyCardS2CServerDelegate(VendorBuyCardS2C model, int errId, bool isDispatch);

	public OnVendorBuyCardS2CServerDelegate OnVendorBuyCardS2CServerCallBackAsync;

	internal virtual async UniTask PushVendorBuyCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnVendorBuyCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议VendorBuyCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		VendorBuyCardS2C model = param.ReadObject<VendorBuyCardS2C>();
		await OnVendorBuyCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
