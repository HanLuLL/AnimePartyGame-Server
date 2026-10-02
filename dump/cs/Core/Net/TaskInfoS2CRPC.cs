using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TaskInfoS2CRPC
{
	public delegate UniTask OnTaskInfoS2CServerDelegate(TaskInfoS2C model, int errId, bool isDispatch);

	public OnTaskInfoS2CServerDelegate OnTaskInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushTaskInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTaskInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TaskInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TaskInfoS2C model = param.ReadObject<TaskInfoS2C>();
		await OnTaskInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
