using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailReadS2CRPC
{
	public delegate UniTask OnMailReadS2CServerDelegate(MailReadS2C model, int errId, bool isDispatch);

	public OnMailReadS2CServerDelegate OnMailReadS2CServerCallBackAsync;

	internal virtual async UniTask PushMailReadS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailReadS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailReadS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailReadS2C model = param.ReadObject<MailReadS2C>();
		await OnMailReadS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
