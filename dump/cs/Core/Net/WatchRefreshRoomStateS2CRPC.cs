using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class WatchRefreshRoomStateS2CRPC
{
	public delegate UniTask OnWatchRefreshRoomStateS2CServerDelegate(WatchRefreshRoomStateS2C model, int errId, bool isDispatch);

	public OnWatchRefreshRoomStateS2CServerDelegate OnWatchRefreshRoomStateS2CServerCallBackAsync;

	internal virtual async UniTask PushWatchRefreshRoomStateS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnWatchRefreshRoomStateS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议WatchRefreshRoomStateS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		WatchRefreshRoomStateS2C model = param.ReadObject<WatchRefreshRoomStateS2C>();
		await OnWatchRefreshRoomStateS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
