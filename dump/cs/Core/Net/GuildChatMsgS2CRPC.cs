using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GuildChatMsgS2CRPC
{
	public delegate UniTask OnGuildChatMsgS2CServerDelegate(GuildChatMsgS2C model, int errId, bool isDispatch);

	public OnGuildChatMsgS2CServerDelegate OnGuildChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushGuildChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGuildChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GuildChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GuildChatMsgS2C model = param.ReadObject<GuildChatMsgS2C>();
		await OnGuildChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
