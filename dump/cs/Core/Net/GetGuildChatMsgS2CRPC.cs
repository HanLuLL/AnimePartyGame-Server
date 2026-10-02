using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetGuildChatMsgS2CRPC
{
	public delegate UniTask OnGetGuildChatMsgS2CServerDelegate(GetGuildChatMsgS2C model, int errId, bool isDispatch);

	public OnGetGuildChatMsgS2CServerDelegate OnGetGuildChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushGetGuildChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetGuildChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetGuildChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetGuildChatMsgS2C model = param.ReadObject<GetGuildChatMsgS2C>();
		await OnGetGuildChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
