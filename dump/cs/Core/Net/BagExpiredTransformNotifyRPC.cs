using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BagExpiredTransformNotifyRPC
{
	public delegate UniTask OnBagExpiredTransformNotifyServerDelegate(BagExpiredTransformNotify model, int errId, bool isDispatch);

	public OnBagExpiredTransformNotifyServerDelegate OnBagExpiredTransformNotifyServerCallBackAsync;

	internal virtual async UniTask PushBagExpiredTransformNotifyCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBagExpiredTransformNotifyServerCallBackAsync == null)
		{
			Debug.LogError("协议BagExpiredTransformNotify的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BagExpiredTransformNotify model = param.ReadObject<BagExpiredTransformNotify>();
		await OnBagExpiredTransformNotifyServerCallBackAsync(model, errId, isDispatch);
	}
}
