using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class KillMessageS2CRPC
{
	public delegate UniTask OnKillMessageS2CServerDelegate(KillMessageS2C model, int errId, bool isDispatch);

	public OnKillMessageS2CServerDelegate OnKillMessageS2CServerCallBackAsync;

	internal virtual async UniTask PushKillMessageS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnKillMessageS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议KillMessageS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		KillMessageS2C model = param.ReadObject<KillMessageS2C>();
		await OnKillMessageS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
