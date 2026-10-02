using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AcquisitionS2CRPC
{
	public delegate UniTask OnAcquisitionS2CServerDelegate(AcquisitionS2C model, int errId, bool isDispatch);

	public OnAcquisitionS2CServerDelegate OnAcquisitionS2CServerCallBackAsync;

	internal virtual async UniTask PushAcquisitionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAcquisitionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AcquisitionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AcquisitionS2C model = param.ReadObject<AcquisitionS2C>();
		await OnAcquisitionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
