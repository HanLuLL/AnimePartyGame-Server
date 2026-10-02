using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RefreshMatchTeamStateNotifyRPC
{
	public delegate UniTask OnRefreshMatchTeamStateNotifyServerDelegate(RefreshMatchTeamStateNotify model, int errId, bool isDispatch);

	public OnRefreshMatchTeamStateNotifyServerDelegate OnRefreshMatchTeamStateNotifyServerCallBackAsync;

	internal virtual async UniTask PushRefreshMatchTeamStateNotifyCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRefreshMatchTeamStateNotifyServerCallBackAsync == null)
		{
			Debug.LogError("协议RefreshMatchTeamStateNotify的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RefreshMatchTeamStateNotify model = param.ReadObject<RefreshMatchTeamStateNotify>();
		await OnRefreshMatchTeamStateNotifyServerCallBackAsync(model, errId, isDispatch);
	}
}
