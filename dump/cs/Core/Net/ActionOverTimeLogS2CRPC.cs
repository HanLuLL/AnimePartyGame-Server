using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActionOverTimeLogS2CRPC
{
	public delegate UniTask OnActionOverTimeLogS2CServerDelegate(ActionOverTimeLogS2C model, int errId, bool isDispatch);

	public OnActionOverTimeLogS2CServerDelegate OnActionOverTimeLogS2CServerCallBackAsync;

	internal virtual async UniTask PushActionOverTimeLogS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActionOverTimeLogS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActionOverTimeLogS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActionOverTimeLogS2C model = param.ReadObject<ActionOverTimeLogS2C>();
		await OnActionOverTimeLogS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
