using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendNotifyS2CRPC
{
	public delegate UniTask OnFriendNotifyS2CServerDelegate(FriendNotifyS2C model, int errId, bool isDispatch);

	public OnFriendNotifyS2CServerDelegate OnFriendNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendNotifyS2C model = param.ReadObject<FriendNotifyS2C>();
		await OnFriendNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
