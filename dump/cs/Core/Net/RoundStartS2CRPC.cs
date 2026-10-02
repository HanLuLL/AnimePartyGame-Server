using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoundStartS2CRPC
{
	public delegate UniTask OnRoundStartS2CServerDelegate(RoundStartS2C model, int errId, bool isDispatch);

	public OnRoundStartS2CServerDelegate OnRoundStartS2CServerCallBackAsync;

	internal virtual async UniTask PushRoundStartS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoundStartS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoundStartS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoundStartS2C model = param.ReadObject<RoundStartS2C>();
		await OnRoundStartS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
