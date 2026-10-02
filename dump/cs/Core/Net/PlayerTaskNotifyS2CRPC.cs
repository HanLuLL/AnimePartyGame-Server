using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PlayerTaskNotifyS2CRPC
{
	public delegate UniTask OnPlayerTaskNotifyS2CServerDelegate(PlayerTaskNotifyS2C model, int errId, bool isDispatch);

	public OnPlayerTaskNotifyS2CServerDelegate OnPlayerTaskNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushPlayerTaskNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPlayerTaskNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PlayerTaskNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PlayerTaskNotifyS2C model = param.ReadObject<PlayerTaskNotifyS2C>();
		await OnPlayerTaskNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
