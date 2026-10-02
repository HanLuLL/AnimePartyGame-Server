using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MoveAgainS2CRPC
{
	public delegate UniTask OnMoveAgainS2CServerDelegate(MoveAgainS2C model, int errId, bool isDispatch);

	public OnMoveAgainS2CServerDelegate OnMoveAgainS2CServerCallBackAsync;

	internal virtual async UniTask PushMoveAgainS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMoveAgainS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MoveAgainS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MoveAgainS2C model = param.ReadObject<MoveAgainS2C>();
		await OnMoveAgainS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
