using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RefreshRoomStateS2CRPC
{
	public delegate UniTask OnRefreshRoomStateS2CServerDelegate(RefreshRoomStateS2C model, int errId, bool isDispatch);

	public OnRefreshRoomStateS2CServerDelegate OnRefreshRoomStateS2CServerCallBackAsync;

	internal virtual async UniTask PushRefreshRoomStateS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRefreshRoomStateS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RefreshRoomStateS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RefreshRoomStateS2C model = param.ReadObject<RefreshRoomStateS2C>();
		await OnRefreshRoomStateS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
