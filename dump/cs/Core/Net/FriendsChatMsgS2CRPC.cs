using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendsChatMsgS2CRPC
{
	public delegate UniTask OnFriendsChatMsgS2CServerDelegate(FriendsChatMsgS2C model, int errId, bool isDispatch);

	public OnFriendsChatMsgS2CServerDelegate OnFriendsChatMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendsChatMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendsChatMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendsChatMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendsChatMsgS2C model = param.ReadObject<FriendsChatMsgS2C>();
		await OnFriendsChatMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
