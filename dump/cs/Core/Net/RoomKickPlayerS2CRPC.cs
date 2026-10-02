using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomKickPlayerS2CRPC
{
	public delegate UniTask OnRoomKickPlayerS2CServerDelegate(RoomKickPlayerS2C model, int errId, bool isDispatch);

	public OnRoomKickPlayerS2CServerDelegate OnRoomKickPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomKickPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomKickPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomKickPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomKickPlayerS2C model = param.ReadObject<RoomKickPlayerS2C>();
		await OnRoomKickPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
