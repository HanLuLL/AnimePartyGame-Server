using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActionStartNotifyS2CRPC
{
	public delegate UniTask OnActionStartNotifyS2CServerDelegate(ActionStartNotifyS2C model, int errId, bool isDispatch);

	public OnActionStartNotifyS2CServerDelegate OnActionStartNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushActionStartNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActionStartNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActionStartNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActionStartNotifyS2C model = param.ReadObject<ActionStartNotifyS2C>();
		await OnActionStartNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
