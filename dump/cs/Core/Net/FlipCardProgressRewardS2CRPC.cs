using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class FlipCardProgressRewardS2CRPC
{
	public delegate UniTask OnFlipCardProgressRewardS2CServerDelegate(FlipCardProgressRewardS2C model, int errId, bool isDispatch);

	public OnFlipCardProgressRewardS2CServerDelegate OnFlipCardProgressRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushFlipCardProgressRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnFlipCardProgressRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议FlipCardProgressRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		FlipCardProgressRewardS2C model = param.ReadObject<FlipCardProgressRewardS2C>();
		await OnFlipCardProgressRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
