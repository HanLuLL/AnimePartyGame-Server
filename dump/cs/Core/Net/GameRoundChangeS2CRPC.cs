using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GameRoundChangeS2CRPC
{
	public delegate UniTask OnGameRoundChangeS2CServerDelegate(GameRoundChangeS2C model, int errId, bool isDispatch);

	public OnGameRoundChangeS2CServerDelegate OnGameRoundChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushGameRoundChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGameRoundChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GameRoundChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GameRoundChangeS2C model = param.ReadObject<GameRoundChangeS2C>();
		await OnGameRoundChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
