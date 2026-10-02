using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class StartGambleS2CRPC
{
	public delegate UniTask OnStartGambleS2CServerDelegate(StartGambleS2C model, int errId, bool isDispatch);

	public OnStartGambleS2CServerDelegate OnStartGambleS2CServerCallBackAsync;

	internal virtual async UniTask PushStartGambleS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnStartGambleS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议StartGambleS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		StartGambleS2C model = param.ReadObject<StartGambleS2C>();
		await OnStartGambleS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
