using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomNotifyS2CRPC
{
	public delegate UniTask OnRoomNotifyS2CServerDelegate(RoomNotifyS2C model, int errId, bool isDispatch);

	public OnRoomNotifyS2CServerDelegate OnRoomNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomNotifyS2C model = param.ReadObject<RoomNotifyS2C>();
		await OnRoomNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
