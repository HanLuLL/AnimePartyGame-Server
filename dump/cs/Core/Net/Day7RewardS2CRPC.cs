using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class Day7RewardS2CRPC
{
	public delegate UniTask OnDay7RewardS2CServerDelegate(Day7RewardS2C model, int errId, bool isDispatch);

	public OnDay7RewardS2CServerDelegate OnDay7RewardS2CServerCallBackAsync;

	internal virtual async UniTask PushDay7RewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnDay7RewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议Day7RewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		Day7RewardS2C model = param.ReadObject<Day7RewardS2C>();
		await OnDay7RewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
