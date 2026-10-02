using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NoGambleNotifyS2CRPC
{
	public delegate UniTask OnNoGambleNotifyS2CServerDelegate(NoGambleNotifyS2C model, int errId, bool isDispatch);

	public OnNoGambleNotifyS2CServerDelegate OnNoGambleNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushNoGambleNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNoGambleNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NoGambleNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NoGambleNotifyS2C model = param.ReadObject<NoGambleNotifyS2C>();
		await OnNoGambleNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
