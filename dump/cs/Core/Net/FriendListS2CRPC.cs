using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendListS2CRPC
{
	public delegate UniTask OnFriendListS2CServerDelegate(FriendListS2C model, int errId, bool isDispatch);

	public OnFriendListS2CServerDelegate OnFriendListS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendListS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendListS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendListS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendListS2C model = param.ReadObject<FriendListS2C>();
		await OnFriendListS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
