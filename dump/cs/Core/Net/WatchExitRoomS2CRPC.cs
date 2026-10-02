using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class WatchExitRoomS2CRPC
{
	public delegate UniTask OnWatchExitRoomS2CServerDelegate(WatchExitRoomS2C model, int errId, bool isDispatch);

	public OnWatchExitRoomS2CServerDelegate OnWatchExitRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushWatchExitRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnWatchExitRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议WatchExitRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		WatchExitRoomS2C model = param.ReadObject<WatchExitRoomS2C>();
		await OnWatchExitRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
