using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class InviteInfoNotifyS2CRPC
{
	public delegate UniTask OnInviteInfoNotifyS2CServerDelegate(InviteInfoNotifyS2C model, int errId, bool isDispatch);

	public OnInviteInfoNotifyS2CServerDelegate OnInviteInfoNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushInviteInfoNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnInviteInfoNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议InviteInfoNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		InviteInfoNotifyS2C model = param.ReadObject<InviteInfoNotifyS2C>();
		await OnInviteInfoNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
