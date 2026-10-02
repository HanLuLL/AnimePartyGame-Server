using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RookieGachaRewardS2CRPC
{
	public delegate UniTask OnRookieGachaRewardS2CServerDelegate(RookieGachaRewardS2C model, int errId, bool isDispatch);

	public OnRookieGachaRewardS2CServerDelegate OnRookieGachaRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushRookieGachaRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRookieGachaRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RookieGachaRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RookieGachaRewardS2C model = param.ReadObject<RookieGachaRewardS2C>();
		await OnRookieGachaRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
