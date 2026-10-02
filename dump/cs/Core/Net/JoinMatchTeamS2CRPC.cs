using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class JoinMatchTeamS2CRPC
{
	public delegate UniTask OnJoinMatchTeamS2CServerDelegate(JoinMatchTeamS2C model, int errId, bool isDispatch);

	public OnJoinMatchTeamS2CServerDelegate OnJoinMatchTeamS2CServerCallBackAsync;

	internal virtual async UniTask PushJoinMatchTeamS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnJoinMatchTeamS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议JoinMatchTeamS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		JoinMatchTeamS2C model = param.ReadObject<JoinMatchTeamS2C>();
		await OnJoinMatchTeamS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
