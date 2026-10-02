using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CreateRoomS2CRPC
{
	public delegate UniTask OnCreateRoomS2CServerDelegate(CreateRoomS2C model, int errId, bool isDispatch);

	public OnCreateRoomS2CServerDelegate OnCreateRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushCreateRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCreateRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CreateRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CreateRoomS2C model = param.ReadObject<CreateRoomS2C>();
		await OnCreateRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
