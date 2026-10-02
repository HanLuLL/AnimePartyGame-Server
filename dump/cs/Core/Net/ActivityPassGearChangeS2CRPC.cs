using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ActivityPassGearChangeS2CRPC
{
	public delegate UniTask OnActivityPassGearChangeS2CServerDelegate(ActivityPassGearChangeS2C model, int errId, bool isDispatch);

	public OnActivityPassGearChangeS2CServerDelegate OnActivityPassGearChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushActivityPassGearChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnActivityPassGearChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ActivityPassGearChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ActivityPassGearChangeS2C model = param.ReadObject<ActivityPassGearChangeS2C>();
		await OnActivityPassGearChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
