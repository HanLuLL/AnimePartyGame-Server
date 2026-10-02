using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ExitGuildS2CRPC
{
	public delegate UniTask OnExitGuildS2CServerDelegate(ExitGuildS2C model, int errId, bool isDispatch);

	public OnExitGuildS2CServerDelegate OnExitGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushExitGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnExitGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ExitGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ExitGuildS2C model = param.ReadObject<ExitGuildS2C>();
		await OnExitGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
