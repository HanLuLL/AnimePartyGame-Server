using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MovePointBuffS2CRPC
{
	public delegate UniTask OnMovePointBuffS2CServerDelegate(MovePointBuffS2C model, int errId, bool isDispatch);

	public OnMovePointBuffS2CServerDelegate OnMovePointBuffS2CServerCallBackAsync;

	internal virtual async UniTask PushMovePointBuffS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMovePointBuffS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MovePointBuffS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MovePointBuffS2C model = param.ReadObject<MovePointBuffS2C>();
		await OnMovePointBuffS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
