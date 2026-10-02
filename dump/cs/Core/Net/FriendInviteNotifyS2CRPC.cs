using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendInviteNotifyS2CRPC
{
	public delegate UniTask OnFriendInviteNotifyS2CServerDelegate(FriendInviteNotifyS2C model, int errId, bool isDispatch);

	public OnFriendInviteNotifyS2CServerDelegate OnFriendInviteNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendInviteNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendInviteNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendInviteNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendInviteNotifyS2C model = param.ReadObject<FriendInviteNotifyS2C>();
		await OnFriendInviteNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
