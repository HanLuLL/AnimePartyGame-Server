using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendApplyListS2CRPC
{
	public delegate UniTask OnFriendApplyListS2CServerDelegate(FriendApplyListS2C model, int errId, bool isDispatch);

	public OnFriendApplyListS2CServerDelegate OnFriendApplyListS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendApplyListS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendApplyListS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendApplyListS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendApplyListS2C model = param.ReadObject<FriendApplyListS2C>();
		await OnFriendApplyListS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
