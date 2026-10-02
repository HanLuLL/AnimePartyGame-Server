using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomReadyS2CRPC
{
	public delegate UniTask OnRoomReadyS2CServerDelegate(RoomReadyS2C model, int errId, bool isDispatch);

	public OnRoomReadyS2CServerDelegate OnRoomReadyS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomReadyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomReadyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomReadyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomReadyS2C model = param.ReadObject<RoomReadyS2C>();
		await OnRoomReadyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
