using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncRoomS2CRPC
{
	public delegate UniTask OnSyncRoomS2CServerDelegate(SyncRoomS2C model, int errId, bool isDispatch);

	public OnSyncRoomS2CServerDelegate OnSyncRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncRoomS2C model = param.ReadObject<SyncRoomS2C>();
		await OnSyncRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
