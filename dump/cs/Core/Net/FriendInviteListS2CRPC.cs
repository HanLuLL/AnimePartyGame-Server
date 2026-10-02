using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendInviteListS2CRPC
{
	public delegate UniTask OnFriendInviteListS2CServerDelegate(FriendInviteListS2C model, int errId, bool isDispatch);

	public OnFriendInviteListS2CServerDelegate OnFriendInviteListS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendInviteListS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendInviteListS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendInviteListS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendInviteListS2C model = param.ReadObject<FriendInviteListS2C>();
		await OnFriendInviteListS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
