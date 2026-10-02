using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TimeOutKickPlayerS2CRPC
{
	public delegate UniTask OnTimeOutKickPlayerS2CServerDelegate(TimeOutKickPlayerS2C model, int errId, bool isDispatch);

	public OnTimeOutKickPlayerS2CServerDelegate OnTimeOutKickPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushTimeOutKickPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTimeOutKickPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TimeOutKickPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TimeOutKickPlayerS2C model = param.ReadObject<TimeOutKickPlayerS2C>();
		await OnTimeOutKickPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
