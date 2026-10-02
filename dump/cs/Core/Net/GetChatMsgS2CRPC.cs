using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetChatMsgS2CRPC
{
	public delegate UniTask OnGetChatMsgS2CServerDelegate(GetChatMsgS2C model, int errId, bool isDispatch);

	public OnGetChatMsgS2CServerDelegate OnGetChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushGetChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetChatMsgS2C model = param.ReadObject<GetChatMsgS2C>();
		await OnGetChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
