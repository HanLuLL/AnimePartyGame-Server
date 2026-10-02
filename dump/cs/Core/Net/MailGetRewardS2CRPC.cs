using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailGetRewardS2CRPC
{
	public delegate UniTask OnMailGetRewardS2CServerDelegate(MailGetRewardS2C model, int errId, bool isDispatch);

	public OnMailGetRewardS2CServerDelegate OnMailGetRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushMailGetRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailGetRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailGetRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailGetRewardS2C model = param.ReadObject<MailGetRewardS2C>();
		await OnMailGetRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
