using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoomHeroCardChangeS2CRPC
{
	public delegate UniTask OnRoomHeroCardChangeS2CServerDelegate(RoomHeroCardChangeS2C model, int errId, bool isDispatch);

	public OnRoomHeroCardChangeS2CServerDelegate OnRoomHeroCardChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushRoomHeroCardChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoomHeroCardChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoomHeroCardChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoomHeroCardChangeS2C model = param.ReadObject<RoomHeroCardChangeS2C>();
		await OnRoomHeroCardChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
