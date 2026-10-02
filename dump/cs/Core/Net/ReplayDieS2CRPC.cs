using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReplayDieS2CRPC
{
	public delegate UniTask OnReplayDieS2CServerDelegate(ReplayDieS2C model, int errId, bool isDispatch);

	public OnReplayDieS2CServerDelegate OnReplayDieS2CServerCallBackAsync;

	internal virtual async UniTask PushReplayDieS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReplayDieS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReplayDieS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReplayDieS2C model = param.ReadObject<ReplayDieS2C>();
		await OnReplayDieS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
