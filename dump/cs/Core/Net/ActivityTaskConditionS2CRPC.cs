using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActivityTaskConditionS2CRPC
{
	public delegate UniTask OnActivityTaskConditionS2CServerDelegate(ActivityTaskConditionS2C model, int errId, bool isDispatch);

	public OnActivityTaskConditionS2CServerDelegate OnActivityTaskConditionS2CServerCallBackAsync;

	internal virtual async UniTask PushActivityTaskConditionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActivityTaskConditionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActivityTaskConditionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActivityTaskConditionS2C model = param.ReadObject<ActivityTaskConditionS2C>();
		await OnActivityTaskConditionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
