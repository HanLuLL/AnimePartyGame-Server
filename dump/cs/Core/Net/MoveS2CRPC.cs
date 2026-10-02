using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MoveS2CRPC
{
	public delegate UniTask OnMoveS2CServerDelegate(MoveS2C model, int errId, bool isDispatch);

	public OnMoveS2CServerDelegate OnMoveS2CServerCallBackAsync;

	internal virtual async UniTask PushMoveS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMoveS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MoveS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MoveS2C model = param.ReadObject<MoveS2C>();
		await OnMoveS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
