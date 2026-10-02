using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PayResultS2CRPC
{
	public delegate UniTask OnPayResultS2CServerDelegate(PayResultS2C model, int errId, bool isDispatch);

	public OnPayResultS2CServerDelegate OnPayResultS2CServerCallBackAsync;

	internal virtual async UniTask PushPayResultS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPayResultS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PayResultS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PayResultS2C model = param.ReadObject<PayResultS2C>();
		await OnPayResultS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
