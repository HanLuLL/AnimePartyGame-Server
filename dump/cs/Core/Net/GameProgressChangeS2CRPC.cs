using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GameProgressChangeS2CRPC
{
	public delegate UniTask OnGameProgressChangeS2CServerDelegate(GameProgressChangeS2C model, int errId, bool isDispatch);

	public OnGameProgressChangeS2CServerDelegate OnGameProgressChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushGameProgressChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGameProgressChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GameProgressChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GameProgressChangeS2C model = param.ReadObject<GameProgressChangeS2C>();
		await OnGameProgressChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
