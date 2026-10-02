using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AcquisitionRewardS2CRPC
{
	public delegate UniTask OnAcquisitionRewardS2CServerDelegate(AcquisitionRewardS2C model, int errId, bool isDispatch);

	public OnAcquisitionRewardS2CServerDelegate OnAcquisitionRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushAcquisitionRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAcquisitionRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AcquisitionRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AcquisitionRewardS2C model = param.ReadObject<AcquisitionRewardS2C>();
		await OnAcquisitionRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
