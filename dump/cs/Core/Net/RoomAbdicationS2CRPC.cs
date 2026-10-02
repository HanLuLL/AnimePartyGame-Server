using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomAbdicationS2CRPC
{
	public delegate UniTask OnRoomAbdicationS2CServerDelegate(RoomAbdicationS2C model, int errId, bool isDispatch);

	public OnRoomAbdicationS2CServerDelegate OnRoomAbdicationS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomAbdicationS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomAbdicationS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomAbdicationS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomAbdicationS2C model = param.ReadObject<RoomAbdicationS2C>();
		await OnRoomAbdicationS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
