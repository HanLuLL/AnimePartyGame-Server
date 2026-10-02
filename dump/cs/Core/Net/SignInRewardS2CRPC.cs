using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SignInRewardS2CRPC
{
	public delegate UniTask OnSignInRewardS2CServerDelegate(SignInRewardS2C model, int errId, bool isDispatch);

	public OnSignInRewardS2CServerDelegate OnSignInRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushSignInRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSignInRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SignInRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SignInRewardS2C model = param.ReadObject<SignInRewardS2C>();
		await OnSignInRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
