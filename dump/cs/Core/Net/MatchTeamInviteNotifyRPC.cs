using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchTeamInviteNotifyRPC
{
	public delegate UniTask OnMatchTeamInviteNotifyServerDelegate(MatchTeamInviteNotify model, int errId, bool isDispatch);

	public OnMatchTeamInviteNotifyServerDelegate OnMatchTeamInviteNotifyServerCallBackAsync;

	internal virtual async UniTask PushMatchTeamInviteNotifyCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchTeamInviteNotifyServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchTeamInviteNotify的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchTeamInviteNotify model = param.ReadObject<MatchTeamInviteNotify>();
		await OnMatchTeamInviteNotifyServerCallBackAsync(model, errId, isDispatch);
	}
}
