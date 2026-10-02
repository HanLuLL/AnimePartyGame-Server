using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FriendApplyOpS2CRPC
{
	public delegate UniTask OnFriendApplyOpS2CServerDelegate(FriendApplyOpS2C model, int errId, bool isDispatch);

	public OnFriendApplyOpS2CServerDelegate OnFriendApplyOpS2CServerCallBackAsync;

	internal virtual async UniTask PushFriendApplyOpS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFriendApplyOpS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FriendApplyOpS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FriendApplyOpS2C model = param.ReadObject<FriendApplyOpS2C>();
		await OnFriendApplyOpS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
