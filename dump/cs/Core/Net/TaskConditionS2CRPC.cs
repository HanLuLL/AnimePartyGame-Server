using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TaskConditionS2CRPC
{
	public delegate UniTask OnTaskConditionS2CServerDelegate(TaskConditionS2C model, int errId, bool isDispatch);

	public OnTaskConditionS2CServerDelegate OnTaskConditionS2CServerCallBackAsync;

	internal virtual async UniTask PushTaskConditionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTaskConditionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TaskConditionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TaskConditionS2C model = param.ReadObject<TaskConditionS2C>();
		await OnTaskConditionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
