using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ThrowDiceResultS2CRPC
{
	public delegate UniTask OnThrowDiceResultS2CServerDelegate(ThrowDiceResultS2C model, int errId, bool isDispatch);

	public OnThrowDiceResultS2CServerDelegate OnThrowDiceResultS2CServerCallBackAsync;

	internal virtual async UniTask PushThrowDiceResultS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnThrowDiceResultS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ThrowDiceResultS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ThrowDiceResultS2C model = param.ReadObject<ThrowDiceResultS2C>();
		await OnThrowDiceResultS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
