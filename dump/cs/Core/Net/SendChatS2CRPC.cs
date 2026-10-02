using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SendChatS2CRPC
{
	public delegate UniTask OnSendChatS2CServerDelegate(SendChatS2C model, int errId, bool isDispatch);

	public OnSendChatS2CServerDelegate OnSendChatS2CServerCallBackAsync;

	internal virtual async UniTask PushSendChatS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSendChatS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SendChatS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SendChatS2C model = param.ReadObject<SendChatS2C>();
		await OnSendChatS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
