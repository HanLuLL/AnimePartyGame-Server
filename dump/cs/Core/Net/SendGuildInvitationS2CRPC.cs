using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SendGuildInvitationS2CRPC
{
	public delegate UniTask OnSendGuildInvitationS2CServerDelegate(SendGuildInvitationS2C model, int errId, bool isDispatch);

	public OnSendGuildInvitationS2CServerDelegate OnSendGuildInvitationS2CServerCallBackAsync;

	internal virtual async UniTask PushSendGuildInvitationS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSendGuildInvitationS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SendGuildInvitationS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SendGuildInvitationS2C model = param.ReadObject<SendGuildInvitationS2C>();
		await OnSendGuildInvitationS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
