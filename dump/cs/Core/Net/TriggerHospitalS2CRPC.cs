using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TriggerHospitalS2CRPC
{
	public delegate UniTask OnTriggerHospitalS2CServerDelegate(TriggerHospitalS2C model, int errId, bool isDispatch);

	public OnTriggerHospitalS2CServerDelegate OnTriggerHospitalS2CServerCallBackAsync;

	internal virtual async UniTask PushTriggerHospitalS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTriggerHospitalS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TriggerHospitalS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TriggerHospitalS2C model = param.ReadObject<TriggerHospitalS2C>();
		await OnTriggerHospitalS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
