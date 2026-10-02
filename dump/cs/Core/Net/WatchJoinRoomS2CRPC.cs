using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class WatchJoinRoomS2CRPC
{
	public delegate UniTask OnWatchJoinRoomS2CServerDelegate(WatchJoinRoomS2C model, int errId, bool isDispatch);

	public OnWatchJoinRoomS2CServerDelegate OnWatchJoinRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushWatchJoinRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnWatchJoinRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议WatchJoinRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		WatchJoinRoomS2C model = param.ReadObject<WatchJoinRoomS2C>();
		await OnWatchJoinRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
