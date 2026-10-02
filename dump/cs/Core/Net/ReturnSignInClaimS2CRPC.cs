using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReturnSignInClaimS2CRPC
{
	public delegate UniTask OnReturnSignInClaimS2CServerDelegate(ReturnSignInClaimS2C model, int errId, bool isDispatch);

	public OnReturnSignInClaimS2CServerDelegate OnReturnSignInClaimS2CServerCallBackAsync;

	internal virtual async UniTask PushReturnSignInClaimS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReturnSignInClaimS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReturnSignInClaimS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReturnSignInClaimS2C model = param.ReadObject<ReturnSignInClaimS2C>();
		await OnReturnSignInClaimS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
