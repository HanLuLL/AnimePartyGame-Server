using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class DevChargeS2CRPC
{
	public delegate UniTask OnDevChargeS2CServerDelegate(DevChargeS2C model, int errId, bool isDispatch);

	public OnDevChargeS2CServerDelegate OnDevChargeS2CServerCallBackAsync;

	internal virtual async UniTask PushDevChargeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnDevChargeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议DevChargeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		DevChargeS2C model = param.ReadObject<DevChargeS2C>();
		await OnDevChargeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
