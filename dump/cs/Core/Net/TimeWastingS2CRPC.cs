using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TimeWastingS2CRPC
{
	public delegate UniTask OnTimeWastingS2CServerDelegate(TimeWastingS2C model, int errId, bool isDispatch);

	public OnTimeWastingS2CServerDelegate OnTimeWastingS2CServerCallBackAsync;

	internal virtual async UniTask PushTimeWastingS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTimeWastingS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TimeWastingS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TimeWastingS2C model = param.ReadObject<TimeWastingS2C>();
		await OnTimeWastingS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
