using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReturnGiftClaimS2CRPC
{
	public delegate UniTask OnReturnGiftClaimS2CServerDelegate(ReturnGiftClaimS2C model, int errId, bool isDispatch);

	public OnReturnGiftClaimS2CServerDelegate OnReturnGiftClaimS2CServerCallBackAsync;

	internal virtual async UniTask PushReturnGiftClaimS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReturnGiftClaimS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReturnGiftClaimS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReturnGiftClaimS2C model = param.ReadObject<ReturnGiftClaimS2C>();
		await OnReturnGiftClaimS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
