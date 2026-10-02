using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SendGuildChatMsgS2CRPC
{
	public delegate UniTask OnSendGuildChatMsgS2CServerDelegate(SendGuildChatMsgS2C model, int errId, bool isDispatch);

	public OnSendGuildChatMsgS2CServerDelegate OnSendGuildChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushSendGuildChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSendGuildChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SendGuildChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SendGuildChatMsgS2C model = param.ReadObject<SendGuildChatMsgS2C>();
		await OnSendGuildChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
