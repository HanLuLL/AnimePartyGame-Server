using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ApplyToGuildS2CRPC
{
	public delegate UniTask OnApplyToGuildS2CServerDelegate(ApplyToGuildS2C model, int errId, bool isDispatch);

	public OnApplyToGuildS2CServerDelegate OnApplyToGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushApplyToGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnApplyToGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ApplyToGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ApplyToGuildS2C model = param.ReadObject<ApplyToGuildS2C>();
		await OnApplyToGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
