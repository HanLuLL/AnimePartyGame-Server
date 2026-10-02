using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class EventThrowDiceS2CRPC
{
	public delegate UniTask OnEventThrowDiceS2CServerDelegate(EventThrowDiceS2C model, int errId, bool isDispatch);

	public OnEventThrowDiceS2CServerDelegate OnEventThrowDiceS2CServerCallBackAsync;

	internal virtual async UniTask PushEventThrowDiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnEventThrowDiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议EventThrowDiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		EventThrowDiceS2C model = param.ReadObject<EventThrowDiceS2C>();
		await OnEventThrowDiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
