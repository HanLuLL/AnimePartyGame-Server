using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RefreshMatchTeamInfoS2CRPC
{
	public delegate UniTask OnRefreshMatchTeamInfoS2CServerDelegate(RefreshMatchTeamInfoS2C model, int errId, bool isDispatch);

	public OnRefreshMatchTeamInfoS2CServerDelegate OnRefreshMatchTeamInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushRefreshMatchTeamInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRefreshMatchTeamInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RefreshMatchTeamInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RefreshMatchTeamInfoS2C model = param.ReadObject<RefreshMatchTeamInfoS2C>();
		await OnRefreshMatchTeamInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
