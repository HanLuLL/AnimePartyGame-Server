using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ThrowDiceS2CRPC
{
	public delegate UniTask OnThrowDiceS2CServerDelegate(ThrowDiceS2C model, int errId, bool isDispatch);

	public OnThrowDiceS2CServerDelegate OnThrowDiceS2CServerCallBackAsync;

	internal virtual async UniTask PushThrowDiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnThrowDiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ThrowDiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ThrowDiceS2C model = param.ReadObject<ThrowDiceS2C>();
		await OnThrowDiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
