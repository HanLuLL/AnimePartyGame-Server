using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchTeamInviteS2CRPC
{
	public delegate UniTask OnMatchTeamInviteS2CServerDelegate(MatchTeamInviteS2C model, int errId, bool isDispatch);

	public OnMatchTeamInviteS2CServerDelegate OnMatchTeamInviteS2CServerCallBackAsync;

	internal virtual async UniTask PushMatchTeamInviteS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchTeamInviteS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchTeamInviteS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchTeamInviteS2C model = param.ReadObject<MatchTeamInviteS2C>();
		await OnMatchTeamInviteS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
