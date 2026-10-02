using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActivityTaskRewardS2CRPC
{
	public delegate UniTask OnActivityTaskRewardS2CServerDelegate(ActivityTaskRewardS2C model, int errId, bool isDispatch);

	public OnActivityTaskRewardS2CServerDelegate OnActivityTaskRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushActivityTaskRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActivityTaskRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActivityTaskRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActivityTaskRewardS2C model = param.ReadObject<ActivityTaskRewardS2C>();
		await OnActivityTaskRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
