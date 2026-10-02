using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomRoundAddTermS2CRPC
{
	public delegate UniTask OnRoomRoundAddTermS2CServerDelegate(RoomRoundAddTermS2C model, int errId, bool isDispatch);

	public OnRoomRoundAddTermS2CServerDelegate OnRoomRoundAddTermS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomRoundAddTermS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomRoundAddTermS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomRoundAddTermS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomRoundAddTermS2C model = param.ReadObject<RoomRoundAddTermS2C>();
		await OnRoomRoundAddTermS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
