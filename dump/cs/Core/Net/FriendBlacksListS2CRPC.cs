using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendBlacksListS2CRPC
{
	public delegate UniTask OnFriendBlacksListS2CServerDelegate(FriendBlacksListS2C model, int errId, bool isDispatch);

	public OnFriendBlacksListS2CServerDelegate OnFriendBlacksListS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendBlacksListS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendBlacksListS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendBlacksListS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendBlacksListS2C model = param.ReadObject<FriendBlacksListS2C>();
		await OnFriendBlacksListS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
