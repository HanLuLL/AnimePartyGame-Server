using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class OnlineSyncRoomIdS2CRPC
{
	public delegate UniTask OnOnlineSyncRoomIdS2CServerDelegate(OnlineSyncRoomIdS2C model, int errId, bool isDispatch);

	public OnOnlineSyncRoomIdS2CServerDelegate OnOnlineSyncRoomIdS2CServerCallBackAsync;

	internal virtual async UniTask PushOnlineSyncRoomIdS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnOnlineSyncRoomIdS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议OnlineSyncRoomIdS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		OnlineSyncRoomIdS2C model = param.ReadObject<OnlineSyncRoomIdS2C>();
		await OnOnlineSyncRoomIdS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
