using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomShortChatS2CRPC
{
	public delegate UniTask OnRoomShortChatS2CServerDelegate(RoomShortChatS2C model, int errId, bool isDispatch);

	public OnRoomShortChatS2CServerDelegate OnRoomShortChatS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomShortChatS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomShortChatS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomShortChatS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomShortChatS2C model = param.ReadObject<RoomShortChatS2C>();
		await OnRoomShortChatS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
