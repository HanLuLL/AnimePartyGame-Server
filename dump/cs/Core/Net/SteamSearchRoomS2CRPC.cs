using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SteamSearchRoomS2CRPC
{
	public delegate UniTask OnSteamSearchRoomS2CServerDelegate(SteamSearchRoomS2C model, int errId, bool isDispatch);

	public OnSteamSearchRoomS2CServerDelegate OnSteamSearchRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushSteamSearchRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSteamSearchRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SteamSearchRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SteamSearchRoomS2C model = param.ReadObject<SteamSearchRoomS2C>();
		await OnSteamSearchRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
