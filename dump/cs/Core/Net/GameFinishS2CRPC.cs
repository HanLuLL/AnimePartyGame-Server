using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GameFinishS2CRPC
{
	public delegate UniTask OnGameFinishS2CServerDelegate(GameFinishS2C model, int errId, bool isDispatch);

	public OnGameFinishS2CServerDelegate OnGameFinishS2CServerCallBackAsync;

	internal virtual async UniTask PushGameFinishS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGameFinishS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GameFinishS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GameFinishS2C model = param.ReadObject<GameFinishS2C>();
		await OnGameFinishS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
