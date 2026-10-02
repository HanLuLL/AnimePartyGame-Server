using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChatMapMarkersS2CRPC
{
	public delegate UniTask OnChatMapMarkersS2CServerDelegate(ChatMapMarkersS2C model, int errId, bool isDispatch);

	public OnChatMapMarkersS2CServerDelegate OnChatMapMarkersS2CServerCallBackAsync;

	internal virtual async UniTask PushChatMapMarkersS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChatMapMarkersS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChatMapMarkersS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChatMapMarkersS2C model = param.ReadObject<ChatMapMarkersS2C>();
		await OnChatMapMarkersS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
