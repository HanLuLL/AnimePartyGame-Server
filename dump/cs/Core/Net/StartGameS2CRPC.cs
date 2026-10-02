using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class StartGameS2CRPC
{
	public delegate UniTask OnStartGameS2CServerDelegate(StartGameS2C model, int errId, bool isDispatch);

	public OnStartGameS2CServerDelegate OnStartGameS2CServerCallBackAsync;

	internal virtual async UniTask PushStartGameS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnStartGameS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议StartGameS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		StartGameS2C model = param.ReadObject<StartGameS2C>();
		await OnStartGameS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
