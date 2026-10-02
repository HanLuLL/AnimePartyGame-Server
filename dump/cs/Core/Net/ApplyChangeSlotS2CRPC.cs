using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ApplyChangeSlotS2CRPC
{
	public delegate UniTask OnApplyChangeSlotS2CServerDelegate(ApplyChangeSlotS2C model, int errId, bool isDispatch);

	public OnApplyChangeSlotS2CServerDelegate OnApplyChangeSlotS2CServerCallBackAsync;

	internal virtual async UniTask PushApplyChangeSlotS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnApplyChangeSlotS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ApplyChangeSlotS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ApplyChangeSlotS2C model = param.ReadObject<ApplyChangeSlotS2C>();
		await OnApplyChangeSlotS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
