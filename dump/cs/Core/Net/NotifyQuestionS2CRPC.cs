using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NotifyQuestionS2CRPC
{
	public delegate UniTask OnNotifyQuestionS2CServerDelegate(NotifyQuestionS2C model, int errId, bool isDispatch);

	public OnNotifyQuestionS2CServerDelegate OnNotifyQuestionS2CServerCallBackAsync;

	internal virtual async UniTask PushNotifyQuestionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNotifyQuestionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NotifyQuestionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NotifyQuestionS2C model = param.ReadObject<NotifyQuestionS2C>();
		await OnNotifyQuestionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
