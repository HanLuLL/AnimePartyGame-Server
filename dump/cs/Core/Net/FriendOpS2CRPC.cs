using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendOpS2CRPC
{
	public delegate UniTask OnFriendOpS2CServerDelegate(FriendOpS2C model, int errId, bool isDispatch);

	public OnFriendOpS2CServerDelegate OnFriendOpS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendOpS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendOpS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendOpS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendOpS2C model = param.ReadObject<FriendOpS2C>();
		await OnFriendOpS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
