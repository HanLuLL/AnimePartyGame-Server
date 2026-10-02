using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class DelChatMsgInfoS2CRPC
{
	public delegate UniTask OnDelChatMsgInfoS2CServerDelegate(DelChatMsgInfoS2C model, int errId, bool isDispatch);

	public OnDelChatMsgInfoS2CServerDelegate OnDelChatMsgInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushDelChatMsgInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnDelChatMsgInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议DelChatMsgInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		DelChatMsgInfoS2C model = param.ReadObject<DelChatMsgInfoS2C>();
		await OnDelChatMsgInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
