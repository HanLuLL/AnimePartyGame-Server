using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetSignInRewardS2CRPC
{
	public delegate UniTask OnGetSignInRewardS2CServerDelegate(GetSignInRewardS2C model, int errId, bool isDispatch);

	public OnGetSignInRewardS2CServerDelegate OnGetSignInRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushGetSignInRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetSignInRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetSignInRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetSignInRewardS2C model = param.ReadObject<GetSignInRewardS2C>();
		await OnGetSignInRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
