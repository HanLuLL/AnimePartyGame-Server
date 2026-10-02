using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChargeS2CRPC
{
	public delegate UniTask OnChargeS2CServerDelegate(ChargeS2C model, int errId, bool isDispatch);

	public OnChargeS2CServerDelegate OnChargeS2CServerCallBackAsync;

	internal virtual async UniTask PushChargeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChargeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChargeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChargeS2C model = param.ReadObject<ChargeS2C>();
		await OnChargeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
