using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GamePassMapSuccessS2CRPC
{
	public delegate UniTask OnGamePassMapSuccessS2CServerDelegate(GamePassMapSuccessS2C model, int errId, bool isDispatch);

	public OnGamePassMapSuccessS2CServerDelegate OnGamePassMapSuccessS2CServerCallBackAsync;

	internal virtual async UniTask PushGamePassMapSuccessS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGamePassMapSuccessS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GamePassMapSuccessS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GamePassMapSuccessS2C model = param.ReadObject<GamePassMapSuccessS2C>();
		await OnGamePassMapSuccessS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
