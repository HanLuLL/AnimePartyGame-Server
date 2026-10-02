using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ExitMatchTeamS2CRPC
{
	public delegate UniTask OnExitMatchTeamS2CServerDelegate(ExitMatchTeamS2C model, int errId, bool isDispatch);

	public OnExitMatchTeamS2CServerDelegate OnExitMatchTeamS2CServerCallBackAsync;

	internal virtual async UniTask PushExitMatchTeamS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnExitMatchTeamS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ExitMatchTeamS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ExitMatchTeamS2C model = param.ReadObject<ExitMatchTeamS2C>();
		await OnExitMatchTeamS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
