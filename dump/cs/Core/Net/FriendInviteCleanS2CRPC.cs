using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendInviteCleanS2CRPC
{
	public delegate UniTask OnFriendInviteCleanS2CServerDelegate(FriendInviteCleanS2C model, int errId, bool isDispatch);

	public OnFriendInviteCleanS2CServerDelegate OnFriendInviteCleanS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendInviteCleanS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendInviteCleanS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendInviteCleanS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendInviteCleanS2C model = param.ReadObject<FriendInviteCleanS2C>();
		await OnFriendInviteCleanS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
