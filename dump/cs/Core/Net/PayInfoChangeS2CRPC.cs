using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PayInfoChangeS2CRPC
{
	public delegate UniTask OnPayInfoChangeS2CServerDelegate(PayInfoChangeS2C model, int errId, bool isDispatch);

	public OnPayInfoChangeS2CServerDelegate OnPayInfoChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushPayInfoChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPayInfoChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PayInfoChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PayInfoChangeS2C model = param.ReadObject<PayInfoChangeS2C>();
		await OnPayInfoChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
