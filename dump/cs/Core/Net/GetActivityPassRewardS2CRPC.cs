using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetActivityPassRewardS2CRPC
{
	public delegate UniTask OnGetActivityPassRewardS2CServerDelegate(GetActivityPassRewardS2C model, int errId, bool isDispatch);

	public OnGetActivityPassRewardS2CServerDelegate OnGetActivityPassRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushGetActivityPassRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetActivityPassRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetActivityPassRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetActivityPassRewardS2C model = param.ReadObject<GetActivityPassRewardS2C>();
		await OnGetActivityPassRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
