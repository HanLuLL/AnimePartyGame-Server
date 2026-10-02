using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeMatchTeamS2CRPC
{
	public delegate UniTask OnChangeMatchTeamS2CServerDelegate(ChangeMatchTeamS2C model, int errId, bool isDispatch);

	public OnChangeMatchTeamS2CServerDelegate OnChangeMatchTeamS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeMatchTeamS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeMatchTeamS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeMatchTeamS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeMatchTeamS2C model = param.ReadObject<ChangeMatchTeamS2C>();
		await OnChangeMatchTeamS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
