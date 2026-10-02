using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TaskRewardS2CRPC
{
	public delegate UniTask OnTaskRewardS2CServerDelegate(TaskRewardS2C model, int errId, bool isDispatch);

	public OnTaskRewardS2CServerDelegate OnTaskRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushTaskRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTaskRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TaskRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TaskRewardS2C model = param.ReadObject<TaskRewardS2C>();
		await OnTaskRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
