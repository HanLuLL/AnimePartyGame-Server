using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class JoinRoomS2CRPC
{
	public delegate UniTask OnJoinRoomS2CServerDelegate(JoinRoomS2C model, int errId, bool isDispatch);

	public OnJoinRoomS2CServerDelegate OnJoinRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushJoinRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnJoinRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议JoinRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		JoinRoomS2C model = param.ReadObject<JoinRoomS2C>();
		await OnJoinRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
