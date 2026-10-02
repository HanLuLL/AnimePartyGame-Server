using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GameScoreChangeS2CRPC
{
	public delegate UniTask OnGameScoreChangeS2CServerDelegate(GameScoreChangeS2C model, int errId, bool isDispatch);

	public OnGameScoreChangeS2CServerDelegate OnGameScoreChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushGameScoreChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGameScoreChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GameScoreChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GameScoreChangeS2C model = param.ReadObject<GameScoreChangeS2C>();
		await OnGameScoreChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
