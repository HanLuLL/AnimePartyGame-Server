using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class OpsChangeSlotS2CRPC
{
	public delegate UniTask OnOpsChangeSlotS2CServerDelegate(OpsChangeSlotS2C model, int errId, bool isDispatch);

	public OnOpsChangeSlotS2CServerDelegate OnOpsChangeSlotS2CServerCallBackAsync;

	internal virtual async UniTask PushOpsChangeSlotS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnOpsChangeSlotS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议OpsChangeSlotS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		OpsChangeSlotS2C model = param.ReadObject<OpsChangeSlotS2C>();
		await OnOpsChangeSlotS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
