using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchTeamReadyS2CRPC
{
	public delegate UniTask OnMatchTeamReadyS2CServerDelegate(MatchTeamReadyS2C model, int errId, bool isDispatch);

	public OnMatchTeamReadyS2CServerDelegate OnMatchTeamReadyS2CServerCallBackAsync;

	internal virtual async UniTask PushMatchTeamReadyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchTeamReadyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchTeamReadyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchTeamReadyS2C model = param.ReadObject<MatchTeamReadyS2C>();
		await OnMatchTeamReadyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
