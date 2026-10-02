using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GachaCountRewardS2CRPC
{
	public delegate UniTask OnGachaCountRewardS2CServerDelegate(GachaCountRewardS2C model, int errId, bool isDispatch);

	public OnGachaCountRewardS2CServerDelegate OnGachaCountRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushGachaCountRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGachaCountRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GachaCountRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GachaCountRewardS2C model = param.ReadObject<GachaCountRewardS2C>();
		await OnGachaCountRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
