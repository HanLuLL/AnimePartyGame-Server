using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RunningGameS2CRPC
{
	public delegate UniTask OnRunningGameS2CServerDelegate(RunningGameS2C model, int errId, bool isDispatch);

	public OnRunningGameS2CServerDelegate OnRunningGameS2CServerCallBackAsync;

	internal virtual async UniTask PushRunningGameS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRunningGameS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RunningGameS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RunningGameS2C model = param.ReadObject<RunningGameS2C>();
		await OnRunningGameS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
