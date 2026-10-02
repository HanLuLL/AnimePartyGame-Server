using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchTeamChatS2CRPC
{
	public delegate UniTask OnMatchTeamChatS2CServerDelegate(MatchTeamChatS2C model, int errId, bool isDispatch);

	public OnMatchTeamChatS2CServerDelegate OnMatchTeamChatS2CServerCallBackAsync;

	internal virtual async UniTask PushMatchTeamChatS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchTeamChatS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchTeamChatS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchTeamChatS2C model = param.ReadObject<MatchTeamChatS2C>();
		await OnMatchTeamChatS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
