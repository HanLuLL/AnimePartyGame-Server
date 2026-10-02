using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendSendMsgS2CRPC
{
	public delegate UniTask OnFriendSendMsgS2CServerDelegate(FriendSendMsgS2C model, int errId, bool isDispatch);

	public OnFriendSendMsgS2CServerDelegate OnFriendSendMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendSendMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendSendMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendSendMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendSendMsgS2C model = param.ReadObject<FriendSendMsgS2C>();
		await OnFriendSendMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
