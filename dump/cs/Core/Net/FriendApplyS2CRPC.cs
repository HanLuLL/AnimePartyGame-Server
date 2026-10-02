using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendApplyS2CRPC
{
	public delegate UniTask OnFriendApplyS2CServerDelegate(FriendApplyS2C model, int errId, bool isDispatch);

	public OnFriendApplyS2CServerDelegate OnFriendApplyS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendApplyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendApplyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendApplyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendApplyS2C model = param.ReadObject<FriendApplyS2C>();
		await OnFriendApplyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
