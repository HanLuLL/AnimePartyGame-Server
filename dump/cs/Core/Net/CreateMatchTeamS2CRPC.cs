using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CreateMatchTeamS2CRPC
{
	public delegate UniTask OnCreateMatchTeamS2CServerDelegate(CreateMatchTeamS2C model, int errId, bool isDispatch);

	public OnCreateMatchTeamS2CServerDelegate OnCreateMatchTeamS2CServerCallBackAsync;

	internal virtual async UniTask PushCreateMatchTeamS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCreateMatchTeamS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CreateMatchTeamS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CreateMatchTeamS2C model = param.ReadObject<CreateMatchTeamS2C>();
		await OnCreateMatchTeamS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
