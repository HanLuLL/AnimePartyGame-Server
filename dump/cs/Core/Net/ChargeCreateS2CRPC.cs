using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChargeCreateS2CRPC
{
	public delegate UniTask OnChargeCreateS2CServerDelegate(ChargeCreateS2C model, int errId, bool isDispatch);

	public OnChargeCreateS2CServerDelegate OnChargeCreateS2CServerCallBackAsync;

	internal virtual async UniTask PushChargeCreateS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChargeCreateS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChargeCreateS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChargeCreateS2C model = param.ReadObject<ChargeCreateS2C>();
		await OnChargeCreateS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
