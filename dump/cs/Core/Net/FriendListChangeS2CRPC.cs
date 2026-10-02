using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendListChangeS2CRPC
{
	public delegate UniTask OnFriendListChangeS2CServerDelegate(FriendListChangeS2C model, int errId, bool isDispatch);

	public OnFriendListChangeS2CServerDelegate OnFriendListChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendListChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendListChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendListChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendListChangeS2C model = param.ReadObject<FriendListChangeS2C>();
		await OnFriendListChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
