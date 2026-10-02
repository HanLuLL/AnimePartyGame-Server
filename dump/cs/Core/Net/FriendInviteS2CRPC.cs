using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendInviteS2CRPC
{
	public delegate UniTask OnFriendInviteS2CServerDelegate(FriendInviteS2C model, int errId, bool isDispatch);

	public OnFriendInviteS2CServerDelegate OnFriendInviteS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendInviteS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendInviteS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendInviteS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendInviteS2C model = param.ReadObject<FriendInviteS2C>();
		await OnFriendInviteS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
