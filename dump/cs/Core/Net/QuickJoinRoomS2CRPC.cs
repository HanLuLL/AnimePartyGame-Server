using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class QuickJoinRoomS2CRPC
{
	public delegate UniTask OnQuickJoinRoomS2CServerDelegate(QuickJoinRoomS2C model, int errId, bool isDispatch);

	public OnQuickJoinRoomS2CServerDelegate OnQuickJoinRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushQuickJoinRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnQuickJoinRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议QuickJoinRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		QuickJoinRoomS2C model = param.ReadObject<QuickJoinRoomS2C>();
		await OnQuickJoinRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
