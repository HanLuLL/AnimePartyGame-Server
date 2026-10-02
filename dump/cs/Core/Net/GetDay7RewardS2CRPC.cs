using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetDay7RewardS2CRPC
{
	public delegate UniTask OnGetDay7RewardS2CServerDelegate(GetDay7RewardS2C model, int errId, bool isDispatch);

	public OnGetDay7RewardS2CServerDelegate OnGetDay7RewardS2CServerCallBackAsync;

	internal virtual async UniTask PushGetDay7RewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetDay7RewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetDay7RewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetDay7RewardS2C model = param.ReadObject<GetDay7RewardS2C>();
		await OnGetDay7RewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
