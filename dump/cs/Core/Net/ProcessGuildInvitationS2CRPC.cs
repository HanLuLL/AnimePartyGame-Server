using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ProcessGuildInvitationS2CRPC
{
	public delegate UniTask OnProcessGuildInvitationS2CServerDelegate(ProcessGuildInvitationS2C model, int errId, bool isDispatch);

	public OnProcessGuildInvitationS2CServerDelegate OnProcessGuildInvitationS2CServerCallBackAsync;

	internal virtual async UniTask PushProcessGuildInvitationS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnProcessGuildInvitationS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ProcessGuildInvitationS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ProcessGuildInvitationS2C model = param.ReadObject<ProcessGuildInvitationS2C>();
		await OnProcessGuildInvitationS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
