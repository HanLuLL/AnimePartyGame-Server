using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendDelNotifyS2CRPC
{
	public delegate UniTask OnFriendDelNotifyS2CServerDelegate(FriendDelNotifyS2C model, int errId, bool isDispatch);

	public OnFriendDelNotifyS2CServerDelegate OnFriendDelNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendDelNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendDelNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendDelNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendDelNotifyS2C model = param.ReadObject<FriendDelNotifyS2C>();
		await OnFriendDelNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
