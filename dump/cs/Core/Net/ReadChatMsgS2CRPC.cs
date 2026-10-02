using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReadChatMsgS2CRPC
{
	public delegate UniTask OnReadChatMsgS2CServerDelegate(ReadChatMsgS2C model, int errId, bool isDispatch);

	public OnReadChatMsgS2CServerDelegate OnReadChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushReadChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReadChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReadChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReadChatMsgS2C model = param.ReadObject<ReadChatMsgS2C>();
		await OnReadChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
