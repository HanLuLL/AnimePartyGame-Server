using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeRoomS2CRPC
{
	public delegate UniTask OnChangeRoomS2CServerDelegate(ChangeRoomS2C model, int errId, bool isDispatch);

	public OnChangeRoomS2CServerDelegate OnChangeRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeRoomS2C model = param.ReadObject<ChangeRoomS2C>();
		await OnChangeRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
