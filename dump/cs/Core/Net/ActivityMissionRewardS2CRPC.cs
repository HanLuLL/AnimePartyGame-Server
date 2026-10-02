using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActivityMissionRewardS2CRPC
{
	public delegate UniTask OnActivityMissionRewardS2CServerDelegate(ActivityMissionRewardS2C model, int errId, bool isDispatch);

	public OnActivityMissionRewardS2CServerDelegate OnActivityMissionRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushActivityMissionRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActivityMissionRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActivityMissionRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActivityMissionRewardS2C model = param.ReadObject<ActivityMissionRewardS2C>();
		await OnActivityMissionRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
