using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ExitRoomS2CRPC
{
	public delegate UniTask OnExitRoomS2CServerDelegate(ExitRoomS2C model, int errId, bool isDispatch);

	public OnExitRoomS2CServerDelegate OnExitRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushExitRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnExitRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ExitRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ExitRoomS2C model = param.ReadObject<ExitRoomS2C>();
		await OnExitRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
