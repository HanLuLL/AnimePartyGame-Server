using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class InviteSuccessS2CRPC
{
	public delegate UniTask OnInviteSuccessS2CServerDelegate(InviteSuccessS2C model, int errId, bool isDispatch);

	public OnInviteSuccessS2CServerDelegate OnInviteSuccessS2CServerCallBackAsync;

	internal virtual async UniTask PushInviteSuccessS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnInviteSuccessS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议InviteSuccessS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		InviteSuccessS2C model = param.ReadObject<InviteSuccessS2C>();
		await OnInviteSuccessS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
